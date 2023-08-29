[Setup]
AppName=ProjInda Recipe Manager
AppVersion=1.0
DefaultDirName={pf}\ProjInda Recipe Manager
DefaultGroupName=ProjInda Recipe Manager
OutputDir=Output2

[Files]
Source: "C:\Users\Adam\Documents\ProjIndaMapp\ProjInda Recipe Manager\bin\Release\net7.0-windows\*"; DestDir: "{app}"
Source: "C:\Users\Adam\Documents\ProjIndaMapp\ProjInda Recipe Manager\bin\Release\net7.0-windows\runtimes\*"; DestDir: "{app}\runtimes"; Flags: recursesubdirs

[Icons]
Name: "{commondesktop}\ProjInda Recipe Manager"; Filename: "{app}\ProjInda Recipe Manager.exe"; WorkingDir: "{app}"
