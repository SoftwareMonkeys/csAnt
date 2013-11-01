InitScriptFile="/scripts/Initialize/Initialize.py"
InitScriptFile=$PWD$InitScriptFile
InitScriptUrl="https://csant.googlecode.com/git/scripts/Initialize/Initialize.py"

if [ ! -f $InitScriptFile ]; then

	mkdir $PWD"/scripts"

	mkdir $PWD"/scripts/Initialize"

	wget -O $InitScriptFile $InitScriptUrl
fi

python $InitScriptFile
