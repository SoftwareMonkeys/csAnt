import os
import argparse
import sys
import getopt

# IsLinux
def IsLinux():
	return os.sep == "/"

# GetArgument
def GetArgument(arg):
 
        # get argument list using sys module
        sys.argv
        
        # Read command line args
        myopts, args = getopt.getopt(sys.argv[1:],arg+":")
         
        output = ""
        
        fixedArg1 = "-" + arg;
        fixedArg2 = "/" + arg;
         
        for option, value in myopts:
            if option == fixedArg1:
                output=value
            if option == fixedArg2:
                output=value

        output = output.strip().strip(':')
        
        return output
        
# ContainsArgument
def ContainsArgument(arg):
 
        # get argument list using sys module
        sys.argv
        
        # Read command line args
        myopts, args = getopt.getopt(sys.argv[1:],arg+":")
         
        output = ""
        
        fixedArg1 = "-" + arg;
        fixedArg2 = "/" + arg;
         
        for option, value in myopts:
            if option == fixedArg1:
                output=value
            if option == fixedArg2:
                output=value

        output = output.strip().strip(':')
        
        return not (output == "")

def GetOriginalDirectory():
        path = os.getcwd()
        if (path.find('.tmp') != -1):
                while (path.find('.tmp') != -1):
                        path = os.path.dirname(path)
                path = path + os.path.sep + os.path.dirname(os.getcwd())
        return path

