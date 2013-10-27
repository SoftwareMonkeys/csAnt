#!/usr/bin/python

import os

def GetRemainingLibs():

	print ""
	print "----------------------------------------"
	print "Getting remaining library files by scanning the .node files"
	print ""

	os.system("sh csAnt.sh GetLibs");

	print ""
	print "Done"

	print "----------------------------------------"
	print ""
