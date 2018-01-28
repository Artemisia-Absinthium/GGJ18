; CieloSegreto installer script
; Update constants to fit new version or diretory tree changes
#define SourcesPath "..\Build"
#define TargetPath ".\Setup"
#define DeployPath ".\"   
#define VersionMajor 1
#define VersionMinor 0

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{2CB362FA-C715-4472-A54B-A287C3B11CD9}
AppName=Une Simple Chronique
AppVersion={#VersionMajor}.{#VersionMinor}
AppPublisher=GGJ
DefaultDirName={pf}\Gamagora\Une Simple Chronique
DefaultGroupName=Une Simple Chronique
AllowNoIcons=yes
OutputDir={#TargetPath}
OutputBaseFilename=UneSimpleChronique_{#VersionMajor}_{#VersionMinor}_Setup
SetupIconFile={#DeployPath}\ICONE.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "{#SourcesPath}\UneSimpleChronique.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourcesPath}\UneSimpleChronique_Data\*"; DestDir: "{app}\CieloSegreto_Data"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\Une Simple Chronique"; Filename: "{app}\UneSimpleChronique.exe"
Name: "{group}\{cm:UninstallProgram,Une Simple Chronique}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\Une Simple Chronique"; Filename: "{app}\UneSimpleChronique.exe"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\Une Simple Chronique"; Filename: "{app}\UneSimpleChronique.exe"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\UneSimpleChronique.exe"; Description: "{cm:LaunchProgram,Une Simple Chronique}"; Flags: nowait postinstall skipifsilent

