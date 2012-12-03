# Set general constants
BASE_DIR=$PWD
LIB_DIR="lib"
SOURCE_PROJECTS_DIR="$HOME/Ubuntu One/Projects"

# Set cs-script constants
CSS_URL="http://www.csscript.net/v3.3.0/cs-script.zip"
CSS_DIR="$LIB_DIR/cs-script"
CSS_ZIPFILE="$LIB_DIR/cs-script.zip"
CSS_FILE="$CSS_DIR/cscs.exe"

# Set SharpZipLib constants
SHARPZIPLIB_URL="http://downloads.sourceforge.net/project/sharpdevelop/SharpZipLib/0.86/SharpZipLib_0860_Bin.zip"
SHARPZIPLIB_DIR="$LIB_DIR/SharpZipLib"
SHARPZIPLIB_ZIPFILE="$SHARPZIPLIB_DIR/SharpZipLib_0860_Bin.zip"

# Set HtmlAgilityPack constants
HAP_LIB_URL="http://download-codeplex.sec.s-msft.com/Download/Release?ProjectName=htmlagilitypack&DownloadId=437941&FileTime=129893731308330000&Build=19692"
HAP_LIB_DIR="$LIB_DIR/HtmlAgilityPack"
HAP_LIB_ZIPFILE="$HAP_LIB_DIR/HtmlAgilityPack.zip"

# Set Prepare.cs script constants
PREPARE_CS_SCRIPT="scripts/prepare/Prepare.cs"

# Import Libraries constants
IMPORT_LIBS_SCRIPT="GetLibs"

# csAnt constants
CSANT_FILE="$LIB_DIR/csAnt/bin/Release/csAnt.exe"


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
	mkdir $LIB_DIR
fi

# ==================   cs-script   =================


# Make the cs-script directory
if [ ! -d "$CSS_DIR" ]; then
	mkdir $CSS_DIR
fi


# Download the cs-script libraries

echo ""
echo "===== Downloading cs-script ====="

if [ ! -f "$CSS_ZIPFILE" ]; then

	wget $CSS_URL -O $CSS_ZIPFILE

fi


# Unzip the cs-script zip file

echo ""
echo "===== Unzipping the cs-script libraries ====="

unzip -o $CSS_ZIPFILE -d $LIB_DIR


# ================   SharpZipLib   =================


# Make the SharpZipLib directory
if [ ! -d "$SHARPZIPLIB_DIR" ]; then
	mkdir $SHARPZIPLIB_DIR
fi


# Download the SharpZipLib libraries

echo ""
echo "===== Downloading SharpZipLib ====="

if [ ! -f "$SHARPZIPLIB_ZIPFILE" ]; then
	wget $SHARPZIPLIB_URL -O $SHARPZIPLIB_ZIPFILE
fi


# Unzip the SharpZipLib zip file

echo ""
echo "===== Unzipping the SharpZipLib libraries ====="

unzip -o $SHARPZIPLIB_ZIPFILE -d $SHARPZIPLIB_DIR


# ================   HtmlAgilityPack   =================


# Make the HtmlAgilityPack directory
if [ ! -d "$HAP_LIB_DIR" ]; then
	mkdir $HAP_LIB_DIR
fi


# Download the HtmlAgilityPack libraries

echo ""
echo "===== Downloading HtmlAgilityPack ====="

if [ ! -f "$HAP_LIB_ZIPFILE" ]; then
	wget $HAP_LIB_URL -O $HAP_LIB_ZIPFILE
fi


# Unzip the HtmlAgilityPack zip file

echo ""
echo "===== Unzipping the HtmlAgilityPack libraries ====="

unzip -o "$HAP_LIB_ZIPFILE" -d $HAP_LIB_DIR

echo "To: $HAP_LIB_DIR" 

# ================   csAnt   =================

# Launch the .cs initialize script

echo ""
echo "===== Executing Prepare.cs ====="

mono $CSS_FILE $PREPARE_CS_SCRIPT "$SOURCE_PROJECTS_DIR"


# ================   Create Project Node   =================
# Create the project node

# Launch the script via csAnt
CREATE_PROJECT_NODE_SCRIPT="scripts/CreateProjectNode.cs"

mono $CSS_FILE $CREATE_PROJECT_NODE_SCRIPT


# ================   Import Libraries via csAnt   =================
# Now csAnt is installed and ready to use

# Launch the import libs script via csAnt
mono $CSANT_FILE $IMPORT_LIBS_SCRIPT



echo ""
echo "===== Preparation completed successfully! ====="
echo ""
echo "You can now run scripts via the csAnt.exe console application. (Pass the script name as the first parameter.)"
echo ""
echo ""
