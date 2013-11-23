using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class FixCurrentDirectoryTestFixture : BaseTestFixture
    {
        public FixCurrentDirectoryTestFixture()
        {
            AutoInitialize = false;
        }

        [Test]
        public void Test_FixCurrentDirectory()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Testing the BaseTestFixture.FixCurrentDirectory() function.");
            Console.WriteLine ("");
            
            Console.WriteLine ("Environment.CurrentDirectory:");
            Console.WriteLine (Environment.CurrentDirectory);

            FixCurrentDirectory();

            Console.WriteLine ("");
            Console.WriteLine ("Fixed Environment.CurrentDirectory:");
            Console.WriteLine (Environment.CurrentDirectory);
            
            Assert.IsFalse(Environment.CurrentDirectory.Contains("bin/Debug"));
            Assert.IsFalse(Environment.CurrentDirectory.Contains("bin/Release"));
        }
    }
}

