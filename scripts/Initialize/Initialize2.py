#!/usr/bin/python

# Imports
import os
from GetCSScript import GetCSScript
from GetSharpZipLib import GetSharpZipLib
from GetHtmlAgilityPack import GetHtmlAgilityPack
from RunCSScript import RunCSScript
from GetRemainingLibs import GetRemainingLibs

# Basic variables
currentDir = os.getcwd()
libDir = currentDir + os.sep + "lib"
sourceProjectsDir = os.path.abspath(currentDir + os.sep + ".." + os.sep)
generalLibDir = os.path.abspath(currentDir + "/../lib")

# Adjust basic variables
if ".tmp" in sourceProjectsDir: # If .tmp is found in the path then adjust the source projects directory
	sourceProjectsDir = os.path.abspath("../../..")

# Script variables
csPrepareScript = "scripts" + os.sep + "prepare" + os.sep + "Prepare.cs"
createProjectNodeScript = "scripts" + os.sep + "CreateProjectNode.cs"

# ==================================================
# Output header and variable values
# ==================================================

print ""
print ""
print "// --------------------"
print "// Preparing Project"
print "// --------------------"
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
GetHtmlAgilityPack(libDir, generalLibDir)

# Launch the Prepare.cs script
RunCSScript(csPrepareScript, sourceProjectsDir)

# Create the project node
RunCSScript(createProjectNodeScript, "")

# Get the remaining libs by checking the .node files in the /libs/ directory
GetRemainingLibs()



print ""
print "----------------------------------------"
print "Preparation completed successfully!"
print "----------------------------------------"
print ""
print "You can now run scripts via csAnt."
print ""
print "Example:"
print "sh csAnt.sh HelloWorld"
print ""
