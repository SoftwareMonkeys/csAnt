//css_ref "SoftwareMonkeys.csAnt.dll";

using System;
using System.IO;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;

class ReplaceScript : BaseScript
{
	public static void Main(string[] args)
	{
		new ReplaceScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Replacing text in files...");
		Console.WriteLine("");

        var replacer = new TextReplacer();

        var commit = Arguments.ContainsAny("c", "commit");

        var text = args[0];

        var replacementText = args[1];

        var patterns = new string[]{ "*" }; // TODO: Should this be "**" to search sub directories?

        if (Arguments.ContainsAny("f", "files"))
            patterns = Arguments["f", "files"].Split(';');

        replacer.ReplaceIn(CurrentDirectory, patterns, text, replacementText, commit); 
        
		return !IsError;
	}
	
	public void Clear()
	{
            foreach (var file in Directory.GetFiles(CurrentDirectory, "*.bak", SearchOption.AllDirectories))
            {
                Console.WriteLine(file);
                
                File.Delete(file);
            }	
        }
}
