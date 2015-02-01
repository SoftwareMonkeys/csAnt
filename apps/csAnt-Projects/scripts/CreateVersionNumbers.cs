//css_ref "SoftwareMonkeys.csAnt.dll";
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
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Creating version numbers...");
		Console.WriteLine("");
                Console.WriteLine("Updated files:");

                var version = new Version("0.0.0.1");

                Console.WriteLine(version.ToString());

                Console.WriteLine(CurrentNode.FilePath);
                
                CurrentNode.Properties["Version"] = version.ToString();
                CurrentNode.Save();

                foreach (var node in CurrentNode.Nodes["Source"].Nodes.Values)
                {
                    node.Properties["Version"] = version.ToString();
                    node.Save();

                    Console.WriteLine(node.FilePath);
                }
		
		AddSummary("Wrote the words 'Hello world!' to the console.");

		// Or
		/*var cmd = new HelloWorldCommand(this);

		ExecuteCommand(cmd);
		*/

		return !IsError;
	}
}
