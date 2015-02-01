//css_ref "SoftwareMonkeys.csAnt.dll";
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class BackupFileScript : BaseScript
{
	public static void Main(string[] args)
	{
		new BackupFileScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		var file = args[0];

		BackupFile(file);

		return !IsError;
	}
}
