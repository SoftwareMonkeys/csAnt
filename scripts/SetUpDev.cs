using System;
using System.IO;
using SoftwareMonkeys.csAnt;

class SetUpDevScript : BaseScript
{
	public static void Main(string[] args)
	{
		new SetUpDevScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var setUpFile = "csAnt-SetUp.exe";

        var arguments = new string[]{
            "-pkg=csAnt-lib"
        };

		return !IsError;
	}
}
