#!/usr/bin/python

import os
import sys
import zipfile
import shutil
import urllib

def GetLib(name, libDir, zipInternal, zipLocal, zipUrl, generalLibDir):

	print("")
	print("----------")
	print("Getting " + name + " library files")
	print("")

	# Output constants
	print(name + " Lib Dir:")
	print(libDir)
	print("")

	print(name + " Lib Zip Local:")
	print(zipLocal)
	print("")

	print(name + " Lib Zip Internal:")
	print(zipInternal)
	print("")

	print(name + " Zip URL:")
	print(zipUrl)
	print("")

	# If the lib directory doesn't exist then create it
	if not os.path.isdir(libDir):
		os.makedirs(libDir)

	# If the internal css-scrip zip file isn't found try getting it from the local path
	if not (os.path.exists(zipInternal)):
		GetLibFromLocal(zipLocal, zipInternal)

	# If the internal css-script zip file still isn't found try getting it from the URL
	if not (os.path.exists(zipInternal)):
		GetLibFromUrl(zipUrl, zipInternal)

	# Extract the files from the internal zip file
	ExtractInternalZipFile( zipInternal, libDir )

	print(name + " library files have been retrieved.")

	print("----------")
	print("")

def GetLibFromLocal( zipLocal, zipInternal ):
	print("")
	print("Getting lib from local location...")
	print("")

	if os.path.exists(zipLocal):
		print("Copying from:")
		print(zipLocal)
		print("To:")
		print(zipInternal)
		print("")

		shutil.copyfile(zipLocal, zipInternal)
	else:
		print("Local zip file not found:")
		print(zipLocal)
		print("")

def GetLibFromUrl( zipUrl, zipInternal ):
	print("")
	print("Getting lib from URL...")
	print("")

	print("Downloading from:")
	print(zipUrl)
	print("To:")
	print(zipInternal)
	print("")

	urllib.urlretrieve (zipUrl, zipInternal)

def ExtractInternalZipFile( zipInternal, libDir ):
	print("")
	print("Extracting files from:")
	print(zipInternal)
	print("To:")
	print(libDir)
	print("")


	zip = zipfile.ZipFile(zipInternal)
	zip.extractall(libDir)
