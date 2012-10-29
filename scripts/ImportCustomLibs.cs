//css_ref ../../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;
using System.Reflection;

class ImportCustomLibsScript : BaseScript
{
	public static void Main(string[] args)
	{
		new ImportCustomLibsScript().Start(args);
	}
	
	public void Start(string[] args)
	{
		ExecuteScript("ImportFileNodesLib");
	}
}
