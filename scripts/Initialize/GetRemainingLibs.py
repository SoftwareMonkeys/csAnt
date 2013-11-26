#!/usr/bin/python

import os
import Utils

def GetRemainingLibs():

        print("")
        print("----------------------------------------")
        print("Getting remaining library files by scanning the .node files...")
        print("Current directory:")
        print(os.path.getcwd())
        print("")

        if (Utils.IsLinux()):
                print("Running on linux. Using 'sh csAnt.sh GetLibs' command.")
                os.system("sh csAnt.sh GetLibs")
        else:
                print("Running on windows. Using 'csAnt.bat GetLibs' command.")
                os.system("csAnt.bat GetLibs")

        print("")
        print("Done")

        print("----------------------------------------")
        print("")
