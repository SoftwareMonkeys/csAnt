csAntDir=$(readlink -f "../../SoftwareMonkeys/csAnt")

echo $csAntDir

if [ -d "$csAntDir" ]; then
    
    echo "Found local csAnt project directory..."
    echo "Copying files..."
    
    csAntLibDir=lib/csAnt/bin/Release/
    
    if [ ! -d "csAntLibDir" ]; then
        mkdir lib/
        mkdir lib/csAnt/
        mkdir lib/csAnt/bin/
        mkdir $csAntLibDir
    fi
        
        cp $csAntDir/bin/Release/SetUpFromLocal.exe lib/csAnt/bin/Release/SetUpFromLocal.exe
        cp $csAntDir/bin/Release/SoftwareMonkeys.csAnt.dll lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll
        cp $csAntDir/bin/Release/SoftwareMonkeys.csAnt.Contracts.dll lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Contracts.dll
        
        mono lib/csAnt/bin/Release/SetUpFromLocal.exe
 fi