REM http://wix.sourceforge.net/manual-wix3/heat.htm

REM echo off
set sourceFolder=%1
set outputFolder=%2 
set transformPath=%3
echo on
REM the source Folder is: %sourceFolder%
REM the Output Folder is: %outputFolder%


echo off
REM This is just for easier understanding of variables
set Arguments=dir %sourceFolder%
REM  autogenerate component guids at compile time
set Arguments= %Arguments% -ag 
REM <ComponentGroupName>  component group name (cannot contain spaces e.g -cg MyComponentGroup)
set Arguments= %Arguments% -cg CorrectionaryInstallationFiles 
REM suppress fragments
set Arguments= %Arguments% -sfrag 
REM use template, one of: fragment,module,product
set Arguments= %Arguments% -template fragment 
REM <VariableName>  substitute File/@Source="SourceDir" with a preprocessor or a wix variable (e.g. -var var.MySource will become File/@Source="$(var.MySource)\myfile.txt" and
set Arguments= %Arguments% -var var.CorrectionaryForm.TargetDir 
REM suppress harvesting the root directory as an element
set Arguments= %Arguments% -srd 
REM  <DirectoryName>  directory reference to root directories (cannot contain spaces e.g. -dr MyAppDirRef)
set Arguments= %Arguments% -dr CorrectionaryFOLDER  
REM transform harvested output with XSL file
set Arguments= %Arguments% -t %transformPath% 
REM specify output file (default: write to current directory)
set Arguments= %Arguments% -out %outputFolder% 

set HeateExe="C:\Program Files (x86)\WiX Toolset v3.8\bin\Heat.exe"
echo on

REM Heat path is: %HeateExe%
REM Arguments are: %Arguments%


%HeateExe% %Arguments%

echo off
REM  usage:  heat.exe harvestType harvestSource <harvester arguments> -o[ut] sourceFile.wxs
REM 
REM Supported harvesting types:
REM 
REM    dir      harvest a directory
REM    file     harvest a file
REM    payload  harvest a bundle payload as RemotePayload
REM    perf     harvest performance counters
REM    project  harvest outputs of a VS project
REM    reg      harvest a .reg file
REM    website  harvest an IIS web site
REM 
REM Options:
REM    -ag      autogenerate component guids at compile time
REM    -cg <ComponentGroupName>  component group name (cannot contain spaces e.g -cg MyComponentGroup)
REM    -configuration  configuration to set when harvesting the project
REM    -directoryid  overridden directory id for generated directory elements
REM    -dr <DirectoryName>  directory reference to root directories (cannot contain spaces e.g. -dr MyAppDirRef)
REM    -ext     <extension>  extension assembly or "class, assembly"
REM    -g1      generated guids are not in brackets
REM    -generate
REM             specify what elements to generate, one of:
REM                 components, container, payloadgroup, layout, packagegroup
REM                 (default is components)
REM    -gg      generate guids now
REM    -indent <N>  indentation multiple (overrides default of 4)
REM    -ke      keep empty directories
REM    -nologo  skip printing heat logo information
REM    -out     specify output file (default: write to current directory)
REM    -platform  platform to set when harvesting the project
REM    -pog
REM             specify output group of VS project, one of:
REM                 Binaries,Symbols,Documents,Satellites,Sources,Content
REM               This option may be repeated for multiple output groups.
REM    -projectname  overridden project name to use in variables
REM    -scom    suppress COM elements
REM    -sfrag   suppress fragments
REM    -srd     suppress harvesting the root directory as an element
REM    -sreg    suppress registry harvesting
REM    -suid    suppress unique identifiers for files, components, & directories
REM    -svb6    suppress VB6 COM elements
REM    -sw<N>   suppress all warnings or a specific message ID
REM             (example: -sw1011 -sw1012)
REM    -swall   suppress all warnings (deprecated)
REM    -t       transform harvested output with XSL file
REM    -template  use template, one of: fragment,module,product
REM    -v       verbose output
REM    -var <VariableName>  substitute File/@Source="SourceDir" with a preprocessor or a wix variable
REM (e.g. -var var.MySource will become File/@Source="$(var.MySource)\myfile.txt" and
REM -var wix.MySource will become File/@Source="!(wix.MySource)\myfile.txt"
REM    -wixvar  generate binder variables instead of preprocessor variables
REM    -wx[N]   treat all warnings or a specific message ID as an error
REM             (example: -wx1011 -wx1012)
REM    -wxall   treat all warnings as errors (deprecated)
REM    -? | -help  this help information
REM 
REM For more information see: http://wixtoolset.org
echo on