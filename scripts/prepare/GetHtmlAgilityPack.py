#!/usr/bin/python

import os
import sys
import zipfile
import shutil
import urllib
from GetLib import GetLib

def GetHtmlAgilityPack(libDir, generalLibDir):

	htmlAgilityPackLibDir = os.path.abspath(libDir + os.sep + "/HtmlAgilityPack")
	
	htmlAgilityPackZipUrl = "http://download-codeplex.sec.s-msft.com/Download/Release?ProjectName=htmlagilitypack&DownloadId=437941&FileTime=129893731308330000&Build=19692"
	
	htmlAgilityPackZipLocal = generalLibDir + "/HtmlAgilityPack/HtmlAgilityPack.zip"
	
	htmlAgilityPackZipInternal = htmlAgilityPackLibDir + "/HtmlAgilityPack.zip"

	GetLib("HtmlAgilityPack", htmlAgilityPackLibDir, htmlAgilityPackZipInternal, htmlAgilityPackZipLocal, htmlAgilityPackZipUrl, generalLibDir)
