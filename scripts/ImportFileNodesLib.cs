//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;
using System.Reflection;

class ImportFileNodesLib : BaseScript
{

	public static void Main(string[] args)
	{
		new ImportFileNodesLib().Start(args);
	}
	
	public void Start(string[] args)
	{
		ImportLocalLib("FileNodes", "Release");

		// TODO: Add support for importing remote library (from the web)
	}


}
