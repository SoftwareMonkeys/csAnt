//css_ref "SoftwareMonkeys.csAnt.dll";
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

[ExpectedArgument("name")]
[ExpectedArgument("url")]
class AddLibScript : BaseScript
{
	public static void Main(string[] args)
	{
		new AddLibScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		if (args.Length < 2)
			Console.WriteLine("Two arguments expected; name and URL (to zip file).");
		else
		{
            var type = Arguments["t"];

            switch (type.ToLower())
            {
                case "url":
                    AddLibUrl();
                    break;
                case "nuget":
                    AddLibNuget();
                    break;
            }
		}

		return !IsError;
	}

    public void AddLibUrl()
    {
        var name = Arguments["name"];

        var url = Arguments["url"];

        var subPath = Arguments["subpath"];

        AddLibUrl(name, url, subPath);
    }

    public void AddLibNuget()
    {
        var name = Arguments["name"];

        var package = Arguments["package"];

        AddLibNuget(name, package);
    }
}
