import os
import Utils

def RunCSScript( scriptPath, args ):

        csScriptExeFile = "lib" + os.sep + "cs-script" + os.sep + "cscs.exe"

        command = ""

        # If running on linux, use mono to launch the cs-script program
        if (Utils.IsLinux()):
                command += "mono "

        command += csScriptExeFile + " " + scriptPath

        if not (args == ""):
                command += " " + args
        
        print("")
        print("Launching script via cs-script:")
        print(scriptPath)
        print("Command:")
        print(command)
        print("")

        os.system(command)
