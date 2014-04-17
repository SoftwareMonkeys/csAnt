sourcePath=$1

echo "Setting up csAnt..."

if [ ! -z $sourcePath ]
then :
    csAntDir=$(readlink -f $sourcePath)
else :
    csAntDir=$(readlink -f "../../SoftwareMonkeys/csAnt")
fi

echo "csAnt path:"
echo $csAntDir

if [ -d "$csAntDir" ]; then
    
    echo "Found local csAnt project directory..."
    echo "Copying files..."
        
    cp $csAntDir/bin/Release/packed/csAnt-SetUpFromLocal.exe csAnt-SetUpFromLocal.exe -f

    mono csAnt-SetUpFromLocal.exe $csAntDir "$@"
fi