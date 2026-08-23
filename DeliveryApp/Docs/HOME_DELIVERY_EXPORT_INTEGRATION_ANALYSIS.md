# Home Delivery Export Integration Analysis

## 1. Executive Summary

The requested Home Delivery data export should be added to the existing `WarehouseCloudSync` solution as a second, logically independent background process inside the existing SyncService application. The current 5-minute warehouse/cloud synchronization must continue to run on its own cadence and must not wait for CSV generation or Dropbox uploads.

Recommended architecture:

```text
WH SyncService
|
+-- Existing WH <-> Cloud Sync Worker
|      Runs about every 5 minutes
|
+-- Home Delivery Export Worker
       Runs every 3 hours
       |
       +-- WH DB export definitions/procedures
       +-- CSV file generation
       +-- New reusable Dropbox utility DLL
```

The clarified design makes the database result set the source of truth for CSV schema. C# should not hardcode vendor CSV headers, column order, or template-specific field availability. No implementation should start until Dropbox path/naming rules, file retention policy, and the final export-definition stored procedure contract are confirmed.

## 2. Existing WH SyncService Architecture

Solution:

- `WarehouseCloudSync.sln`
- One project: `WarehouseCloudSync\WarehouseCloudSync.csproj`
- Target framework: `.NET Framework 4.7.2`
- Output type: `Exe`
- Existing NuGet package: `log4net` 2.0.13

Entry point:

- `WarehouseCloudSync\Program.cs`
- `Main()` creates a global mutex named `Global\WarehouseCloudSync` to prevent multiple running instances.
- The Windows Service startup path is currently commented out.
- The active execution path is `new SyncData().StartSync();`.

Windows Service wrapper:

- `WarehouseCloudSync\NSRetailWareHouseCloudSync.cs`
- `OnStart()` starts `new SyncData().StartSync` on a raw `Thread`.
- `OnStop()` is empty, so there is currently no graceful cancellation or shutdown signal.

Current sync loop:

- `WarehouseCloudSync\SyncData.cs`
- `StartSync()` performs all work synchronously.
- After each run it executes `Thread.Sleep(5 * 60 * 1000)` and then recursively calls `StartSync()` again.
- Exceptions inside the sync process are caught and written via `SyncData.WriteLine`, so most sync failures are logged and the loop continues after the sleep.
- There is no explicit overlap protection, but the synchronous loop naturally prevents overlapping runs of the existing sync job.
- There is no `CancellationToken`, no async loop, and no scheduler abstraction.

Existing sync behavior:

- Hardcoded `BranchID = 45`.
- Sets `syncStartTime = DateTime.Now.AddMinutes(-5)`.
- Reads cloud entity sync metadata from `CloudRepository.GetEntityData(BranchID, "ToCloud")`.
- Calls `WarehouseRepository.ProccessStockMove()` before upload sync.
- For each cloud entity, reads warehouse data through `WarehouseRepository.GetEntityWiseData(entityName, syncDate)` and saves to cloud with `CloudRepository.SaveData(...)`.
- Reads cloud entity sync metadata for `"FromCloud"`.
- Reads cloud data through `CloudRepository.GetEntityWiseData(...)` and saves to warehouse with `WarehouseRepository.SaveData(...)`.
- Calls `WarehouseRepository.ProccessDayClosures()`.

Database access:

- `WarehouseCloudSync\Data\SqlCon.cs`
- Uses `System.Data.SqlClient`.
- Builds warehouse and cloud connection strings from encrypted appSettings.
- Build environment is selected by `BuildType`.
- Credentials are decrypted using a shared static AES/password-derived helper.
- Uses static connection fields: `ObjWHCon` and `ObjCloudCon`.
- Repository methods repeatedly call `SqlCon.SqlWHconn()` / `SqlCon.SqlCloudconn()` and close by calling those same methods again in `finally`.

Repository conventions:

- `WarehouseRepository` and `CloudRepository` use stored procedures and `DataTable`.
- Bulk saves pass `DataTable` values to stored procedure parameters using `AddWithValue`.
- Entity-to-procedure mappings are hardcoded in dictionaries.
- Existing command timeouts are 600 seconds for stock/day closure processing and 3600 seconds for large saves.

Logging:

- `SyncData.WriteLine()` writes to console and optionally appends to the configured `TelemetryLogFile`.
- `LoggerUtility` configures `log4net`, but it does not appear to be called from `Program.Main()` or `SyncData`.
- `App.config` contains a `log4net` event log appender, but the active sync path uses `SyncData.WriteLine`.

Configuration:

- `WarehouseCloudSync\App.config`
- Uses appSettings for encrypted warehouse/cloud database details.
- Uses `BuildType` to choose `Prod`, `Left`, or `Dev` connection settings.
- Uses `TelemetryLogFile`, currently empty.

## 3. Existing Dropbox Upload Architecture

Reference solution:

