//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;

class Test_InitializeScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_InitializeScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
                var initFile = "";
                
                if (IsLinux)
                    initFile = "init-csAnt-project-linux.sh";
                else
                    initFile = "init-csAnt-project-windows.vbs";
	
	        FilesGrabber.GrabOriginalFiles(initFile);
	        
	        CreateNodes();
	        
		InitializeProject();
		
		return !IsError;
	}

}
