#!/usr/bin/python

import os
import sys
import zipfile
import shutil
import urllib
from GetLib import GetLib

def GetCSScript(libDir, generalLibDir):

        cssLibDir = os.path.abspath(libDir + os.sep + "/cs-script")
        
        cssZipUrl = "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qQkRIbFpGRTB4WW8"
        
        cssZipLocal = generalLibDir + "/cs-script/cs-script.zip"
        
        cssZipInternal = cssLibDir + "/cs-script.zip"

        GetLib("cs-script", cssLibDir, cssZipInternal, cssZipLocal, cssZipUrl, generalLibDir)
