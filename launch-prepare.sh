SCRIPTS_DIR="scripts";
PREPARE_SCRIPTS_DIR="$SCRIPTS_DIR/prepare/";

echo ""
echo "Downloading prepare scripts..."
echo ""

PREPARE_SCRIPT_1_URL="https://csant.googlecode.com/git/scripts/prepare/prepare.sh";
PREPARE_SCRIPT_1_PATH="$PREPARE_SCRIPTS_DIR/prepare.sh";

PREPARE_SCRIPT_2_URL="https://csant.googlecode.com/git/scripts/prepare/Prepare.cs";
PREPARE_SCRIPT_2_PATH="$PREPARE_SCRIPTS_DIR/Prepare.cs";

# Ensure the directory exists
if [ ! -d "$SCRIPTS_DIR" ]; then
	mkdir $SCRIPTS_DIR
fi

if [ ! -d "$PREPARE_SCRIPTS_DIR" ]; then
	mkdir $PREPARE_SCRIPTS_DIR
fi

# Download script 1
if [ ! -f "$PREPARE_SCRIPT_1_PATH" ]; then
	wget $PREPARE_SCRIPT_1_URL -O $PREPARE_SCRIPT_1_PATH
fi

# Download script 2
if [ ! -f "$PREPARE_SCRIPT_2_PATH" ]; then
	wget $PREPARE_SCRIPT_2_URL -O $PREPARE_SCRIPT_2_PATH
fi

# Launch the prepare script
sh scripts/prepare/prepare.sh
