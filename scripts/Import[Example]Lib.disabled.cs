// To use this file:
// 1) Rename from "Import[Example]Lib.disabled.cs" to "ImportMyProjectLib.cs" (and replace MyProject with your project name)
// 2) Use the examples below to import the library

//css_ref ../../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;
using System.Reflection;

class ImportExampleLib : BaseScript
{

	public static void Main(string[] args)
	{
		new ImportExampleLib().Start(args);
	}
	
	public void Start(string[] args)
	{
		// Example:
		// ImportLocalLib("ExampleProject", "Release");
		// ...or...
		// ImportLocalLib("ExampleGroup", "ExampleProject", "Release");

		// TODO: Add support for importing remote library (from the web)
	}


}
