#!/usr/bin/python

import os
import sys
import zipfile
import shutil
import urllib
from GetLib import GetLib

def GetHtmlAgilityPack(libDir, generalLibDir):

	htmlAgilityPackLibDir = os.path.abspath(libDir + os.sep + "/HtmlAgilityPack")
	
	htmlAgilityPackZipUrl = "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qclRRRnNMTGJhalE"

	htmlAgilityPackZipLocal = generalLibDir + "/HtmlAgilityPack/HtmlAgilityPack.zip"
	
	htmlAgilityPackZipInternal = htmlAgilityPackLibDir + "/HtmlAgilityPack.zip"

	GetLib("HtmlAgilityPack", htmlAgilityPackLibDir, htmlAgilityPackZipInternal, htmlAgilityPackZipLocal, htmlAgilityPackZipUrl, generalLibDir)
