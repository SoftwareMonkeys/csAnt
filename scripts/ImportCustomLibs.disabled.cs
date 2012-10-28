// ------------------------------------------------------------------------------------------

// To use this file....

// 1) Rename this file to "ImportCustomLibs.cs" (remove the '.disabled' section of the file name).

// 2) Create a script called "Import[LibName]Lib.cs" for each library to import,
//    such as "ImportMyProjectLib.cs"
//    (Tip: use the "Import[Example]Lib.disabled.cs" file as a template)

// 3) Use the BaseScript.ExecuteScript(...) function (look at the example provided below) to
//    call each 'import library' script

// ------------------------------------------------------------------------------------------

//css_ref ../../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;
using System.Reflection;

class ImportCustomLibs : BaseScript
{
	public static void Main(string[] args)
	{
		new ImportCustomLibs().Start(args);
	}
	
	public void Start(string[] args)
	{
		// TODO: Add custom library imports, using the ExecuteScript function

		// Example:
		//	ExecuteScript("Import[LibName]Lib"); // Exclude the .cs extension
	}
}
