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
        
        fileNodesLibZipUrl = "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qalFoR2ZpdXN3alk"
        
        fileNodesLibZipLocal = generalLibDir + "/FileNodes/FileNodes.zip"
        
        fileNodesLibZipInternal = fileNodesLibDir + "/FileNodes.zip"

        GetLib("FileNodes", fileNodesLibDir, fileNodesLibZipInternal, fileNodesLibZipLocal, fileNodesLibZipUrl, generalLibDir)
