#!/usr/bin/python

# Imports
import os
from GetCSScript import GetCSScript
from GetSharpZipLib import GetSharpZipLib
from GetHtmlAgilityPack import GetHtmlAgilityPack
from RunCSScript import RunCSScript
from GetRemainingLibs import GetRemainingLibs
from GetFileNodes import GetFileNodes
import Utils

# Basic variables
timeStamp = ""
currentDir = os.getcwd()
libDir = currentDir + os.sep + "lib"
sourceProjectsDir = os.path.dirname(Utils.GetOriginalDirectory())
generalLibDir = os.path.dirname(Utils.GetOriginalDirectory()) + os.path.sep + "lib"

# Script variables
csInitializeScript = "scripts" + os.sep + "Initialize" + os.sep + "Initialize3.cs"
createProjectNodeScript = "scripts" + os.sep + "CreateProjectNode.cs"

# ==================================================
# Output header and variable values
# ==================================================

print("")
print("")
print("// --------------------")
print("// Initializing Project")
print("// --------------------")
print("")
print("The project is being initialized ready for development, by downloading required files.")
print("")

timeStamp = Utils.GetArgument("t")
isVerbose = Utils.ContainsArgument("v")

print("Time stamp:");
print(timeStamp);
print("")

print("Is verbose");
print(isVerbose);
print("")

print("Current directory:");
print(currentDir);
print("")

print("Lib dir:");
print(libDir);
print("")

print("Source projects dir:");
print(sourceProjectsDir);
print("")

# ==================================================
# Get library files
# ==================================================

# cs-script
GetCSScript(libDir, generalLibDir)

# SharpZipLib
GetSharpZipLib(libDir, generalLibDir)

# HtmlAgilityPack
GetHtmlAgilityPack(libDir, generalLibDir)

# FileNodes
GetFileNodes(libDir, generalLibDir)

# Launch the Initialize.cs script
initArgs = sourceProjectsDir;
if (isVerbose):
        initArgs = initArgs + " -v"

initArgs = initArgs + " -t:" + timeStamp
RunCSScript(csInitializeScript, initArgs)

# Create the project node
RunCSScript(createProjectNodeScript, "")

# Get the remaining libs by checking the .node files in the /libs/ directory
GetRemainingLibs()



print("")
print("----------------------------------------")
print("Initialization complete!")
print("----------------------------------------")
print("")
print("You can now run scripts via csAnt.")
print("")
print("Example:")
if (Utils.IsLinux()):
        print("sh csAnt.sh HelloWorld")
else:
        print("csAnt.bat HelloWorld")
print("")
