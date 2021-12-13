#define MyAppName "AMSService"
#define MyAppVersion "1.0"
#define MyAppPublisher "Categis Software"
#define MyAppURL "http://www.softwaretogo.de/"
#define MyAppExeName "AMSService.exe"

[Setup]
AppId={{E3E92DFD-156F-4ADF-8A5F-9E680E2A7E68}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={win}\AMSService
; DisableDirPage=yes
AlwaysShowDirOnReadyPage=yes
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
OutputBaseFilename=AMSService
Compression=lzma
SolidCompression=yes
DisableProgramGroupPage=yes
UsePreviousTasks=Yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"

[Files]
source: "E:\Setup\AMSService\input\*"; destdir: "{win}\AMSService"; flags: ignoreversion recursesubdirs createallsubdirs

[Run]
Filename: {sys}\sc.exe; Parameters: "create AMSService start=auto binPath= ""{win}\AMSService\AMSService.exe""" ; Flags: runhidden
Filename: {sys}\sc.exe; Parameters: "start AMSService" ; Flags: runhidden

[UninstallRun]
Filename: {sys}\sc.exe; Parameters: "stop AMSService" ; Flags: runhidden
Filename: {sys}\sc.exe; Parameters: "delete AMSService" ; Flags: runhidden