- `BackupUploadAPI.sln`
- Project: `BackupUploadAPI\BackupUploadAPI.csproj`
- Target framework: `.NET Framework 4.7.2`
- Existing Dropbox package: `Dropbox.Api` 7.0.0
- This project must remain unchanged.

Entry point:

- `BackupUploadAPI\Program.cs`
- Expects one command-line argument, for example `Prod`.
- Reads `{backuptype}Source` and `{backuptype}Target` from appSettings.
- Picks the newest file from the configured source directory.
- Calls `DropboxRepository.Upload(inputFile, targetFolder).Wait()`.

Dropbox implementation:

- `BackupUploadAPI\DropboxRepository.cs`
- Uses `Dropbox.Api.DropboxClient`.
- Uses access token, app key/app id, and app secret/app value.
- Credentials are currently encrypted strings hardcoded in source and decrypted at runtime through `Utility.Decrypt`.
- Upload destination is built as `remotePath + "/" + Path.GetFileName(localPath)`.
- Files at or below 10 MB use `client.Files.UploadAsync(remotePath, body: stream)`.
- Files over 10 MB use Dropbox upload sessions in chunks of 10 MB.
- There is no explicit retry policy, no folder creation, no timeout configuration, and no structured result object.
- Upload overwrite behavior is not explicitly specified through `CommitInfo`; therefore the new DLL should make overwrite/versioning behavior explicit after business confirmation.

Security observation:

- The reference project contains encrypted Dropbox credentials in source code. This is better than plaintext, but still not ideal because the encryption key and salt are also in source.
- The new reusable DLL should not hardcode Dropbox credentials. It should accept credentials/settings from the caller's configuration and avoid logging secrets.

## 4. Proposed Architecture

```text
WH SyncService
|
+-- Existing Sync Worker -----------------> Cloud DB
|
+-- Home Delivery Export Worker
       |
       +-- HomeDeliveryExportRepository
       |      |
       |      +-- Export-definition stored procedure
       |      +-- Export data stored procedures or approved SELECTs
       |
       +-- CsvExportService
       |      |
       |      +-- Dynamic DataTable.Columns header/order
       |      +-- temp-file write and final rename
       |
       +-- Dropbox Utility DLL
              |
              +-- Dropbox.Api client
              +-- Upload result/error model
              +-- Configured overwrite/retry behavior
```

The safest integration is to introduce a small application-lifetime coordinator in `WarehouseCloudSync` that starts two independent workers:

- Existing sync worker preserving the current 5-minute behavior.
- New Home Delivery export worker on its own 3-hour schedule.

The existing sync logic should be touched as little as possible. The preferred implementation is to wrap the existing sync cycle in an iterative worker loop, not to mix Home Delivery work into `SyncData.StartSync()`.

## 5. Threading / Async Design

Because the project targets .NET Framework 4.7.2, `PeriodicTimer` is not available unless the framework/runtime approach changes. Recommended options:

- Use async `Task` loops with `Task.Delay(interval, cancellationToken)`.
- Keep each worker isolated behind `StartAsync` / `StopAsync` style methods.
- Use `CancellationTokenSource` from `Program` or the Windows Service class.
- Use `SemaphoreSlim` or an `Interlocked` flag in the Home Delivery worker to prevent overlapping exports.

Recommended behavior:

- Existing sync worker runs its own loop every 5 minutes.
- Home Delivery worker runs its own loop every 3 hours.
- Each loop catches and logs exceptions inside the worker, so an exception in one process does not terminate the other.
- If a Home Delivery export is still running when the next 3-hour tick arrives, skip that tick and log that the previous export is still active.
- On shutdown, request cancellation and wait for a bounded time for active work to finish.

First-run timing:

- The current sync executes immediately, then sleeps for 5 minutes.
- To match that operational pattern, the Home Delivery worker should preferably execute once shortly after startup and then every configured interval.
- If the vendor or business wants uploads only at fixed wall-clock times, that should be treated as a separate scheduling requirement.

## 6. Database Export Design

Existing code strongly favors stored procedures and typed `DataTable` results. The clarified Home Delivery export contract should follow that pattern and should prefer stored procedure names over raw dynamic SELECT text.

Recommended export-definition procedure:

```text
USP_R_HOMEDELIVERY_EXPORT_DEFINITIONS
```

Suggested result columns:

```text
ExportId
ExportCode
FileName
ExportProcedureName
ExecutionOrder
Enabled
DropboxTargetFolder
```

Optional columns can be added later if needed, such as `ExportName`, `CommandTimeoutSeconds`, or `IsInitialPull`, but they are not required for the initial clarified contract.

Example:

```text
1 | INVENTORY      | inventory.csv       | USP_R_HD_INVENTORY      | 1 | 1 | /HomeDelivery
2 | ENTITY_MASTER  | entity-master.csv   | USP_R_HD_ENTITY_MASTER  | 2 | 1 | /HomeDelivery
3 | PRODUCT_MASTER | product-master.csv  | USP_R_HD_PRODUCT_MASTER | 3 | 1 | /HomeDelivery
```

