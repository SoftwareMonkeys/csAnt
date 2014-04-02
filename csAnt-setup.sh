# Get nuget
mkdir lib
wget "http://nuget.org/nuget.exe" -O lib/nuget.exe

# Get csAnt setup package
mono lib/nuget.exe install csAnt-setup -Source https://www.myget.org/F/csant/ -OutputDirectory lib

# Enter csAnt package dir
cd lib/csAnt-setup.*

# Move the setup file back to the root
cp csAnt-SetUp.exe ../../

# Return to the root dir
cd ../../

# Run the setup file
mono csAnt-SetUp.exe
