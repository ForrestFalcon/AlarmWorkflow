@echo off
rem -------------------------------------------------
rem AlarmWorkflow build script (RELEASE)
rem 
rem Please change "Framework64" to "Framework" in the following path
rem if you're running this script on a x86 or 32-bit OS.
rem -------------------------------------------------
SET build="C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\msbuild.exe"
IF NOT EXIST build SET build="C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe"
SET root=%~dp0\..

echo -------------------------------------------------
echo Build Shared...
%build% %root%\Shared\Shared.sln /p:Configuration=Release /verbosity:minimal

echo -------------------------------------------------
echo Build Windows-specific stuff...
%build% %root%\Backend\Backend.sln /p:Configuration=Release /verbosity:minimal
%build% %root%\BackendServices\BackendServices.sln /p:Configuration=Release /verbosity:minimal

%build% %root%\AlarmSources\AlarmSources.sln /p:Configuration=Release /verbosity:minimal

%build% %root%\Windows\Windows.sln /p:Configuration=Release /verbosity:minimal
%build% %root%\WindowsUIWidgets\WindowsUIWidgets.sln /p:Configuration=Release /verbosity:minimal
%build% %root%\Configuration\Configuration.sln /p:Configuration=Release /verbosity:minimal

%build% %root%\Jobs\Engine\EngineJobs.sln /p:Configuration=Release /verbosity:minimal
%build% %root%\Jobs\WindowsUI\WindowsUIJobs.sln /p:Configuration=Release /verbosity:minimal

%build% %root%\Tools\Tools.sln /p:Configuration=Release /verbosity:minimal

pause