Recommended data flow:

1. Application calls one definition procedure.
2. Procedure returns enabled export jobs in execution order.
3. Application invokes the returned stored procedure names after validating them against an allow-list or naming convention.
4. Each export procedure returns a `DataTable`.
5. CSV generator reads `DataTable.Columns` and writes headers in exactly that returned order.
6. CSV generator writes row values in the same column order.
7. Generated CSV is uploaded to the configured Dropbox target folder.

CSV schema ownership:

- Stored procedures determine which columns exist.
- Stored procedures determine header names through SQL aliases.
- Stored procedures determine column order through the SELECT list.
- Stored procedures determine blank/unavailable values, for example `'' AS [SEO Title]`.
- C# should not contain export-specific knowledge such as which Product Master fields are unavailable.
- Adding, removing, renaming, or reordering CSV columns should normally be achievable by changing the relevant stored procedure, without redeploying the application.

Dynamic-query approach requested in the prompt:

- A procedure can return SELECT text, but this increases security and maintenance risk.
- If dynamic SQL is retained, the app should only execute rows from trusted DB configuration, reject non-SELECT statements, reject multiple statements, log only query identifiers, and preferably execute through stored procedures using parameters rather than concatenated SQL.
- The procedure should return `ExportId`, `ExportCode`, `FileName`, `SqlText`, `ExecutionOrder`, `Enabled`, `DropboxTargetFolder`, and optional timeout/parameter metadata.

Cleaner alternative:

- Have the definition procedure return stored procedure names, not raw SELECT strings.
- This aligns with the existing repository style, improves deployment/versioning, reduces SQL injection risk, simplifies parameterization, and makes errors easier to diagnose.

SQL connection concurrency:

- The existing application uses static `SqlConnection` fields in `SqlCon`.
- Because the existing sync worker and Home Delivery worker can run simultaneously, the Home Delivery repository should preferably create and dispose a separate `SqlConnection` per DB operation.
- The Home Delivery repository should not depend on sharing an already-open static connection with the existing sync process.
- This can be done without refactoring unrelated existing repository code unless implementation reveals it is necessary.

## 7. CSV Template Analysis

The vendor CSV templates were provided and should be used to design the SQL stored procedure outputs. The clarified runtime architecture remains unchanged: C# must not hardcode those template schemas, and each stored procedure result set remains the source of truth for CSV structure.

Clarified CSV generation rule:

- The vendor templates are useful for designing the SQL output, not for hardcoding C# template schemas.
- C# must not define fixed template headers for Inventory, Entity Master, or Product Master.
- Each export stored procedure returns the exact columns to write.
- `DataTable.Columns` is the runtime CSV schema.
- Header names must exactly match returned column names.
- Header order must exactly match returned column order.
- Row values must be serialized in the same returned order.
- Missing/unavailable vendor fields should be emitted by SQL as empty aliases where required, for example `'' AS [SEO Title]`.

Entity Initial Pull / Bulk Inventory Template:

Mandatory columns:

- `Item Code / Barcode *`
- `Is Publish To Customer *`
- `Inventory Count *`

Likely mappings:

- `Item Code / Barcode *`: item barcode from `ITEMCODE` or item code from `ITEM`.
- `Inventory Count *`: stock quantity from stock summary/inventory result, likely related to existing `STOCKSUMMARY` sync entity.
- `Is Publish To Customer *`: business-controlled publish flag; not discoverable from current code.
- In the clarified design, these mappings belong in `USP_R_HD_INVENTORY` or its equivalent SQL procedure, not in C#.

Unknown decisions:

- Whether item code or barcode takes precedence.
- Whether inventory count is warehouse-level, store-level, saleable quantity, or physical stock.
- Whether negative stock should be exported as negative or clamped to zero.
- Whether this file is an initial/full pull only or also the recurring inventory update.

Entity Master Template:

Mandatory columns:

- `Entity Name *`
- `Entity Type *`
- `Entity Category *`
- `Erp Id *`
- `Address Line 1 *`
- `Locality *`
- `City *`
- `State *`
- `Country *`
- `Pincode *`

Likely mappings:

- Entity likely maps to warehouse/store/branch records from the ERP branch/location tables.
- Existing sync includes `BRANCH`, so branch/warehouse identifiers are probably relevant.
- `Entity Code` and `Erp Id` should likely use stable branch/store/warehouse identifiers, but the exact field must be confirmed.
- `Erp Source` could identify this ERP instance/source, for example `NSRetail`, but this is a business/API decision.
- In the clarified design, available and unavailable columns should be selected/aliased by `USP_R_HD_ENTITY_MASTER` or its equivalent SQL procedure.

Unknown decisions:

