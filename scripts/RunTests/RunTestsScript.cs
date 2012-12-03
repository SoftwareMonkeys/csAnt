//css_ref ../../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;

class RunTestsScript : BaseScript
{
	public static void Main(string[] args)
	{
		new RunTestsScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		var parser = new Arguments(args);

		var dateStamp = GetDateStamp();

		EnsureDirectories(dateStamp);

		var mode = "Release";

		if (parser.Contains("mode"))
			mode = parser["mode"];

		RunTests(
			dateStamp,
			mode
		);

		GenerateReports(
			dateStamp
		);

		return !IsError;
	}

	public void RunTests(string dateStamp, string mode)
	{
		var binPath = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "bin"
			+ Path.DirectorySeparatorChar
			+ mode;

		Console.WriteLine("Test assemblies found:");

		List<string> executedAssemblies = new List<string>();

		foreach (string assemblyFile in Directory.GetFiles(binPath, "*.Tests.dll", SearchOption.AllDirectories))
		{
			var assemblyFileName = Path.GetFileName(assemblyFile);

			if (!executedAssemblies.Contains(assemblyFileName))
			{
				executedAssemblies.Add(
					assemblyFileName
				);
				
				Console.WriteLine(assemblyFile);

				RunAssemblyTests(dateStamp, assemblyFile);
			}
		}
	}

	public void RunAssemblyTests(string dateStamp, string assemblyFile)
	{
		string assemblyFileName = Path.GetFileName(assemblyFile);

		string xmlResult = GetXmlResultDir(dateStamp)
			+ Path.DirectorySeparatorChar
			+ Path.GetFileNameWithoutExtension(assemblyFileName).Replace(".", "-")
			+ ".xml";

			string command = "mono";

			List<string> arguments = new List<string>();

			arguments.Add("--runtime=v4.0");

			arguments.Add("lib/NUnit/bin/nunit-console.exe");

			arguments.Add("\"" + assemblyFile + "\"");

			arguments.Add("-xml=\"" + xmlResult + "\"");

			StartProcess(
				command,
				arguments.ToArray()
			);				

	}

	public void GenerateReports(string dateStamp)
	{
		var xmlResultDir = GetXmlResultDir(dateStamp)
			+ Path.DirectorySeparatorChar
			+ "*";

		string htmlResultDir = GetHtmlResultDir(dateStamp);
			
		List<string> arguments = new List<string>();

		arguments.Add("lib/NUnitResults/nunit-results.exe");

		arguments.Add("\"" + xmlResultDir + "\"");

		arguments.Add("\"" + htmlResultDir + "\"");

		Execute(
			"mono",
			arguments.ToArray()
		);
	}

	public void EnsureDirectories(string dateStamp)
	{
		var xmlResultDir = GetXmlResultDir(dateStamp);

		if (!Directory.Exists(xmlResultDir))
			Directory.CreateDirectory(xmlResultDir);

		var htmlResultDir = GetHtmlResultDir(dateStamp);

		if (!Directory.Exists(htmlResultDir))
			Directory.CreateDirectory(htmlResultDir);

	}

	public string GetResultDir(string dateStamp)
	{
		string resultDir = Path.GetFullPath(
			String.Format(
				"{0}/tests/results/{1}",
				CurrentDirectory,
				dateStamp
			)
		);

		return resultDir;
	}

	public string GetXmlResultDir(string dateStamp)
	{
		return GetResultDir(dateStamp)
			+ Path.DirectorySeparatorChar
			+ "xml";
	}

	public string GetHtmlResultDir(string dateStamp)
	{
		return GetResultDir(dateStamp)
			+ Path.DirectorySeparatorChar
			+ "html";
	}

	public string GetDateStamp()
	{
		return DateTime.Now.Year
			+ "-"
			+ DateTime.Now.Month
			+ "-"
			+ DateTime.Now.Day
			+ "--"
			+ DateTime.Now.Hour
			+ "-"
			+ DateTime.Now.Minute
			+ "-"
			+ DateTime.Now.Second;
	}
}
