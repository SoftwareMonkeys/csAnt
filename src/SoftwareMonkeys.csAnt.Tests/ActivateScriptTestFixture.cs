using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class ActivateScriptTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_ActivateScript_ParentScriptPropertyIsSet()
        {
            var s = GetDummyScript();

            s.FilesGrabber.GrabOriginalScriptingFiles();

            var s2 = s.ActivateScript("HelloWorld");

            Assert.IsNotNull(s2.ParentScript, "The ParentScript property isn't set.");
        }
        
        [Test]
        public void Test_ActivateScript_ConsolePropertyIsSet()
        {
            var s = GetDummyScript();

            s.FilesGrabber.GrabOriginalScriptingFiles();

            var s2 = s.ActivateScript("HelloWorld");

            Assert.AreEqual(typeof(SubConsoleWriter).FullName, s2.Console.GetType().FullName, "The Console property isn't a SubConsoleWriter as it should be.");
            
            Assert.IsNotNull(
                ((SubConsoleWriter)s2.Console).ParentWriter,
                "The parent writer is null."
                );

            Assert.AreEqual(
                s.ScriptName,
                ((SubConsoleWriter)s2.Console).ParentWriter.ScriptName,
                "The script name for the parent writer is incorrect."
                );

            Assert.AreEqual(
                s.ScriptName,
                ((SubConsoleWriter)s2.Console).ParentWriter.ScriptName,
                "The script for the parent writer isn't as expected."
                );
        }
        
        [Test]
        public void Test_ActivateScript_TimePropertyIsSet()
        {
            var s = GetDummyScript();

            s.FilesGrabber.GrabOriginalScriptingFiles();

            var s2 = s.ActivateScript("HelloWorld");
            
            Assert.AreEqual(
                s.Time.ToString (),
                s2.Time.ToString(),
                "The time property of the activated script wasn't set to the same as the parent script."
            );

            Assert.AreEqual(
                s.TimeStamp,
                s2.TimeStamp,
                "The time stamp property of the activated script wasn't set to the same as the parent script."
            );
        }
        
        [Test]
        public void Test_ActivateScript_IndentIsSet()
        {
            var s = GetDummyScript();

            s.FilesGrabber.GrabOriginalScriptingFiles();

            var s2 = s.ActivateScript("HelloWorld");
            
            Assert.AreEqual(
                0,
                s.Indent,
                "The indent property on parent script is incorrect."
            );

            Assert.AreEqual(
                1,
                s2.Indent,
                "The indent property on activated script is incorrect."
            );
        }
        
        [Test]
        public void Test_ActivateScript_IsVerbosePropertyIsSet()
        {
            var s = GetDummyScript();

            s.IsVerbose = false;

            s.FilesGrabber.GrabOriginalScriptingFiles();

            var s2 = s.ActivateScript("HelloWorld");

            Assert.AreEqual(s.IsVerbose, s2.IsVerbose);

            s.IsVerbose = true;

            var s3 = s.ActivateScript("HelloWorld");
            
            Assert.AreEqual(s.IsVerbose, s3.IsVerbose);

        }
    }
}

