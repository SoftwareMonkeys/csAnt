using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class ActivateScriptTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_ActivateScript_ParentScriptPropertyIsSet()
        {
            var script = GetDummyScript();

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();

            var script2 = script.ActivateScript("HelloWorld");

            Assert.IsNotNull(script2.ParentScript, "The ParentScript property isn't set.");
        }
        
        [Test]
        public void Test_ActivateScript_ConsolePropertyIsSet()
        {
            throw new NotImplementedException("This should be obsolete. Remove if not needed.");
            /*
            var s = GetDummyScript();

            s.FilesGrabber.GrabOriginalScriptingFiles();

            var s2 = s.ActivateScript("HelloWorld");

            Assert.AreEqual(typeof(SubConsoleWriter).FullName, s2.ConsoleWriter.GetType().FullName, "The Console property isn't a SubConsoleWriter as it should be.");
            
            Assert.IsNotNull(
                ((SubConsoleWriter)s2.ConsoleWriter).ParentWriter,
                "The parent writer is null."
                );

            Assert.AreEqual(
                s.ScriptName,
                ((SubConsoleWriter)s2.ConsoleWriter).ParentWriter.ScriptName,
                "The script name for the parent writer is incorrect."
                );

            Assert.AreEqual(
                s.ScriptName,
                ((SubConsoleWriter)s2.ConsoleWriter).ParentWriter.ScriptName,
                "The script for the parent writer isn't as expected."
                );*/
        }
        
        [Test]
        public void Test_ActivateScript_TimePropertyIsSet()
        {
            var script = GetDummyScript();

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();

            var script2 = script.ActivateScript("HelloWorld");
            
            Assert.AreEqual(
                script.Time.ToString (),
                script2.Time.ToString(),
                "The time property of the activated script wasn't set to the same as the parent script."
            );

            Assert.AreEqual(
                script.TimeStamp,
                script2.TimeStamp,
                "The time stamp property of the activated script wasn't set to the same as the parent script."
            );
        }
        
        [Test]
        public void Test_ActivateScript_IndentIsSet()
        {
            var script = GetDummyScript();

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();

            var script2 = script.ActivateScript("HelloWorld");
            
            Assert.AreEqual(
                0,
                script.Indent,
                "The indent property on parent script is incorrect."
            );

            Assert.AreEqual(
                1,
                script2.Indent,
                "The indent property on activated script is incorrect."
            );
        }
        
        [Test]
        public void Test_ActivateScript_IsVerbosePropertyIsSet()
        {
            var script = GetDummyScript();

            script.IsVerbose = false;

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();

            var script2 = script.ActivateScript("HelloWorld");

            Assert.AreEqual(script.IsVerbose, script2.IsVerbose);

            script.IsVerbose = true;

            var script3 = script.ActivateScript("HelloWorld");
            
            Assert.AreEqual(script.IsVerbose, script3.IsVerbose);

        }
    }
}

