using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class GetDummyScriptTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_GetDummyScript()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Testing the GetTestScript function.");

            var script = GetDummyScript("MyTestScript");
            
            Console.WriteLine ("");
            Console.WriteLine ("Working directory:");
            Console.WriteLine (WorkingDirectory);
            
            Console.WriteLine ("");
            Console.WriteLine ("Time:");
            Console.WriteLine (script.Time.ToString ());
            Console.WriteLine ("");
            Console.WriteLine ("Time stamp:");
            Console.WriteLine (script.TimeStamp);

            Assert.IsNotNull (script);

            Assert.IsNotNullOrEmpty(script.TimeStamp);

            Assert.AreNotEqual(DateTime.MinValue, script.Time);

            Assert.IsNotNullOrEmpty(script.CurrentDirectory);
            
            Console.WriteLine ("Working directory:");
            Console.WriteLine (WorkingDirectory);
            Console.WriteLine ("Current directory:");
            Console.WriteLine (script.CurrentDirectory);

            Assert.AreEqual(WorkingDirectory, script.CurrentDirectory);
        }
    }
}

