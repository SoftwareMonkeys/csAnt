arg=$1

echo "Setting up csAnt..."

echo "arg:"
echo $arg

if [ ! -z $arg ]
then :
    csAntDir=$(readlink -f $arg)
else :
    csAntDir=$(readlink -f "../../SoftwareMonkeys/csAnt")
fi

echo "csAnt path:"
echo $csAntDir

if [ -d "$csAntDir" ]; then
    
    echo "Found local csAnt project directory..."
    echo "Copying files..."
        
    cp $csAntDir/bin/Release/packed/csAnt-SetUpFromLocal.exe csAnt-SetUpFromLocal.exe

    mono csAnt-SetUpFromLocal.exe $csAntDir
fi