- Vendor-accepted values for `Entity Type`, `Entity Category`, `Status`, `Default`, and `OwnerShip`.
- Whether warehouse and stores both need to be exported.
- Which address/contact/GST/FSSAI/CIN fields exist in ERP and which are mandatory to vendor validation.
- Whether `Entity Code` can be blank when `Erp Id` is populated.

Global Product & Master Catalogue Template:

Mandatory columns:

- `Product Code *`
- `Product Name *`
- `Variant SKU *`
- `UOM *`
- `Product Type *`
- `Category *`

Likely mappings:

- Product/item fields likely map from existing `ITEM`.
- Barcode likely maps from existing `ITEMCODE`.
- UOM likely maps from existing `UOM`.
- Category/subcategory likely maps from existing `TBLCATEGORY` and/or `ITEMGROUP`.
- MRP likely maps from `ITEMPRICE`.
- Stock/inventory likely maps from stock summary/inventory data if the catalogue file must include availability.
- HSN/tax fields likely map from GST/tax tables because existing sync includes `GST`.
- In the clarified design, available and unavailable columns should be selected/aliased by `USP_R_HD_PRODUCT_MASTER` or its equivalent SQL procedure.

Unknown decisions:

- Product/variant relationship rules.
- Whether `Variant SKU` is item code, barcode, item-price id, or another SKU.
- Image folder and image naming conventions.
- Online/in-store discount mapping from offers.
- Required values for `Inventory Type`, `Tax Template`, `Tax Inclusive`, publish flag, SEO fields, slug, tags, and classification.
- Whether master catalogue should be full export every 3 hours or less frequent.

CSV formatting concerns:

- Header text and order must be read dynamically from `DataTable.Columns`.
- Values containing commas, quotes, or line breaks must be escaped correctly.
- Use invariant culture for decimals and quantities.
- Prefer UTF-8 with a clear decision on BOM based on vendor import behavior.
- Write to `.tmp` first and rename only after successful generation.
- The CSV writer must remain export-agnostic and must not contain branches such as Inventory/Product/Entity-specific handling.

## 8. New Dropbox Utility DLL Design

Recommended project name:

```text
WarehouseCloudSync.DropboxUtility
```

This matches the current solution name more closely than a broader `WH.SyncService.*` prefix.

Responsibility:

- Encapsulate Dropbox SDK usage.
- Accept upload settings and paths from callers.
- Upload one file or all files in a folder.
- Hide Dropbox token/client details from application code.
- Return structured upload results.

Possible public API:

```text
Task<DropboxUploadResult> UploadFileAsync(string localFilePath, string dropboxDestinationPath, CancellationToken cancellationToken)
Task<IReadOnlyList<DropboxUploadResult>> UploadFilesAsync(string localFolderPath, string dropboxDestinationFolder, string searchPattern, CancellationToken cancellationToken)
```

Configuration model:

```text
AccessToken
AppKey
AppSecret
ChunkSizeBytes
OverwriteMode
CreateFolderIfMissing
MaxRetryCount
RetryDelaySeconds
```

Result model:

```text
Success
LocalPath
DropboxPath
UploadedBytes
StartedAt
CompletedAt
ErrorCode
ErrorMessage
```

Dependency:

- Reuse `Dropbox.Api` unless a newer compatible package is intentionally chosen.
- Avoid adding Google Drive dependencies to this new DLL.

## 9. Home Delivery Export Components

Class: `HomeDeliveryExportWorker`

- Responsibility: Own the 3-hour schedule, cancellation, overlap protection, and top-level exception isolation.
- Project: `WarehouseCloudSync`
- Dependencies: `HomeDeliveryExportService`, logger/configuration.

Class: `HomeDeliveryExportService`

- Responsibility: Run one full export cycle: load definitions, fetch data, generate CSVs, upload files, record results.
- Project: `WarehouseCloudSync`
- Dependencies: repository, CSV writer, Dropbox utility, logger.

Class: `HomeDeliveryExportRepository`

- Responsibility: Call warehouse DB export-definition and export-data stored procedures.
- Project: `WarehouseCloudSync`
- Dependencies: `SqlConnection` creation helper or existing `SqlCon` pattern.

Class: `HomeDeliveryExportDefinition`

- Responsibility: Represent one export file/job returned by DB configuration.
- Project: `WarehouseCloudSync`
- Dependencies: none.

Class: `CsvExportService`

- Responsibility: Write any `DataTable` to CSV by reading `DataTable.Columns`, writing headers in returned order, writing rows in the same order, escaping values, writing a `.tmp` file, and renaming to the final `.csv` only after successful completion.
- Project: `WarehouseCloudSync`
- Dependencies: `System.Data`, file system.

Conceptual API, to be finalized only during implementation:

```text
Task<CsvExportResult> WriteAsync(DataTable data, string outputFilePath, CancellationToken cancellationToken)
```

