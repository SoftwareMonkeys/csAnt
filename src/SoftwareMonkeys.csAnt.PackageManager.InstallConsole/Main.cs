using System;

namespace SoftwareMonkeys.csAnt.InstallConsole
{
	class MainClass
	{
		public static string Path { get;set; }

		public static bool IsVerbose { get;set; }

        public static bool OverwriteFiles { get;set; }

        public static string InstallList { get; set; }

        public static bool IsHelp { get; set; }

		public static void Main (string[] args)
        {
            SetVariables (args);

            if (IsHelp)
                PrintHelp ();
            else {
                throw new NotImplementedException();
                /*var installer = new PackageInstaller (
    				Path,
    				IsVerbose,
                    OverwriteFiles
                );

                installer.Install (InstallList);*/
            }
		}

		public static void SetVariables(string[] args)
		{
			var arguments = new Arguments(args);

			Path = Environment.CurrentDirectory;

			if (arguments.Contains("p"))
				Path = arguments["p"];

            IsHelp = arguments.Contains("h");

			Console.WriteLine ("Path:");
			Console.WriteLine (Path);

			IsVerbose = arguments.Contains("v");

			if (IsVerbose)
				Console.WriteLine ("Is verbose: " + IsVerbose);

            OverwriteFiles = arguments.Contains("o");

            if (IsVerbose)
                Console.WriteLine ("Overwrite files: " + OverwriteFiles);

            InstallList = "standard";
            if (arguments.Contains("l"))
                InstallList = arguments["l"];

        }

        public static void PrintHelp()
        {
            Console.WriteLine ("------------------------------");
            Console.WriteLine ("");
            Console.WriteLine ("csAnt-Install Help");
            Console.WriteLine ("");

            Console.WriteLine ("Syntax:");
            Console.WriteLine ("");
            Console.WriteLine ("  csAnt-Install.exe");
            Console.WriteLine ("... or...");
            Console.WriteLine ("  mono csAnt-Install.exe");
            Console.WriteLine ("... or...");
            Console.WriteLine ("  csAnt-Install.exe [-l:(listname)] [-p:(path)] [-o] [-v]");
            Console.WriteLine ("... or...");
            Console.WriteLine ("  mono csAnt-Install.exe [-l:(listname)] [-p:(path)] [-o] [-v]");
            Console.WriteLine ("");

            Console.WriteLine ("Arguments:");
            Console.WriteLine ("");
            Console.WriteLine ("  -l:(listname)");
            Console.WriteLine ("    The name of the install list to use. (Example: 'standard', 'project')");
            Console.WriteLine ("");
            Console.WriteLine ("  -p");
            Console.WriteLine ("    The path to the install directory.");
            Console.WriteLine ("");
            Console.WriteLine ("  -o");
            Console.WriteLine ("    If the argument is provided then files will be overwritten if necessary.");
            Console.WriteLine ("");
            Console.WriteLine ("  -v");
            Console.WriteLine ("    If the argument is provided then verbose output will be generated.");
            
            Console.WriteLine ("");
            Console.WriteLine ("------------------------------");
        }
	}
}
