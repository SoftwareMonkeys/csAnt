using System;
using NUnit.Framework;
using System.IO;
using SoftwareMonkeys.csAnt.Processes;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests.Unit
{
	[TestFixture]
	public class ProcessStarterTestFixture : BaseUnitTestFixture
	{
        [Test]
		public void Test_Start()
		{
            var testFile = PathConverter.ToAbsolute("TestFile.txt");

            File.WriteAllText(testFile, "Hello world");

            var starter = new ProcessStarter();

            if (Platform.IsLinux)
                starter.Start ("cp", "TestFile.txt", "TestFile2.txt");
            else
                throw new NotImplementedException("Windows support is yet to be implemented.");

			Assert.IsTrue(File.Exists(PathConverter.ToAbsolute("TestFile2.txt")), "File wasn't copied.");
        }
        
        [Test]
        public void Test_Start_Error()
        {
            // TODO: Overhaul test
            var script = GetDummyScript();
            
            var starter = new ProcessStarter()
            {
                ThrowExceptionOnError = false
            };

            starter.Start ("missingcommand", "Hello world!");

            var output = script.ConsoleWriter.Output;
            Assert.IsTrue(output.Contains ("Win32Exception: ApplicationName='missingcommand'"), "The output is incorrect.");
        }

        [Test]
        public void Test_FixArgument()
        {
            // TODO: Should this be moved to FixArgumentTestFixture
            var testFile = PathConverter.ToAbsolute("TestFile.txt");

            File.WriteAllText(testFile, "Hello world");

            var starter = new ProcessStarter();

            var toFileName = "Test File 2.txt";

            var fixedFileName = starter.FixArgument(toFileName);

            var expected = "\"" + toFileName + "\"";

            Assert.AreEqual(expected, fixedFileName, "The argument wasn't fixed.");
        }
	}
}

