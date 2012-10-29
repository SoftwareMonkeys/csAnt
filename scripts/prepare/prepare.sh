# Set general constants
BASE_DIR=$PWD
LIB_DIR="lib"
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

echo ""
echo ""
echo "========================================"
echo "Preparing Project"
echo "========================================"
echo ""
echo "The project is being prepared for development, by downloading required libraries, etc."
echo ""

echo "Base dir: $BASE_DIR"


# ==================   lib directory   =================

# Make the lib directory
if [ ! -d "$LIB_DIR" ]; then
	mkdir "\"$LIB_DIR\""
fi

# ==================   cs-script   =================


# Make the cs-script directory
if [ ! -d "$CSS_DIR" ]; then
	mkdir "\"$CSS_DIR\""
fi


# Download the cs-script libraries

echo ""
echo "===== Downloading cs-script ====="

if [ ! -f "$CSS_ZIPFILE" ]; then
	wget $CSS_URL -P $LIB_DIR
fi


# Unzip the cs-script zip file

echo ""
echo "===== Unzipping the cs-script libraries ====="

unzip -o $CSS_ZIPFILE -d $LIB_DIR


# ================   SharpZipLib   =================


# Make the cs-script directory
if [ ! -d "$SHARPZIPLIB_DIR" ]; then
	mkdir $SHARPZIPLIB_DIR
fi


# Download the SharpZipLib libraries

echo ""
echo "===== Downloading SharpZipLib ====="

if [ ! -f "$SHARPZIPLIB_ZIPFILE" ]; then
	wget $SHARPZIPLIB_URL -P $SHARPZIPLIB_DIR
fi


# Unzip the SharpZipLib zip file

echo ""
echo "===== Unzipping the SharpZipLib libraries ====="

unzip -o $SHARPZIPLIB_ZIPFILE -d $SHARPZIPLIB_DIR


# ================   csAnt   =================

# Launch the .cs initialize script

echo ""
echo "===== Executing Prepare.cs ====="

mono $CSS_FILE $PREPARE_CS_SCRIPT "$SOURCE_PROJECTS_DIR"

# ================   Import Libraries via csAnt   =================
# Now csAnt is installed and ready to use

# Launch the import libs script via csAnt
IMPORT_LIBS_SCRIPT="ImportLibs"
CSANT_FILE="$LIB_DIR/csAnt/bin/Release/csAnt.exe"

mono $CSANT_FILE $IMPORT_LIBS_SCRIPT

echo ""
echo "===== Preparation completed successfully! ====="
echo ""
echo "You can now run scripts via the csAnt.exe console application. (Pass the script name as the first parameter.)"
echo ""
echo ""
