//css_ref "SoftwareMonkeys.csAnt.dll";
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class RaiseEventScript : BaseScript
{
	public static void Main(string[] args)
	{
		new RaiseEventScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var eventName = args[0];

		RaiseEvent(eventName);

		return !IsError;
	}
}
