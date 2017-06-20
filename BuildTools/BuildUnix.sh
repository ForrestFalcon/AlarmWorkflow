#!/bin/sh

# -------------------------------------------------
# AlarmWorkflow build script (DEBUG)
# -------------------------------------------------
export root=../

echo -------------------------------------------------
echo Restoring .nuget Packages

# ./RestorePackages.Unix.sh

echo -------------------------------------------------
echo Build Shared...
msbuild $root/Shared/Shared.sln /p:Configuration=Debug /verbosity:minimal

echo -------------------------------------------------
echo Build Windows-specific stuff...
msbuild $root/Backend/Backend.sln /p:Configuration=Debug /verbosity:minimal
msbuild $root/BackendServices/BackendServices.sln /p:Configuration=Debug /verbosity:minimal

msbuild $root/AlarmSources/AlarmSources.sln /p:Configuration=Debug /verbosity:minimal
msbuild $root/Jobs/Engine/EngineJobs.sln /p:Configuration=Debug /verbosity:minimal
