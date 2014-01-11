using System;
using NUnit.Framework;
using System.Reflection;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    [TestFixture]
    public class GetTestScriptTestFixture : BaseScriptingTestFixture
    {
        [Test]
        public void Test_GetTestScript_HasCorrectDefaultPropertyValues()
        {
            Console.WriteLine ("");
            Console.WriteLine ("Testing the GetTestScript function.");

            var script = GetTestScript("MyTestScript");
            
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
        
        [Test]
        public void Test_GetTestScript_HasCorrectCurrentDirectory()
        {

            Console.WriteLine ("");
            Console.WriteLine ("Testing the GetTestScript function.");

            var script = GetTestScript("MyTestScript");
            
            Console.WriteLine ("");
            Console.WriteLine ("Working directory:");
            Console.WriteLine (WorkingDirectory);

            var expected = script.OriginalDirectory + ".tmp"
                + Path.DirectorySeparatorChar
                + TimeStamp
                + Path.DirectorySeparatorChar
                + "csAnt";

            var cd = script.CurrentDirectory;
            
            Console.WriteLine ("");
            Console.WriteLine ("Expected:");
            Console.WriteLine (expected);
            Console.WriteLine ("CurrentDirectory:");
            Console.WriteLine (cd);
            Console.WriteLine ("");

            Assert.AreEqual(expected, cd, "Script.CurrentDirectory isn't correct.");
        }
        
        [Test]
        public void Test_GetTestScript_SubScript_HasCorrectCurrentDirectory()
        {

            Console.WriteLine ("");
            Console.WriteLine ("Testing the GetTestScript function.");

            var script = GetTestScript("MyTestScript");

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();

            var script2 = script.ActivateScript("HelloWorld");
            
            Console.WriteLine ("");
            Console.WriteLine ("Working directory:");
            Console.WriteLine (WorkingDirectory);

            var expected = script.OriginalDirectory + ".tmp"
                //+ Path.DirectorySeparatorChar // TODO: Remove if not needed.
                //+ TimeStamp
                //+ Path.DirectorySeparatorChar
                //+ "csAnt.tmp"
                + Path.DirectorySeparatorChar
                + TimeStamp
                + Path.DirectorySeparatorChar
                + "csAnt";
            
            Console.WriteLine ("");
            Console.WriteLine ("Expected:");
            Console.WriteLine (expected);
            Console.WriteLine ("CurrentDirectory:");
            Console.WriteLine (script2.CurrentDirectory);
            Console.WriteLine ("");

            Assert.AreEqual(expected, script2.CurrentDirectory, "Script.CurrentDirectory isn't correct.");
        }
        
        [Test]
        public void Test_GetTestScript_SubScript_HasCorrectOriginalDirectory()
        {

            Console.WriteLine ("");
            Console.WriteLine ("Testing the GetTestScript function.");

            var script = GetTestScript("MyTestScript");

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();

            var script2 = script.ActivateScript("Test_HelloWorld");
            
            Console.WriteLine ("");
            Console.WriteLine ("Working directory:");
            Console.WriteLine (WorkingDirectory);

            var expected = script.OriginalDirectory;
            
            Console.WriteLine ("");
            Console.WriteLine ("Expected:");
            Console.WriteLine (expected);
            Console.WriteLine ("CurrentDirectory:");
            Console.WriteLine (script2.OriginalDirectory);
            Console.WriteLine ("");

            Assert.AreEqual(expected, script2.OriginalDirectory, "Script.OriginalDirectory isn't correct.");
        }
        
        [Test]
        public void Test_GetTestScript_SubScript_HasCorrectTimeAndTimeStamp()
        {
            var script = GetTestScript("MyTestScript");

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();

            var script2 = script.ActivateScript("HelloWorld");

            Console.WriteLine ("");
            Console.WriteLine ("Expected time:");
            Console.WriteLine (Time.ToString());
            Console.WriteLine ("Actual time:");
            Console.WriteLine (script2.Time.ToString());
            Console.WriteLine ("");
            
            Console.WriteLine ("");
            Console.WriteLine ("Expected time stamp:");
            Console.WriteLine (TimeStamp);
            Console.WriteLine ("Actual time stamp:");
            Console.WriteLine (script2.TimeStamp);
            Console.WriteLine ("");

            Assert.AreEqual(Time.ToString(), script2.Time.ToString(), "Time property is incorrect.");
            Assert.AreEqual(TimeStamp, script2.TimeStamp, "TimeStamp property is incorrect.");
        }
    }
}

