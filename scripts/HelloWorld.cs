//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class HelloWorldScript : BaseScript
{
	public static void Main(string[] args)
	{
		new HelloWorldScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		var cmd = Injection.Retriever.Get<HelloWorldCommand>();

		ExecuteCommand(cmd);

		return true;
	}
}