No template-specific C# classes should be introduced for CSV shape unless a later requirement creates a compelling non-schema responsibility outside CSV columns.

Class: `DropboxUploadClient` or `DropboxFileUploader`

- Responsibility: Upload files to Dropbox with chunk handling and result reporting.
- Project: `WarehouseCloudSync.DropboxUtility`
- Dependencies: `Dropbox.Api`.

## 10. Configuration Changes

Final implementation uses DB-backed JSON configuration, not Home Delivery appSettings.

Home Delivery config JSON:

```json
{
  "Enabled": false,
  "IntervalMinutes": 180,
  "RunOnStartup": true,
  "WorkingFolder": "D:\\HomeDelivery\\Exports",
  "MainProcedureName": "USP_R_HOMEDELIVERY_EXPORT_DEFINITIONS",
  "CommandTimeoutSeconds": 3600,
  "DropboxRootFolder": "/HomeDelivery"
}
```

Dropbox config JSON:

```json
{
  "AccessToken": "<encrypted access token>",
  "AppKey": "<encrypted app key>",
  "AppSecret": "<encrypted app secret>",
  "Encrypted": true,
  "Overwrite": true,
  "MaxRetryCount": 3,
  "RetryDelaySeconds": 30,
  "ChunkSizeBytes": 10485760
}
```

DB storage:

- `dbo.APP_CONFIG` stores separate JSON rows for `HOME_DELIVERY` and `DROPBOX`.
- `dbo.HD_EXPORT_PROCEDURE` stores export sub-procedure definitions.
- `USP_R_HOMEDELIVERY_CONFIG` returns Home Delivery JSON.
- `USP_R_DROPBOX_CONFIG` returns generic Dropbox JSON.
- `MainProcedureName` points to the main export-definition procedure, which returns the enabled sub-procs to execute.

## 11. Logging and Monitoring

Use the active logging mechanism currently used by the service unless logging is deliberately standardized first.

Recommended event examples:

```text
Home Delivery export started
Home Delivery export definitions loaded: 3 enabled
Executing export: ProductMaster
ProductMaster: 18420 records fetched
ProductMaster.csv generated: <path>, <bytes>
Uploading ProductMaster.csv to Dropbox folder: /HomeDelivery/Products
ProductMaster upload successful
Home Delivery export completed. Duration: 00:02:31
```

Failure logs should include:

- Export code/name.
- Stage: definition load, query execution, CSV generation, validation, upload, archive/delete.
- Exception type and message.
- No access tokens, app secrets, full connection strings, or decrypted credentials.

## 12. Error Handling / Retry Strategy

Database failures:

- Catch per export cycle.
- Log procedure/export identifier.
- Use command timeouts appropriate to expected data volume.
- Do not crash the existing sync worker.

CSV/filesystem failures:

- Write to `.tmp`.
- Flush and close stream before rename.
- Delete incomplete temp files where safe.
- Retain failed output files for troubleshooting if configured.

Dropbox failures:

- Retry transient network/rate-limit failures with bounded retries.
- Treat authentication failures as non-transient and log clearly without secrets.
- Return structured failure results from the Dropbox DLL.

Partial failure:

- Recommended default: each file is independently generated and uploaded; if one export fails, log it and continue with the remaining enabled exports.
- Business decision required: whether one failed file should block uploading the others.

## 13. File Lifecycle

Recommended lifecycle:

```text
DB result
  |
  v
WorkingFolder\FileName.csv.tmp
  |
  v
WorkingFolder\FileName.csv
  |
  v
Dropbox upload
  |
  v
Archive / Retain / Delete according to configuration
```

Recommended defaults:

- Keep failed files and logs for diagnosis.
- Archive successful files with timestamped names unless business requires deletion.
- Use deterministic vendor-facing filenames only if Dropbox overwrite is expected.
- Include timestamped local archive names to avoid losing historical evidence.

Open decisions:

- Whether Dropbox files should overwrite the previous file or create new versions/timestamped files.
- Retention duration/count for local archives.
- Whether successful local files should be deleted after upload.

## 14. Existing Files That Would Need Modification

Existing file: `WarehouseCloudSync\Program.cs`

- Proposed modification: Start an application coordinator or two independent workers instead of directly blocking on `new SyncData().StartSync()`.
- Reason: Allows the existing 5-minute sync and Home Delivery 3-hour export to coexist without blocking each other.

Existing file: `WarehouseCloudSync\NSRetailWareHouseCloudSync.cs`

- Proposed modification: Store worker/coordinator instance, start it in `OnStart`, signal cancellation in `OnStop`, and wait briefly for graceful shutdown.
- Reason: Current `OnStop()` is empty, so new background work needs a lifetime signal.

Existing file: `WarehouseCloudSync\SyncData.cs`

- Proposed modification: Prefer extracting one sync cycle method or changing recursion/sleep into a loop controlled by the coordinator.
- Reason: The current recursive `StartSync()` is hard to coordinate with cancellation and other background workers. Behavior should remain the same: run once, then wait about 5 minutes.

