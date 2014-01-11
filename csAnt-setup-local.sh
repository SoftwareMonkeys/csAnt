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
        
	cp $csAntDir/bin/Release/SoftwareMonkeys.csAnt.IO.dll lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.IO.dll
	cp $csAntDir/bin/Release/SoftwareMonkeys.csAnt.IO.Contracts.dll lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.IO.Contracts.dll
	cp $csAntDir/bin/Release/SetUpFromLocal.exe lib/csAnt/bin/Release/SetUpFromLocal.exe

	mono lib/csAnt/bin/Release/SetUpFromLocal.exe
 fi
