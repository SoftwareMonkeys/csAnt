//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CompileScriptScript : BaseProjectScript
{
        public static void Main(string[] args)
        {
                new CompileScriptScript().Start(args);
        }

        public override bool Run(string[] args)
        {
                var scriptName = args[0];
        
                var arguments = new Arguments(args);

                CompileScript(scriptName, arguments.Contains("f"));

                return !IsError;
        }
}
