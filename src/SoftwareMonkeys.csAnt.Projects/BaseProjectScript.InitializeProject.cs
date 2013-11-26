using System;
using System.IO;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
        public void InitializeProject ()
        {
            InitializeProject(CurrentDirectory);
        }

		public void InitializeProject(string projectDirectory)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Initializing project:");
			Console.WriteLine (projectDirectory);
			Console.WriteLine ("");

			var initFile = "";
			if (!IsWindows)
				initFile = "init-" + ProjectName + "-project-linux.sh";
			else	
				initFile = "init-" + ProjectName + "-project-windows.sh";

			var cmdName = "bash";

			ProjectDirectory = projectDirectory;

			var initPath = projectDirectory
				+ Path.DirectorySeparatorChar
				+ initFile;

			Console.WriteLine("Command name:");
			Console.WriteLine(cmdName);
			Console.WriteLine("");
			Console.WriteLine("Initialize script launcher:");
			Console.WriteLine(initPath);

			if (File.Exists(initPath))
			{
                List<string> args = new List<string>();

                args.Add("\"" + initPath + "\"");
                args.Add ("-t:" + TimeStamp);
                args.Add ("-d:'" + ProjectDirectory + "'");

                if (IsVerbose)
                    args.Add ("-v");

				StartProcess(
					cmdName,
					args.ToArray()
				);
			}
			else
			{
				Console.WriteLine("");
				Console.WriteLine("Can't find initialization file:");
				Console.WriteLine(initPath);
				Console.WriteLine("");
			}
		}
	}
}

