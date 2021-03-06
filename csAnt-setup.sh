libDir="lib"
nugetFile="$libDir/nuget.exe"
sourcePath="https://www.myget.org/F/softwaremonkeys/"
nugetUrl="http://nuget.org/nuget.exe"

echo ""
echo "Setting up csAnt..."
echo ""
echo "(Please wait. This might take a while, as files need to be downloaded and installed.)"
echo ""

STATUS=''

for i in "$@"
do
case $i in
    -s=*|-status=*|--status=*)
    STATUS="${i#*=}"
    shift
    ;;
esac
done

if [ -z $STATUS ]; then
    STATUS='stable'
fi 

echo "Target status: $STATUS"

echo "Installing certificates..."
mozroots --import --sync
echo "... done."
echo ""

# Create lib directory
if [ ! -d "$libDir" ]; then
    mkdir $libDir
fi

# Get nuget
if [ ! -f "$nugetFile" ]; then
    echo "Getting the nuget.exe file"
    wget $nugetUrl -O $nugetFile
    echo "Done"
    echo ""
fi

# TODO: If the user specified a status then modify the nuget install command to get the installer with that status
# Get csAnt setup package
echo "Getting the installer"
echo "(this may take a while as the installer will is being downloaded.... please wait...)"
echo ""

if [ "$STATUS" = "stable" ]; then
    mono $nugetFile install csAnt-setup -Source $sourcePath -OutputDirectory lib -NoCache
else
    mono $nugetFile install csAnt-setup -Source $sourcePath -OutputDirectory lib -NoCache -Pre
fi
echo "Done"
echo ""

# Enter csAnt package dir
cd lib/csAnt-setup.*

# Move the setup file back to the root
echo "Moving the installer to the correct location"
echo ""
cp csAnt-SetUp.exe ../../ -f

# Return to the root dir
cd ../../

# Run the setup file
echo "Launching the installer..."
echo ""
mono csAnt-SetUp.exe "$@"
