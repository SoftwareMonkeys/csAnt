using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class ExecuteScriptTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_ExecuteScriptFromFile()
		{
			var script = GetScriptCode();

			var scriptFile = Path.GetTempPath()
				+ Path.DirectorySeparatorChar
				+ "HelloWorld.cs";

			File.WriteAllText(
				scriptFile,
				script
			);

			var testScript = new TestScript("HelloWorld", this);

			testScript.ExecuteScriptFromFile(scriptFile, new string[]{});

			var success = !testScript.IsError;

			Assert.AreEqual(true, success);
		}

		private string GetScriptCode()
		{
			return @"//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
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
		Console.WriteLine(""Hello world"");

		return true;
	}
}";
		}

	}
}

