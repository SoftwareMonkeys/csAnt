//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.Tests.dll";
//css_ref "nunit.framework.dll";

using System;
using System.IO;
using System.Collections.Generic;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;

class RunScriptTestsScript : BaseScript
{
	public static void Main(string[] args)
	{
		new RunScriptTestsScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        // Run tests
        RunTests();

        return !IsError;
	}
	
	public void RunTests()
	{
        var succeededList = new List<string>();
        var failedList = new List<string>();

        foreach (var script in FindScripts("Test_*"))
        {
            try
            {
                ExecuteScript(script);
                succeededList.Add(script);
            }
            catch (Exception ex)
            {
                Console.WriteLine(script + " failed...");
                Console.WriteLine(ex.ToString());

                failedList.Add(script);
            }
        }

        Console.WriteLine("");
        Console.WriteLine("Succeeded:");
        foreach (var successfulScript in succeededList)
        {
            Console.WriteLine(successfulScript);
        }

        Console.WriteLine("");
        Console.WriteLine("Failed:");
        foreach (var failedScript in failedList)
        {
            Console.WriteLine(failedScript);
        }
	}
}
