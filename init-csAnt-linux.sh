PrepareScriptFile="scripts/Initialize/Initialize.py"
PrepareScriptUrl="https://csant.googlecode.com/git/scripts/Initialize/Initialize.py"

if [ ! -f $PrepareScriptFile ]; then

	mkdir "scripts"

	mkdir "scripts/prepare"

	wget -O $PrepareScriptFile $PrepareScriptUrl
fi

python $PrepareScriptFile
