@echo off
setlocal DisableDelayedExpansion
if not defined NSRETAIL_UPDATE_LOG set "NSRETAIL_UPDATE_LOG=%TEMP%\NSRetailPOS-AppUpdate.log"
if /i "%~1"=="--worker" goto worker
set "NSRETAIL_UPDATE_SOURCE=%~f0"
for %%I in ("%~dp0.") do if /i not "%%~nxI"=="AppFiles" goto run
rem Hand off to a local copy so the running batch stays outside AppFiles.
for %%I in ("%~dp0..") do set "NSRETAIL_UPDATE_WORKER=%%~fI\NSRetailPOS-UpdateWorker.bat"
copy /y "%~f0" "%NSRETAIL_UPDATE_WORKER%" >nul
if errorlevel 1 exit /b 1
"%NSRETAIL_UPDATE_WORKER%" --worker "%NSRETAIL_UPDATE_SOURCE%"

:worker
set "NSRETAIL_UPDATE_SOURCE=%~f2"
if not "%~3"=="" set "NSRETAIL_UPDATE_LOG=%~f3"
:run
set "NSRETAIL_UPDATE_SCRIPT=%~f0"
cd /d "%SystemRoot%"
powershell.exe -NoProfile -Command "$parts = (Get-Content -LiteralPath $env:NSRETAIL_UPDATE_SCRIPT -Raw) -split '(?m)^# POWERSHELL\r?\n', 2; & ([scriptblock]::Create($parts[1]))"
exit /b %errorlevel%
# POWERSHELL
$ErrorActionPreference = 'Stop'
$transcriptStarted = $false
$resultCode = 1
try {
Start-Transcript -LiteralPath $env:NSRETAIL_UPDATE_LOG -Append -Force | Out-Null
$transcriptStarted = $true
Write-Host "App update started at $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss.fff')"
Write-Host "Log: $env:NSRETAIL_UPDATE_LOG"
Write-Host "Source batch: $env:NSRETAIL_UPDATE_SOURCE"
Write-Host "User: $([Security.Principal.WindowsIdentity]::GetCurrent().Name)"
$principal = [Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()
Write-Host "Administrator: $($principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator))"
if (-not $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    # Pass paths explicitly: an elevated process can run as another administrator
    # and must not depend on inherited environment variables or .bat associations.
    $bootstrap = @'
$ErrorActionPreference = 'Stop'
$env:NSRETAIL_UPDATE_SCRIPT = '__SCRIPT__'
$env:NSRETAIL_UPDATE_SOURCE = '__SOURCE__'
$env:NSRETAIL_UPDATE_LOG = '__LOG__'
try {
    Add-Content -LiteralPath $env:NSRETAIL_UPDATE_LOG -Value "Elevated PowerShell started at $(Get-Date -Format o); user: $([Security.Principal.WindowsIdentity]::GetCurrent().Name)"
    $parts = (Get-Content -LiteralPath $env:NSRETAIL_UPDATE_SCRIPT -Raw) -split '(?m)^# POWERSHELL\r?\n', 2
    if ($parts.Count -ne 2) { throw 'Cannot find the PowerShell section in the updater.' }
    & ([scriptblock]::Create($parts[1]))
} catch {
    $failure = "Elevated startup failed: $($_.Exception.ToString())`r`n$($_.InvocationInfo.PositionMessage)`r`n$($_.ScriptStackTrace)"
    try { Add-Content -LiteralPath $env:NSRETAIL_UPDATE_LOG -Value $failure } catch { Write-Host $_ }
    Write-Host $failure
    exit 1
}
'@
    $bootstrap = $bootstrap.Replace('__SCRIPT__', $env:NSRETAIL_UPDATE_SCRIPT.Replace("'", "''")).Replace('__SOURCE__', $env:NSRETAIL_UPDATE_SOURCE.Replace("'", "''")).Replace('__LOG__', $env:NSRETAIL_UPDATE_LOG.Replace("'", "''"))
    $encodedBootstrap = [Convert]::ToBase64String([Text.Encoding]::Unicode.GetBytes($bootstrap))
    $startOptions = @{
        FilePath = Join-Path $PSHOME 'powershell.exe'
        Verb = 'RunAs'
        PassThru = $true
        WorkingDirectory = $env:SystemRoot
        ArgumentList = '-NoProfile -EncodedCommand ' + $encodedBootstrap
    }
    Write-Host 'Requesting administrator elevation.'
    Stop-Transcript | Out-Null
    $transcriptStarted = $false
    try {
        $updater = Start-Process @startOptions
    } catch {
        Add-Content -LiteralPath $env:NSRETAIL_UPDATE_LOG -Value "Administrator elevation failed: $($_.Exception.ToString())"
        throw
    }
    $updater.WaitForExit()
    $resultCode = $updater.ExitCode
    Add-Content -LiteralPath $env:NSRETAIL_UPDATE_LOG -Value "Elevated updater exit code: $resultCode"
    exit $resultCode
}

function Get-ApplicationFiles($directory, [switch]$CleanupOnly) {
    foreach ($item in Get-ChildItem -LiteralPath $directory -Force) {
        if ($item.Attributes -band [IO.FileAttributes]::ReparsePoint) {
            throw "Cannot clean an installation containing a link: $($item.FullName)"
        }
        if ($item.PSIsContainer) {
            foreach ($child in Get-ApplicationFiles $item.FullName -CleanupOnly:$CleanupOnly) {
                Join-Path $item.Name $child
            }
        } elseif ($item.Extension -notin '.mdf', '.ldf' -and
            (-not $CleanupOnly -or $item.Name -like 'DevExpress*.dll' -or $item.Name -ieq 'NSRetailPOS.exe')) {
            $item.Name
        }
    }
}

$source = Split-Path -Parent $env:NSRETAIL_UPDATE_SOURCE
$installation = 'C:\Windows\NSRetailPOS'
$executable = 'NSRetailPOS.exe'
Write-Host "Source folder: $source"
Write-Host "Installation folder: $installation"

if ($source -eq $installation -or $source.StartsWith($installation + '\', [StringComparison]::OrdinalIgnoreCase)) {
    throw 'Run the updater from the downloaded AppFiles folder.'
}
$installDirectory = Get-Item -LiteralPath $installation -Force
if (-not $installDirectory.PSIsContainer -or ($installDirectory.Attributes -band [IO.FileAttributes]::ReparsePoint)) {
    throw "Expected a regular POS installation directory at $installation"
}
# POS has already downloaded the update when it starts this batch.
Write-Host "Installation permissions: $((Get-Acl -LiteralPath $installation).AccessToString)"
# Rename the cache to stage its contents, then recreate the empty download folder.
$staged = $null
$sourceDirectory = Get-Item -LiteralPath $source -Force
if ($sourceDirectory.Name -eq 'AppFiles') {
    if ($sourceDirectory.Attributes -band [IO.FileAttributes]::ReparsePoint) {
        throw "Expected a regular AppFiles directory at $source"
    }

    $staged = Join-Path $sourceDirectory.Parent.FullName ('AppFiles-update-' + [Guid]::NewGuid().ToString('N'))
    Write-Host "Staging download folder at $staged"
    [IO.Directory]::Move($sourceDirectory.FullName, $staged)
    try {
        [IO.Directory]::CreateDirectory($source) | Out-Null
    } catch {
        Write-Warning "The downloaded update is still available at $staged"
        throw
    }
    $source = $staged
}

try {
    foreach ($name in @($executable, "$executable.config")) {
        Write-Host "Validating update file: $name"
        $file = Get-Item -LiteralPath (Join-Path $source $name) -Force
        if ($file.PSIsContainer -or $file.Length -eq 0 -or ($file.Attributes -band [IO.FileAttributes]::ReparsePoint)) {
            throw "Invalid application update file: $name"
        }
    }

    $backupName = 'NSRetailPOS-rollback-' + [Guid]::NewGuid().ToString('N')
    $backup = Join-Path (Split-Path -Parent $installation) $backupName
    New-Item -Path $backup -ItemType Directory | Out-Null
    Write-Host "Previous application files will be saved to $backup"

    $pos = Get-Process -Name 'NSRetailPOS' -ErrorAction SilentlyContinue
    if ($pos) {
        Write-Host "Stopping POS process IDs: $($pos.Id -join ', ')"
        Stop-Process -InputObject $pos -Force
        Wait-Process -InputObject $pos -Timeout 15
    }

    $copyStarted = $false
    try {
        $oldFiles = @(Get-ApplicationFiles $installation)
        foreach ($relativePath in $oldFiles) {
            $backupFile = Join-Path $backup $relativePath
            [IO.Directory]::CreateDirectory((Split-Path -Parent $backupFile)) | Out-Null
            $installedFile = Join-Path $installation $relativePath
            if ([IO.Path]::GetFileName($relativePath) -like 'DevExpress*.dll' -or
                [IO.Path]::GetFileName($relativePath) -ieq 'NSRetailPOS.exe') {
                Write-Host "MOVE $installedFile -> $backupFile"
                Move-Item -LiteralPath $installedFile -Destination $backupFile -Force
            } else {
                Write-Host "BACKUP COPY $installedFile -> $backupFile"
                Copy-Item -LiteralPath $installedFile -Destination $backupFile -Force
            }
        }

        $copyStarted = $true
        Write-Host "Copying update from $source to $installation"
        & robocopy.exe $source $installation '*' /COPY:DAT /IS /IT /R:2 /W:1 /XJ /XF '*.mdf' '*.ldf' 'UpdateEXEScript.bat' 'CleanUpdateEXEScript.bat' 2>&1 | ForEach-Object { Write-Host $_ }
        $copyResult = $LASTEXITCODE
        Write-Host "Robocopy exit code: $copyResult"
        if ($copyResult -ge 8) {
            throw "Copy failed (Robocopy exit code $copyResult)."
        }

        Write-Host "Starting POS from $installation"
        $restarted = Start-Process -FilePath (Join-Path $installation $executable) -WorkingDirectory $installation -PassThru
        Write-Host "POS process created. PID: $($restarted.Id)"
    } catch {
        $updateError = $_
        Write-Host "Update failed: $($_.Exception.ToString())"
        Write-Host "Failure location: $($_.InvocationInfo.PositionMessage)"
        Write-Host "Beginning rollback from $backup"
        try {
            if ($copyStarted) {
                $partialFiles = @(Get-ApplicationFiles $installation -CleanupOnly)
                foreach ($relativePath in $partialFiles) {
                    Write-Host "ROLLBACK REMOVE $relativePath"
                    Remove-Item -LiteralPath (Join-Path $installation $relativePath) -Force
                }
            }
            foreach ($relativePath in Get-ApplicationFiles $backup) {
                Write-Host "ROLLBACK RESTORE $relativePath"
                Move-Item -LiteralPath (Join-Path $backup $relativePath) -Destination (Join-Path $installation $relativePath) -Force
            }
            Write-Host 'Restored the previous application files. POS was not restarted.'
        } catch {
            Write-Warning "Rollback could not complete. Check $backup for the previous application files. $($_.Exception.Message)"
        }
        throw $updateError
    }
} finally {
    if ($staged) {
        try {
            Write-Host "Removing staged download folder: $staged"
            [IO.Directory]::Delete($staged, $true)
        } catch {
            Write-Warning "Could not remove the staged update at $staged. $($_.Exception.Message)"
        }
    }
}
$resultCode = 0
Write-Host "SUCCESS: Update copied and POS launch requested at $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss.fff')"
} catch {
    Write-Host "FAILED: $($_.Exception.ToString())"
    Write-Host "Category: $($_.CategoryInfo)"
    Write-Host "Error ID: $($_.FullyQualifiedErrorId)"
    Write-Host "Location: $($_.InvocationInfo.PositionMessage)"
    Write-Host "Stack: $($_.ScriptStackTrace)"
} finally {
    Write-Host "Updater exit code: $resultCode. Log: $env:NSRETAIL_UPDATE_LOG"
    if ($transcriptStarted) { Stop-Transcript | Out-Null }
}
exit $resultCode
