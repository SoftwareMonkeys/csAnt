using System;
using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class NUnitTestRunner
    {
        public IScript Script { get;set; }

        public NUnitTestResultReturner Returner { get; set; }

        public XmlResultFileNamer XmlFileNamer { get;set; }

        public HtmlResultFileNamer HtmlFileNamer { get;set; }

        public string Mode { get;set; }

        /// <summary>
        /// A value indicating whether to automatically finish up by generating results and returning them to the original project.
        /// </summary>
        public bool AutoFinish = true;

        public IConsoleWriter Console { get; set; }
        
        public IFileFinder FileFinder { get;set; }
        
        public string[] IncludeCategories { get;set; }
        public string[] ExcludeCategories { get;set; }
        
        public NUnitTestRunner (
            IScript script,
            string mode
        )
            : this(
                script,
                new NUnitTestResultReturner(
                    script
                ),
                mode
            )
        {

        }
        public NUnitTestRunner (
            IScript script,
            NUnitTestResultReturner returner,
            string mode
        )
            : this(
                script,
                returner,
                new XmlResultFileNamer(),
                new HtmlResultFileNamer(),
                mode
            )
        {
        }

        public NUnitTestRunner (
            IScript script,
            NUnitTestResultReturner returner,
            XmlResultFileNamer xmlFileNamer,
            HtmlResultFileNamer htmlFileNamer,
            string mode
        )
        {
            Script = script;
            Returner = returner;
            Mode = mode;
            Console = Script.ConsoleWriter;
            XmlFileNamer = xmlFileNamer;
            HtmlFileNamer = htmlFileNamer;
            FileFinder = new FileFinder();
        }
        
        public void RunTests ()
        {
            var dir = Script.CurrentDirectory
                    + Path.DirectorySeparatorChar
                    + "bin"
                    + Path.DirectorySeparatorChar
                    + "Release";

            RunTestsInDirectory(dir);
        }

        public void RunTestsInDirectory (string directory)
        {
            RunTestsInDirectory(directory, new string[]{});
        }

        public void RunTestsInDirectory (string directory, params string[] testNames)
        {
            EnsureDirectories ();
    
            Console.WriteLine ("Test assemblies:");

            List<string> executedAssemblies = new List<string> ();
            
            var extensions = new string[]{
                "*.dll",
                "*.exe"
            };

            foreach (string assemblyFile in FileFinder.FindFiles(directory, extensions)) {
                if (AssemblyContainsTestFixtures (assemblyFile, testNames)) {
                    var assemblyFileName = Path.GetFileName (assemblyFile);

                    if (!executedAssemblies.Contains (assemblyFileName)) {
                        executedAssemblies.Add (
                                        assemblyFileName
                        );
                                
                        Console.WriteLine (assemblyFile);

                        if (testNames != null && testNames.Length > 0) {
                            foreach (var name in testNames)
                                RunAssemblyTests (assemblyFile, name);
                        } else
                            RunAssemblyTests (assemblyFile, String.Empty);
                    }
                }
            }

            if (AutoFinish) {
                Finish();
            }
        }

        public void Finish()
        {
            GenerateResults ();

            ReturnResults ();
        }

        public void ReturnResults()
        {
            Returner.ReturnResults();
        }

        public bool AssemblyContainsTestFixtures (string assemblyFile, string[] testNames)
        {
            var a = Assembly.LoadFrom (assemblyFile);

            bool does = false;

            foreach (var t in a.GetTypes()) {
                if (t.GetCustomAttributes(typeof(TestFixtureAttribute), true).Length > 0)
                {
                    if (testNames == null
                        || testNames.Length == 0)
                        does = true;
                    else
                    {
                        foreach (var name in testNames)
                        {
                            if (t.FullName == name)
                                does = true;
                        }
                    }
                }
            }

            return does;
        }

        public void RunAssemblyTests(string assemblyFile, string testName)
        {
            string assemblyFileName = Path.GetFileName(assemblyFile);

            string xmlResult = XmlFileNamer.GetResultsDirectory(Script)
                    + Path.DirectorySeparatorChar
                    + Path.GetFileNameWithoutExtension(assemblyFileName).Replace(".", "-")
                    + ".xml";
                    
            if (!Directory.Exists(Path.GetDirectoryName(xmlResult)))
                Directory.CreateDirectory(Path.GetDirectoryName(xmlResult));

            string command = "mono";

            List<string> arguments = new List<string>();

            // TODO: Remove if not needed
            arguments.Add("--runtime=v4.0");

            // TODO: Make configurable
            arguments.Add("lib/NUnit.Runners.2.6.0.12051/tools/nunit-console.exe");

            arguments.Add("\"" + PathConverter.ToRelative(assemblyFile) + "\"");

            arguments.Add("-xml=" + PathConverter.ToRelative(xmlResult));
            
            if (!String.IsNullOrEmpty(testName))
                arguments.Add ("-run=" + testName);
            
            if (IncludeCategories != null && IncludeCategories.Length > 0)
                arguments.Add ("-include=" + String.Join(",", IncludeCategories));

            if (ExcludeCategories != null && ExcludeCategories.Length > 0)
                arguments.Add ("-exclude=" + String.Join(",", ExcludeCategories));

            Script.StartProcess(
                    command,
                    arguments.ToArray()
            );

        }

        public void GenerateResults ()
        {
            var xmlResultDir = XmlFileNamer.GetResultsDirectory (Script)
                + Path.DirectorySeparatorChar;

            string htmlResultDir = HtmlFileNamer.GetResultsDirectory (Script);
                        
            List<string> arguments = new List<string> ();

            arguments.Add ("lib/NUnitResults.1.1/nunit-results.exe");

            arguments.Add ("\"" + xmlResultDir + "\"");

            arguments.Add ("\"" + htmlResultDir + "\"");

            Script.StartProcess (
                        "mono",
                        arguments.ToArray ()
            );
        }

        public void EnsureDirectories ()
        {
            var xmlResultDir = XmlFileNamer.GetResultsDirectory (Script);

            if (!Directory.Exists (xmlResultDir))
                Directory.CreateDirectory (xmlResultDir);

            var htmlResultDir = HtmlFileNamer.GetResultsDirectory (Script);

            if (!Directory.Exists (htmlResultDir))
                Directory.CreateDirectory (htmlResultDir);

        }

        public void AddIncludeCategory(string category)
        {
            var list = new List<string>();
            if (IncludeCategories != null)
                list.AddRange(IncludeCategories);

            list.Add(category);

            IncludeCategories = list.ToArray();
        }

        public void AddExcludeCategory(string category)
        {
            var list = new List<string>();
            if (ExcludeCategories != null)
                list.AddRange(ExcludeCategories);

            list.Add(category);

            ExcludeCategories = list.ToArray();
        }
    }
}

