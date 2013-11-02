import os
import urllib.request

def Initialize():

        DownloadScripts()

        NextStep()

def NextStep():

        f = os.path.abspath("scripts/Initialize/Initialize2.py")

        exec(open(f).read())


def DownloadScripts():

<<<<<<< HEAD
        scripts = ['GetCSScript.py', 'GetHtmlAgilityPack.py', 'GetLib.py', 'GetRemainingLibs.py', 'GetSharpZipLib.py', 'Initialize2.py', 'Initialize3.cs', 'RunCSScript.py', 'Utils.py']
=======
        scripts = ['GetCSScript.py', 'GetHtmlAgilityPack.py', 'GetLib.py', 'GetRemainingLibs.py', 'GetSharpZipLib.py', 'Initialize2.py', 'Initialize3.cs', 'RunCSScript.py', 'Utils']
>>>>>>> 71e08859eaf962c1c2976cc842f589a1df08168a

        for script in scripts:

                print("")
                print("Script: " + script)

<<<<<<< HEAD
                scriptPath = os.path.abspath("scripts/Initialize/" + script)
=======
                scriptPath = os.path.abspath("scripts/Initialize/" + script + ".py")
>>>>>>> 71e08859eaf962c1c2976cc842f589a1df08168a
                scriptUrl = "https://csant.googlecode.com/git/scripts/Initialize/" + script

                print("Script path: " + scriptPath)
                print("Script URL: " + scriptUrl)
        
                CheckScript(scriptPath,scriptUrl)

                print("")


def CheckScript( scriptPath, scriptUrl ):
        if not os.path.isfile(scriptPath):
                print("Downloading script")
<<<<<<< HEAD
                
                # urllib.request.urlretrieve (scriptUrl, scriptPath)
                
=======
>>>>>>> 71e08859eaf962c1c2976cc842f589a1df08168a
                fp = urllib.request.urlopen(scriptUrl)
                with open(scriptPath, "w") as fo:
                    fo.write(fp.read())
        else:
                
                print("Skipping download")

Initialize()

