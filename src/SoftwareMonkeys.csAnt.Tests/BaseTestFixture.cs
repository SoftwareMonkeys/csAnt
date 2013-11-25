using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
	public abstract partial class BaseTestFixture
	{
        public DateTime Time { get;set; }
        public string TimeStamp { get;set; }
      
        public bool IsVerbose { get;set; }

        private string workingDirectory;
        public string WorkingDirectory {
            get {
                if (String.IsNullOrEmpty(workingDirectory))
                    return Environment.CurrentDirectory;
                return workingDirectory; }
            set { workingDirectory = value; }
        }

        public List<IScript> Scripts = new List<IScript>();

        /// <summary>
        /// A flag indicating whether to auto initialize the test.
        /// Initialization moves the test to a temporary directory, etc.
        /// </summary>
        public bool AutoInitialize = true;

		public BaseTestFixture ()
		{
#if DEBUG
            IsVerbose = true;
#else
            IsVerbose = false;
#endif
		}

        [SetUp]
        public void SetUp ()
        {
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("----------------------------------------");
                Console.WriteLine ("");
                Console.WriteLine ("Setting up test BaseTestFixture...");
                Console.WriteLine (GetType ().Name);
            }

            TimeStamp = GetTimeStamp ();

            if (IsVerbose) {
                Console.WriteLine ("Time stamp: " + TimeStamp);

                Console.WriteLine ("Auto initialize: " + AutoInitialize.ToString ());
                Console.WriteLine ("");
            }

            if (AutoInitialize) {
                FixCurrentDirectory ();

                WorkingDirectory = GetWorkingDirectory();
            }
             
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Working directory:");
                Console.WriteLine (WorkingDirectory);
                Console.WriteLine ("");
                Console.WriteLine ("----------------------------------------");
                Console.WriteLine ("");
            }
        }

        [TearDown]
        public void TearDown ()
        {
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("----------------------------------------");
                Console.WriteLine ("");
                Console.WriteLine ("Tearing down test fixture.");
                Console.WriteLine ("Component: " + GetType ().Name);
                Console.WriteLine ("");
            }

            foreach (var s in Scripts) {
                if (!s.IsTornDown)
                    s.TearDown ();
            }

            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("----------------------------------------");
                Console.WriteLine ("");
            }
        }

        public string GetTimeStamp()
        {
            if (Time == DateTime.MinValue)
                Time = DateTime.Now;

            return Time.Year
                + "-"
                + Time.Month
                + "-"
                + Time.Day
                + "--"
                + Time.Hour
                + "-"
                + Time.Minute
                + "-"
                + Time.Second;
        }

        public void FixCurrentDirectory ()
        {
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Fixing current directory...");
                Console.WriteLine ("Current directory:");
                Console.WriteLine (Environment.CurrentDirectory);
                Console.WriteLine ("");
            }

            var modes = new string[]{"Debug", "Release"};

            foreach (var mode in modes) {
                if (Environment.CurrentDirectory.EndsWith ("/bin/" + mode))
                    Environment.CurrentDirectory = Environment.CurrentDirectory.Replace ("/bin/" + mode, "");
            }
            
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("New current directory:");
                Console.WriteLine (Environment.CurrentDirectory);
                Console.WriteLine ("");
            }
        }

        public void WriteDirectoryStructure (string[] patterns)
        {
            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Directory structure...");
                Console.WriteLine ("Working directory:");
                Console.WriteLine (WorkingDirectory);
                Console.WriteLine ("");
            }
            var dir = WorkingDirectory;
            WriteDirectoryStructure(dir);

            if (IsVerbose)
                Console.WriteLine ("");
        }
        
        public void WriteDirectoryStructure(string subDirectory)
        {
            WriteDirectoryStructure(WorkingDirectory, subDirectory);
        }

        public void WriteDirectoryStructure (string baseDirectory, string subDirectory)
        {
            if (IsVerbose) {
                Console.WriteLine (subDirectory.Replace (baseDirectory, ""));
            }

            foreach (string f in Directory.GetFiles (subDirectory)) {
                if (IsVerbose)
                    Console.WriteLine(f.Replace (baseDirectory, ""));
            }

            foreach (string d in Directory.GetDirectories(subDirectory)) {
                WriteDirectoryStructure(d);
            }
        }
    }
}