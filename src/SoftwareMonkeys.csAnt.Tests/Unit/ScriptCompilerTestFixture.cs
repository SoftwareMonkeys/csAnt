using System;
using System.IO;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.Tests.Unit;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class ScriptCompilerTestFixture : BaseUnitTestFixture
    {
        [Test]
        public void Test_CompileScripts()
        {
            new FilesGrabber (OriginalDirectory, CurrentDirectory)
                .GrabOriginalFiles ();

            new ScriptExecutor (WorkingDirectory).Execute ("CycleBuild");

            //var script = GetDummyScript();

            //var scriptPath = CreateScriptFile();

            var scriptCompiler = new ScriptCompiler();
            scriptCompiler.CompileAll();

            var assemblyFile = scriptCompiler.FileNamer.GetScriptAssemblyPath("HelloWorld");

            Assert.IsTrue(File.Exists(assemblyFile), "Assembly file not found.");
        }

        public string CreateScriptFile ()
        {
            var scriptCode = GetScriptCode();

            var scriptPath = WorkingDirectory
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                    + "HelloWorld.cs";

            if (!Directory.Exists (Path.GetDirectoryName(scriptPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(scriptPath));

            File.WriteAllText(scriptPath, scriptCode);

            return scriptPath;
        }
        
        public string GetScriptCode()
        {
            return @"//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
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
        Console.WriteLine(""Hello world!"");

        return !IsError;
    }
}
";
        }
    }
}

