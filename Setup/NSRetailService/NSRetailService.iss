#define MyAppName "NSRetailService"
#define MyAppVersion "1.0.1"
#define MyAppPublisher "NSoftSol"
#define MyAppURL "http://www.NSoftSol.com/"
#define MyAppExeName "NSRetailService.exe"

[Setup]
AppId={{E3E92DFD-156F-4ADF-8A5F-9E680E2A7E68}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={win}\NSRetailService
; DisableDirPage=yes
AlwaysShowDirOnReadyPage=yes
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
OutputBaseFilename=NSRetailService
Compression=lzma
SolidCompression=yes
DisableProgramGroupPage=yes
UsePreviousTasks=Yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"

[Files]
source: "G:\Projects\InventoryControl\Setup\NSRetailService\input\*"; destdir: "{win}\NSRetailService"; flags: ignoreversion recursesubdirs createallsubdirs

[Run]
Filename: {sys}\sc.exe; Parameters: "create WarehouseCloudSync start=auto binPath= ""{win}\NSRetailService\WarehouseCloudSync.exe""" ; Flags: runhidden
Filename: {sys}\sc.exe; Parameters: "start WarehouseCloudSync" ; Flags: runhidden

[UninstallRun]
Filename: {sys}\sc.exe; Parameters: "stop WarehouseCloudSync" ; Flags: runhidden
Filename: {sys}\sc.exe; Parameters: "delete WarehouseCloudSync" ; Flags: runhidden




