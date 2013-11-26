InitScriptFile="scripts/Initialize/Initialize.py"
InitScriptFileUrl="https://csant.googlecode.com/git/scripts/Initialize/Initialize.py"

BaseDir=$PWD
ProjectsDir=$BaseDir

echo "Current directory:"
echo $BaseDir

if [ "$(echo $ProjectsDir | grep '.tmp')" != "" ]; then
        while true; do
                     echo $ProjectsDir
                     
                     if [ "$(echo $ProjectsDir | grep '.tmp')" = "" ]; then
                        exit
                     fi
                     if [ "$(echo $ProjectsDir | grep '.tmp')" != "" ]; then
                        ProjectsDir=$(dirname $ProjectsDir)
                     fi
        done
fi
ProjectsDir=$(dirname $ProjectsDir)
ProjectsDir=$(dirname $ProjectsDir)

echo "Projects dir:"
echo $ProjectsDir

InitScriptFileInternal=$BaseDir/$InitScriptFile

CsAntProjectDir="$ProjectsDir/SoftwareMonkeys/csAnt"

InitScriptFileLocal=$CsAntProjectDir/$InitScriptFile

echo "Initialize script file (internal):"
echo $InitScriptFileInternal
echo ""

echo "Initialize script file (local):"
echo $InitScriptFileLocal
echo ""

echo "Initialize script file (URL):"
echo $InitScriptFileUrl
echo ""
        
if [ ! -f $InitScriptFileInternal ]; then
        
	mkdir "$BaseDir/scripts"

	mkdir "$BaseDir/scripts/Initialize"

        if [ -f $InitScriptFileLocal ]; then
                echo "Copying initialize script from local location..."
        
                cp $InitScriptFileLocal $InitScriptFileInternal
        else
                echo "Downloading initialize script..."
                wget -O $InitScriptFileInternal $InitScriptFileUrl
        fi
fi

python3 $InitScriptFileInternal %1 %2 %3 %4 %5
