using System;
using System.IO;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CycleLiveTestsScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CycleLiveTestsScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Starting a live test cycle.");
		Console.WriteLine("");

        bool skipIncrement = Arguments.Contains("skipincrement");

        // Run a release cycle so the latest binaries and release zips are available
		ExecuteScript(
            "EnsurePackage",
            skipIncrement ? "-skipincrement" : ""
        );

		if (!IsError)
		{
			Console.WriteLine("Running the tests...");
			Console.WriteLine("");

			ExecuteScript("RunLiveTests");
		}

		return !IsError;
	}
}
