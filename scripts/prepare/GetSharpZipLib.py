#!/usr/bin/python

import os
import sys
import zipfile
import shutil
import urllib
from GetLib import GetLib

def GetSharpZipLib(libDir, generalLibDir):


	# Set constants
	sharpZipLibDir = os.path.abspath(libDir + os.sep + "/SharpZipLib")
	
	sharpZipLibZipUrl = "http://downloads.sourceforge.net/project/sharpdevelop/SharpZipLib/0.86/SharpZipLib_0860_Bin.zip"
	
	sharpZipLibZipLocal = generalLibDir + "/SharpZipLib/SharpZipLib.zip"
	
	sharpZipLibZipInternal = sharpZipLibDir + "/SharpZipLib.zip"

	GetLib("SharpZipLib", sharpZipLibDir, sharpZipLibZipInternal, sharpZipLibZipLocal, sharpZipLibZipUrl, generalLibDir)
