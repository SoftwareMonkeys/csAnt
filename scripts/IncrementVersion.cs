using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Versions;

class IncrementVersionScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new IncrementVersionScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        if (args.Length > 0)
            IncrementVersion(Convert.ToInt32(args[0]));
        else
    	    IncrementVersion();
         
		return !IsError;
	}
}