Existing file: `WarehouseCloudSync\Data\SqlCon.cs`

- Proposed modification: Either add a safe way to create independent warehouse connections for the Home Delivery repository or ensure repository calls do not share static open connections across concurrent workers.
- Reason: Static `SqlConnection` fields are risky once two workers can access the warehouse DB concurrently.

Existing file: `WarehouseCloudSync\App.config`

- Proposed modification: Add Home Delivery export configuration and Dropbox settings.
- Reason: Interval, folders, stored procedure names, and credentials must not be hardcoded.

Existing file: `WarehouseCloudSync\WarehouseCloudSync.csproj`

- Proposed modification: Include new Home Delivery classes and add a project reference to the new Dropbox utility DLL.
- Reason: Required to compile the new worker/export code.

Existing file: `WarehouseCloudSync.sln`

- Proposed modification: Add the new Dropbox utility class library project.
- Reason: The reusable Dropbox uploader should be part of the WH SyncService solution.

## 15. New Files / Projects That Would Be Created

New project:

```text
WarehouseCloudSync.DropboxUtility
```

Likely files:

- `DropboxOptions.cs`
- `DropboxUploadResult.cs`
- `DropboxUploadException.cs`
- `IDropboxFileUploader.cs`
- `DropboxFileUploader.cs`

New files in `WarehouseCloudSync`:

- `ApplicationCoordinator.cs`
- `Workers\WarehouseSyncWorker.cs`
- `HomeDelivery\HomeDeliveryExportWorker.cs`
- `HomeDelivery\HomeDeliveryExportService.cs`
- `HomeDelivery\HomeDeliveryExportRepository.cs`
- `HomeDelivery\HomeDeliveryExportDefinition.cs`
- `HomeDelivery\HomeDeliveryExportResult.cs`
- `HomeDelivery\CsvExportService.cs`
- `HomeDelivery\CsvExportResult.cs`
- `HomeDelivery\HomeDeliveryExportOptions.cs`

Optional test project:

- `WarehouseCloudSync.Tests` if the solution should add unit coverage for CSV escaping, dynamic `DataTable.Columns` header order, file naming, and scheduler overlap logic.
- CSV coverage should verify that `DataColumn.ColumnName` becomes the header exactly, column ordinal order is preserved, adding/removing/renaming/reordering columns changes the CSV without C# changes, `DBNull` and empty strings become empty fields, commas/quotes/CR/LF are escaped correctly, empty tables with defined columns still write a header row, `.tmp` files are not treated as completed exports, and the final `.csv` exists only after successful write/close.

## 16. NuGet / External Dependency Impact

Existing reusable packages:

- `log4net` exists in WH SyncService, though current active code mostly uses `SyncData.WriteLine`.
- `Dropbox.Api` exists in the reference project, not in WH SyncService.

Recommended new dependency:

- `Dropbox.Api` in `WarehouseCloudSync.DropboxUtility`.

CSV dependency:

- No CSV library exists in the current WH SyncService project.
- Because the CSV writer is generic and only needs to serialize `DataTable.Columns` and rows correctly, a small internal CSV writer is acceptable and avoids a new dependency.
- If future exports become complex, `CsvHelper` could be considered, but it is not necessary for the first implementation.

No recommendation to add Google Drive packages for this requirement.

## 17. Risks

- Existing recursive sync loop is not cancellation-friendly.
- Existing static SQL connections may become unsafe when multiple workers run concurrently.
- Large product exports may take long enough to overlap the next schedule.
- Dynamic SQL returned from DB increases injection and operational risk.
- CSV column mismatch could cause vendor import failure.
- Culture-specific decimal/quantity formatting could corrupt numeric fields.
- Partial CSV files may be uploaded if temp-file handling is not used.
- Dropbox auth/network failures could cause repeated failures.
- Hardcoded or logged credentials would create a security issue.
- Duplicate exports may occur if filenames/overwrite rules are unclear.
- Initial pull versus recurring update behavior is not yet defined.
- If master files and inventory files have different expected frequencies, a single 3-hour cadence may be wrong.

## 18. Open Questions / Business Decisions

- Should all three files run every 3 hours?
- Is `entity-initial-pull-template-bulk.csv` only for first-time synchronization?
- Should exports be full snapshots or changed records only?
- Should successful files upload even if another export fails?
- Should Dropbox files overwrite previous files or use timestamped filenames?
- What exact file names does the vendor expect?
- Should old generated files be archived, retained, or deleted after upload?
- What determines `Is Publish To Customer`?
- Should inventory count be warehouse stock, store stock, saleable stock, or available-to-promise quantity?
- Should negative inventory be exported?
- What vendor values are expected for entity type/category/status/default/ownership?
- Should entity include only stores, only warehouse, or both?
- What should be used for `Entity Code` versus `Erp Id`?
- What should `Erp Source` be?
- Which product field is `Variant SKU`?
- How should product images be supplied and named?
- How should discounts/offers map to online and in-store discount columns?
- What happens if Dropbox upload succeeds for one file and fails for another?
- What retry count and retry delay are acceptable?
- Should DB export definitions return stored procedure names or raw SELECT text?
- Are actual vendor CSV sample files available for validation beyond the column lists in the prompt?

