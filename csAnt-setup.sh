libDir="lib"
nugetFile="$libDir/nuget.exe"
sourcePath="https://www.myget.org/F/softwaremonkeys/"

# Create lib directory
if [ ! -d "$libDir" ]; then
    mkdir $libDir
fi

# Get nuget
if [ ! -f "$nugetFile" ]; then
    wget "http://nuget.org/nuget.exe" -O $nugetFile
fi

# Get csAnt setup package

mono $nugetFile install csAnt-setup -Source $sourcePath -OutputDirectory lib -NoCache

# Enter csAnt package dir
cd lib/csAnt-setup.*

# Move the setup file back to the root
cp csAnt-SetUp.exe ../../

# Return to the root dir
cd ../../

# Run the setup file
mono csAnt-SetUp.exe "$@"
