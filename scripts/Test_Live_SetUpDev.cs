//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;
//css_ref ../lib/NUnit.2.6.3/lib/nunit.framework.dll;

using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;
using NUnit.Framework;

class Test_SetUpScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_SetUpScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        new FilesGrabber(
            OriginalDirectory,
            WorkingDirectory
        ).GrabOriginalScriptingFiles();

        ExecuteScript("SetUpDev");

        bool ilRepackIsFound = Directory.GetFiles(
            WorkingDirectory
            + Path.DirectorySeparatorChar
            + "lib"
            + Path.DirectorySeparatorChar
            + "ILRepack.1.25.0"
            + Path.DirectorySeparatorChar
            + "tools"
        ).Length == 1;

        Assert.IsTrue(ilRepackIsFound);

		return !IsError;
	}
}
