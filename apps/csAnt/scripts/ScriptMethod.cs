//css_ref "SoftwareMonkeys.csAnt.dll";
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
	
	public override bool Run(string[] args)
	{
        // TODO: Add support for method parameters
        // TODO: Add support for specifying a script type
                
		Console.WriteLine("");
		Console.WriteLine("Running script method...");
		Console.WriteLine("");
		
		var methodName = "";
		
		if (args.Length == 1)
		  methodName = args[0];
		  
		var scriptType = GetType();
		
		var methods = scriptType.GetMethods();

		var foundMethod = false;
		
		foreach (var method in methods)
		{
                    if (method.Name == methodName
                        && method.GetParameters().Length == 0)
                    {
                        foundMethod = true;
                        method.Invoke(this, new object[]{});
                    }
                }

                if (!foundMethod)
                    Error("Cannot find a method '" + methodName + "' on the script type '" + scriptType.Name + "' with no parameters.");
                
		return !IsError;
	}
}
