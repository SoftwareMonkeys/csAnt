# Move to the project root
cd ../..

# Set general constants
LIB_DIR="lib"
BASE_DIR=$PWD
SOURCE_PROJECTS_DIR="$HOME/Ubuntu One/Projects"

# Set cs-script constants
CSS_URL="http://www.csscript.net/v3.3.0/cs-script.zip"
CSS_DIR="$LIB_DIR/cs-script"
CSS_ZIPFILE="$LIB_DIR/cs-script.zip"
CSS_FILE="$LIB_DIR/cs-script/cscs.exe"

# Set SharpZipLib constants
SHARPZIPLIB_URL="http://downloads.sourceforge.net/project/sharpdevelop/SharpZipLib/0.86/SharpZipLib_0860_Bin.zip"
SHARPZIPLIB_DIR="$LIB_DIR/SharpZipLib"
SHARPZIPLIB_ZIPFILE="$SHARPZIPLIB_DIR/SharpZipLib_0860_Bin.zip"

# Set Prepare.cs script constants
PREPARE_CS_SCRIPT="scripts/prepare/Prepare.cs"

# ==================================================

# Output some relevant details
echo "Base dir: $BASE_DIR"

echo "Destination: $CSS_ZIPFILE"

# ==================   cs-script   =================

# Make the cs-script directory
if [ ! -d "$CSS_DIR" ]; then
	mkdir $CSS_DIR
fi

echo ""
echo "===== Downloading cs-script ====="

# Download the cs-script libraries
if [ ! -f "$CSS_ZIPFILE" ]; then
	wget $CSS_URL -P $LIB_DIR
fi

echo ""

echo "===== Unzipping the cs-script libraries ====="

# Unzip the cs-script zip file
unzip -o $CSS_ZIPFILE -d $LIB_DIR

# ================   SharpZipLib   =================

# Make the cs-script directory
if [ ! -d "$SHARPZIPLIB_DIR" ]; then
	mkdir $SHARPZIPLIB_DIR
fi

echo ""
echo "===== Downloading SharpZipLib ====="

# Download the SharpZipLib libraries
if [ ! -f "$SHARPZIPLIB_ZIPFILE" ]; then
	wget $SHARPZIPLIB_URL -P $SHARPZIPLIB_DIR
fi

echo "===== Unzipping the SharpZipLib libraries ====="

# Unzip the SharpZipLib zip file
unzip -o $SHARPZIPLIB_ZIPFILE -d $SHARPZIPLIB_DIR

echo ""

echo ""
echo "===== Executing Prepare.cs ====="

# Launch the .cs initialize script
mono $CSS_FILE $PREPARE_CS_SCRIPT "$SOURCE_PROJECTS_DIR"
