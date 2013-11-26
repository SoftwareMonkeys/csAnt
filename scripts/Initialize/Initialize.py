import os
import urllib.request
import Utils

timeStamp = ""
isVerbose = False

def Initialize():

        timeStamp = Utils.GetArgument("t")
        isVerbose = Utils.ContainsArgument("v")
        
        print("Time stamp: " + timeStamp)

        DownloadScripts()

        NextStep()

def NextStep():

        args = ""
        
        if not (timeStamp) == "":
                args = " -t:" + timeStamp
        
        if (isVerbose):
                args = " -v"

        f = os.path.abspath("scripts/Initialize/Initialize2.py" + args)

        exec(open(f).read())


def DownloadScripts():

        scripts = ['GetCSScript.py', 'GetHtmlAgilityPack.py', 'GetLib.py', 'GetRemainingLibs.py', 'GetSharpZipLib.py', 'GetFileNodes.py', 'Initialize2.py', 'Initialize3.cs', 'RunCSScript.py', 'Utils.py']

        for script in scripts:

                print("")
                print("Script: " + script)

                internalScriptPath = os.path.abspath("scripts/Initialize/" + script)

                localScriptPath = Utils.GetOriginalDirectory() + "/../../SoftwareMonkeys/csAnt/scripts/Initialize" + script

                onlineScriptUrl = "https://csant.googlecode.com/git/scripts/Initialize/" + script

                print("Script path: " + internalScriptPath)
                print("Script URL: " + onlineScriptUrl)
                print("Script local file: " + localScriptPath)
        
                if not CheckLocalScript(internalScriptPath, localScriptPath):
                        CheckOnlineScript(internalScriptPath, onlineScriptUrl)

                print("")


def CheckLocalScript( internalScriptPath, localScriptPath ):
        if not os.path.isfile(internalScriptPath):
                print("Grabbing script")

                shutil.copy(localScriptPath, internalScriptPath)
        else:
                
                print("Skipping retrieve")
                
def CheckOnlineScript( scriptPath, scriptUrl ):
        if not os.path.isfile(scriptPath):
                print("Downloading script")

                urllib.request.urlretrieve (scriptUrl, scriptPath)
        else:
                
                print("Skipping download")

Initialize()