## 19. Recommended Implementation Sequence

Phase 1 - Confirm contracts and mappings:

- Finalize SQL-owned export procedure outputs.
- Finalize initial/full versus recurring behavior.
- Finalize Dropbox paths and overwrite rules.
- Finalize DB stored procedure contract.

Phase 2 - Reusable Dropbox utility:

- Add `WarehouseCloudSync.DropboxUtility`.
- Implement configurable Dropbox upload with structured results and retries.

Phase 3 - Home Delivery DB repository:

- Implement export definition loading.
- Implement export data retrieval through stored procedures or approved dynamic SELECT flow.

Phase 4 - CSV generation:

- Implement generic `DataTable` to CSV serialization.
- Read headers and order from `DataTable.Columns`.
- Add escaping, invariant formatting, null/`DBNull` handling, temp writes, atomic rename, and validation.

Phase 5 - Home Delivery export service:

- Orchestrate DB -> CSV -> Dropbox.
- Log per-file results and partial failures.

Phase 6 - Background worker integration:

- Add independent 3-hour worker.
- Add overlap protection and cancellation.
- Preserve existing 5-minute sync behavior.

Phase 7 - Configuration/logging:

- Add appSettings.
- Ensure secrets are protected and never logged.

Phase 8 - Testing:

- Unit test CSV escaping/header order.
- Integration test DB procedure contract in a safe environment.
- Test Dropbox upload with small and chunked files.
- Test worker overlap prevention and shutdown.

## 20. Implementation Approval Gate

```text
STATUS: IMPLEMENTATION APPROVED AND COMPLETED

Source-code changes have been made after explicit "go ahead" approval.
```

## 21. Implementation Summary

Files created:

- `WarehouseCloudSync.DropboxUtility\WarehouseCloudSync.DropboxUtility.csproj`
- `WarehouseCloudSync.DropboxUtility\Properties\AssemblyInfo.cs`
- `WarehouseCloudSync.DropboxUtility\DropboxOptions.cs`
- `WarehouseCloudSync.DropboxUtility\DropboxUploadResult.cs`
- `WarehouseCloudSync.DropboxUtility\IDropboxFileUploader.cs`
- `WarehouseCloudSync.DropboxUtility\DropboxFileUploader.cs`
- `WarehouseCloudSync\ApplicationCoordinator.cs`
- `WarehouseCloudSync\Workers\WarehouseSyncWorker.cs`
- `WarehouseCloudSync\HomeDelivery\HomeDeliveryExportOptions.cs`
- `WarehouseCloudSync\HomeDelivery\HomeDeliveryExportDefinition.cs`
- `WarehouseCloudSync\HomeDelivery\HomeDeliveryExportRepository.cs`
- `WarehouseCloudSync\HomeDelivery\DropboxUploadConfig.cs`
- `WarehouseCloudSync\HomeDelivery\CsvExportService.cs`
- `WarehouseCloudSync\HomeDelivery\CsvExportResult.cs`
- `WarehouseCloudSync\HomeDelivery\HomeDeliveryExportService.cs`
- `WarehouseCloudSync\HomeDelivery\HomeDeliveryExportResult.cs`
- `WarehouseCloudSync\HomeDelivery\HomeDeliveryExportWorker.cs`
- `Database\HomeDeliveryExportSchema.sql`

Files modified:

- `WarehouseCloudSync.sln`
- `WarehouseCloudSync\WarehouseCloudSync.csproj`
- `WarehouseCloudSync\App.config`
- `WarehouseCloudSync\Program.cs`
- `WarehouseCloudSync\NSRetailWareHouseCloudSync.cs`
- `WarehouseCloudSync\SyncData.cs`
- `HOME_DELIVERY_EXPORT_INTEGRATION_ANALYSIS.md`

New project created:

- `WarehouseCloudSync.DropboxUtility`

NuGet packages added:

- `Dropbox.Api` 7.0.0 through `PackageReference` in `WarehouseCloudSync.DropboxUtility`.
- The existing `log4net` 2.0.13 package was restored for the existing `WarehouseCloudSync` project during build; it was not upgraded or changed. NuGet reported a known moderate vulnerability for this existing version.

Configuration storage:

