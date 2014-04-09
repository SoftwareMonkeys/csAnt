//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;

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

        if (args.Length == 2)
        {
            var text = args[0];

            var replacementText = args[1];

            replacer.ReplaceIn(CurrentDirectory, "**", text, replacementText, commit); 
        }
        if (args.Length == 3)
        {
            var pattern = args[0];

            var text = args[1];

            var replacementText = args[2];

            replacer.ReplaceIn(CurrentDirectory, pattern, text, replacementText, commit); 
        }
        

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
