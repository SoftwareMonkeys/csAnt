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
	}
}

