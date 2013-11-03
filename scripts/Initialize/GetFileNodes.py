#!/usr/bin/python

import os
import sys
import zipfile
import shutil
import urllib
from GetLib import GetLib

def GetFileNodes(libDir, generalLibDir):


        # Set constants
        fileNodesLibDir = os.path.abspath(libDir + os.sep + "/FileNodes")
        
        fileNodesLibZipUrl = "https://filenodes.googlecode.com/files/FileNodes-bin-%5B2012-11-14--9-17-20%5D.zip"
        
        fileNodesLibZipLocal = generalLibDir + "/FileNodes/FileNodes.zip"
        
        fileNodesLibZipInternal = fileNodesLibDir + "/FileNodes.zip"

        GetLib("FileNodes", fileNodesLibDir, fileNodesLibZipInternal, fileNodesLibZipLocal, fileNodesLibZipUrl, generalLibDir)
