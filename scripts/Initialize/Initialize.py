import os
import urllib

def Initialize():

        DownloadScripts()

        NextStep()

def NextStep():

        execfile(os.path.abspath("scripts/Initialize/Initialize2.py"))

def DownloadScripts():

        scripts = ['GetCSScript', 'GetHtmlAgilityPack', 'GetLib', 'GetRemainingLibs', 'GetSharpZipLib', 'Initialize2', 'Initialize3', 'RunCSScript', 'Utils']

        for script in scripts:

                print("")
                print("Script: " + script)

                scriptPath = os.path.abspath("scripts/Initialize/" + script + ".py")
                scriptUrl = "https://csant.googlecode.com/git/scripts/Initialize/" + script + ".py"

                print("Script path: " + scriptPath)
                print("Script URL: " + scriptUrl)
        
                CheckScript(scriptPath,scriptUrl)

                print("")


def CheckScript( scriptPath, scriptUrl ):
        print("Checking script:");
        print(scriptPath);

        if not os.path.isfile(scriptPath):
                print("Downloading script")
                urllib.request.urlretrieve (scriptUrl, scriptPath)
        else:
                
                print("Skipping download")

Initialize()

