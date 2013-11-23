using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	public class TestUtilities
	{
		public IDummyScript Script { get;set; }

		public TestUtilities (
			IDummyScript script
		)
		{
			Script = script;
		}

		public void CopyTestResults (string fromProjectDirectory, string toProjectDirectory)
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Copying test results to original directory...");
                Console.WriteLine ("From dir:");
                Console.WriteLine (fromProjectDirectory);
                Console.WriteLine ("To dir:");
                Console.WriteLine (toProjectDirectory);
            }

            var fromTestsDir = fromProjectDirectory
                + Path.DirectorySeparatorChar
                + "tests"
                + Path.DirectorySeparatorChar
                + "results";

            var destinationTestsDir = toProjectDirectory
                + Path.DirectorySeparatorChar
                + "tests"
                + Path.DirectorySeparatorChar
                + "results";

            var groupName = GetGroupName ();

            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Group name: " + groupName);
                Console.WriteLine ("");
			
                Console.WriteLine ("Moving test results from:");
                Console.WriteLine ("  " + fromTestsDir);
                Console.WriteLine ("To:");
                Console.WriteLine ("  " + destinationTestsDir);
                Console.WriteLine ("");
            }

            if (Directory.Exists (fromTestsDir)) {
                foreach (var typeDir in Directory.GetDirectories(fromTestsDir)) {
                    foreach (var dateDir in Directory.GetDirectories(typeDir)) {
                        foreach (var formatDir in Directory.GetDirectories(dateDir)) {

                            var type = Path.GetFileNameWithoutExtension (typeDir);

                            var date = Path.GetFileNameWithoutExtension (dateDir);

                            var format = Path.GetFileNameWithoutExtension (formatDir);

                            var toDir = destinationTestsDir
                                + Path.DirectorySeparatorChar
                                + type
                                + Path.DirectorySeparatorChar
                                + date
                                + Path.DirectorySeparatorChar
                                + format;
						
                            if (Script.IsVerbose)
                            {
                                Console.WriteLine ("");
                                Console.WriteLine ("Dir:");
                                Console.WriteLine ("  " + formatDir);
                                Console.WriteLine ("To dir:");
                                Console.WriteLine ("  " + toDir);
                                Console.WriteLine ("");
                                Console.WriteLine ("Type:");
                                Console.WriteLine ("  " + type);
                                Console.WriteLine ("Date:");
                                Console.WriteLine ("  " + date);
                                Console.WriteLine ("Format:");
                                Console.WriteLine ("  " + format);
                                Console.WriteLine ("");
                            }

                            if (!String.IsNullOrEmpty (groupName)) {
                                toDir = toDir
                                    + Path.DirectorySeparatorChar
                                    + groupName;
                            }

                            Script.CopyDirectory (formatDir, toDir);
                        }
                    }
                }
            }
            
            if (Script.IsVerbose) {
                Console.WriteLine ("");
            }
		}

		public string GetGroupName()
		{
			ITestScript s = Script;

			string groupName = String.Empty;

			while (s != null)
			{
				if (s is IDummyScript)
					groupName = ((IDummyScript)s).TestGroupName;

				if (s.Console is SubConsoleWriter)
					s = ((SubConsoleWriter)s.Console).ParentWriter.Script;
				else
					s = null;
			}

			return groupName;
		}
	}
}

