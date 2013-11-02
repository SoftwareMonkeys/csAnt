import os
import urllib
import urllib.request

def Initialize():

        DownloadScripts()

        NextStep()

def NextStep():

        f = os.path.abspath("scripts/Initialize/Initialize2.py")

        exec(open(f).read())


def DownloadScripts():

        scripts = ['GetCSScript.py', 'GetHtmlAgilityPack.py', 'GetLib.py', 'GetRemainingLibs.py', 'GetSharpZipLib.py', 'Initialize2.py', 'Initialize3.cs', 'RunCSScript.py', 'Utils.py']

        for script in scripts:

                print("")
                print("Script: " + script)

                scriptPath = os.path.abspath("scripts/Initialize/" + script)
                scriptUrl = "https://csant.googlecode.com/git/scripts/Initialize/" + script

                print("Script path: " + scriptPath)
                print("Script URL: " + scriptUrl)
        
                CheckScript(scriptPath,scriptUrl)

                print("")


def CheckScript( scriptPath, scriptUrl ):
        print("Checking script:");
        print(scriptPath);

        if not os.path.isfile(scriptPath):
                print("Downloading script")
                
                # urllib.request.urlretrieve (scriptUrl, scriptPath)
                
                fp = urllib.request.urlopen(scriptUrl)
                with open(scriptPath, "w") as fo:
                    fo.write(fp.read())
        else:
                
                print("Skipping download")

Initialize()

