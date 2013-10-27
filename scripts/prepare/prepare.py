#!/usr/bin/python

# Imports
import os
from GetCSScript import GetCSScript
from GetSharpZipLib import GetSharpZipLib
from GetHtmlAgilityPack import GetHtmlAgilityPack
from RunCSScript import RunCSScript

# Basic variables
currentDir = os.getcwd()
libDir = currentDir + os.sep + "lib"
sourceProjectsDir = os.path.abspath(currentDir + os.sep + ".." + os.sep)
generalLibDir = os.path.abspath(currentDir + "/../lib")

# Script variables
csPrepareScript = "scripts" + os.sep + "prepare" + os.sep + "Prepare.cs"
createProjectNodeScript = "scripts" + os.sep + "CreateProjectNode.cs"

# ==================================================
# Output header and variable values
# ==================================================

print ""
print ""
print "========================================"
print "Preparing Project"
print "========================================"
print ""
print "The project is being prepared for development, by downloading required files."
print ""

print("Current directory:");
print(currentDir);
print ""

print("Lib dir:");
print(libDir);
print ""

print("Source projects dir:");
print(sourceProjectsDir);
print ""

# ==================================================
# Get library files
# ==================================================

# cs-script
GetCSScript(libDir, generalLibDir)

# SharpZipLib
GetSharpZipLib(libDir, generalLibDir)

# HtmlAgilityPack
GetSharpZipLib(libDir, generalLibDir)

# Launch the Prepare.cs script
RunCSScript(csPrepareScript, sourceProjectsDir)

# Create the project node
RunCSScript(createProjectNodeScript, "")