- Home Delivery-specific appSettings were removed from `App.config`.
- Home Delivery export settings are stored as JSON in DB config row `CONFIGCODE = 'HOME_DELIVERY'`.
- Dropbox settings are stored as separate JSON in DB config row `CONFIGCODE = 'DROPBOX'`.
- Dropbox credential values may be stored encrypted using the same existing `SqlCon.Decrypt`/BackupUploadAPI encryption format when JSON has `"Encrypted": true`.
- Export sub procedures are stored separately in `dbo.HD_EXPORT_PROCEDURE`.
- `Database\HomeDeliveryExportSchema.sql` defines the proposed tables and procedures.
- Generic Dropbox config is exposed through `USP_R_DROPBOX_CONFIG` so it can be reused by future processes such as DB backup upload.

Key implementation decisions:

- Home Delivery configuration is loaded from DB through `USP_R_HOMEDELIVERY_CONFIG`.
- Dropbox configuration is loaded from DB through generic `USP_R_DROPBOX_CONFIG`, after CSV generation succeeds.
- Home Delivery is disabled by default in the seed DB JSON to avoid accidental execution before export procedures and Dropbox credentials are configured.
- Existing sync behavior is preserved by extracting the existing sync body into `SyncData.ExecuteSyncCycle()` and keeping `StartSync()` as a 5-minute loop.
- `ApplicationCoordinator` starts the existing sync worker and the Home Delivery worker independently; the Home Delivery worker decides whether to run based on DB config.
- `HomeDeliveryExportWorker` reads Home Delivery JSON config from DB, uses a configurable interval, defaults to 180 minutes, supports run-on-startup, catches isolated failures, and prevents overlapping executions.
- `HomeDeliveryExportRepository` uses separate `SqlConnection` instances per operation and does not share the existing static open connections.
- The Home Delivery JSON contains `MainProcedureName`; that main procedure returns configured export sub-procedure rows.
- Export sub-procedure names are loaded from DB; the three export jobs are not hardcoded in orchestration.
- `CsvExportService` is fully generic and writes headers/rows from `DataTable.Columns` ordinal order.
- CSV generation writes `FileName.csv.tmp` first and only moves to final `.csv` after a successful write/close.
- `.tmp` files are rejected by the Dropbox uploader and are never uploaded.
- The new Dropbox DLL does not hardcode credentials and returns structured upload results.
- The new Dropbox DLL supports both single-file upload and local-folder upload to a supplied Dropbox destination folder.
- Successful CSV files are retained locally after upload; delete/archive policy remains a business decision.

Deviations from original analysis:

- Removed the proposed `HomeDeliveryCsvTemplates` class because the final design makes SQL result sets the only runtime CSV schema source.
- Did not add archive/delete behavior because retention rules are still ambiguous; the conservative implementation keeps generated files.
- Did not implement Dropbox folder creation because the first-version requirement only needs externally supplied destination paths and avoiding over-engineering was requested.
- Replaced Home Delivery appSettings with DB JSON config after review feedback.

Build status:

- `MSBuild WarehouseCloudSync.sln /t:Restore /p:RestorePackagesConfig=true /p:Configuration=Debug` succeeded.
- `MSBuild WarehouseCloudSync.sln /p:Configuration=Debug /m` succeeded with 0 warnings and 0 errors after restore.

Tests/verifications performed:

- Built the new Dropbox utility DLL.
- Built the WH SyncService solution.
- Verified Home Delivery appSettings were removed from `App.config`.
- Verified generic CSV behavior using the compiled `CsvExportService`:
- `DataColumn.ColumnName` becomes the header exactly.
- `DataTable.Columns` ordinal order is preserved.
- Added columns automatically appear.
- Removed columns disappear.
- Renamed columns change the CSV header.
- Reordered columns change CSV order.
- `DBNull` becomes an empty field.
- Empty string remains an empty field.
- Commas are quoted.
- Double quotes are escaped as doubled quotes.
- Newlines are quoted without corrupting rows.
- Empty `DataTable` with columns still writes a header row.
- `.tmp` file was not left behind after successful write.
- Final `.csv` existed only after successful generation.

Outstanding items requiring business/database confirmation:

- Create and deploy `USP_R_HOMEDELIVERY_EXPORT_DEFINITIONS`.
- Create and deploy `USP_R_HOMEDELIVERY_CONFIG` and generic `USP_R_DROPBOX_CONFIG` using `Database\HomeDeliveryExportSchema.sql`.
- Create and deploy each configured export stored procedure, for example `USP_R_HD_INVENTORY`, `USP_R_HD_ENTITY_MASTER`, and `USP_R_HD_PRODUCT_MASTER`.
- Confirm final Dropbox credentials and destination folder paths.
- Decide whether Home Delivery should be enabled by default in production configuration.
- Decide whether successful local CSV files should be retained, archived, or deleted.
- Confirm whether all exports run every 3 hours or whether master data needs a different cadence.
- Confirm Dropbox overwrite/versioning expectations.
- Confirm vendor/business mappings in SQL for publish flags, inventory quantity, entity type/category, product variant SKU, images, discounts, and SEO fields.
