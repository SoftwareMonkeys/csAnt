#!/usr/bin/python

import os
import Utils

def GetRemainingLibs():

	print("")
	print("----------------------------------------")
	print("Getting remaining library files by scanning the .node files")
	print("")

        if (Utils.IsLinux():
                os.system("sh csAnt.sh GetLibs")
        else
                os.system("cscript csAnt.vbs GetLibs")

	print("")
	print("Done")

	print("----------------------------------------")
	print("")
