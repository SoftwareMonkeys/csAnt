PrepareScriptFile="/scripts/Initialize/Initialize.py"
PrepareScriptFile=$PWD$PrepareScriptFile
PrepareScriptUrl="https://csant.googlecode.com/git/scripts/Initialize/Initialize.py"

if [ ! -f $PrepareScriptFile ]; then

	mkdir $PWD"/scripts"

	mkdir $PWD"/scripts/Initialize"

	wget -O $PrepareScriptFile $PrepareScriptUrl
fi

python $PrepareScriptFile
