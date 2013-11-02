import os
import urllib
import urllib.request

def Initialize():

        DownloadScripts()

        NextStep()

def NextStep():

        os.system(os.path.abspath("scripts/Initialize/Initialize2.py"))

def DownloadScripts():

        scripts = ['GetCSScript.py', 'GetHtmlAgilityPack.py', 'GetLib.py', 'GetRemainingLibs.py', 'GetSharpZipLib.py', 'Initialize2.py', 'Initialize3.cs', 'RunCSScript.py', 'Utils.py']

        for script in scripts:

                print("")
                print("Script: " + script)

                scriptPath = os.path.abspath("scripts/Initialize/" + script + ".py")
                scriptUrl = "https://csant.googlecode.com/git/scripts/Initialize/" + script

                print("Script path: " + scriptPath)
                print("Script URL: " + scriptUrl)
        
                CheckScript(scriptPath,scriptUrl)

                print("")


def CheckScript( scriptPath, scriptUrl ):
        if not os.path.isfile(scriptPath):
                print("Downloading script")
                urllib.request.urlretrieve (scriptUrl, scriptPath)
        else:
                
                print("Skipping download")

Initialize()

