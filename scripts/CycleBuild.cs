//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class CycleBuildScript : BaseScript
{
	public static void Main(string[] args)
	{
		new CycleBuildScript().Start();
	}
	
	public void Start()
	{
		Console.WriteLine("");
		Console.WriteLine("Starting a full build cycle.");
		Console.WriteLine("");

		Console.WriteLine("Building...");
		Console.WriteLine("");

		// Build the solution
		ExecuteScript(
			"BuildSolution",
			new string[]{
				ProjectName + ".MonoDevelop"
			}
		);

		if (!IsError)
		{
			// Copy the binaries to the libraries folder
			ExecuteScript(
				"CopyBinToLib"
			);
		}
		
	}
}
