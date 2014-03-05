csAntDir=$(readlink -f "../../SoftwareMonkeys/csAnt")

echo $csAntDir

if [ -d "$csAntDir" ]; then
    
    echo "Found local csAnt project directory..."
    echo "Copying files..."
        
    cp $csAntDir/bin/Release/packed/csAnt-SetUpFromLocal.exe csAnt-SetUpFromLocal.exe

    mono csAnt-SetUpFromLocal.exe
fi
