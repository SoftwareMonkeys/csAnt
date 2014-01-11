using System;
using NUnit.Framework;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class RaiseEventTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_RaiseEvent()
        {
            var script = GetDummyScript();

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();

            var eventName = "CustomEvent";

            var messageOutput = "Handling 'CustomEvent' event";

            CreateEventScript(eventName, messageOutput);

            script.RaiseEvent(eventName);

            Assert.IsFalse(script.IsError, "An error occurred.");

            Assert.IsTrue(Console.Out.ToString().Contains(messageOutput), "Expected output not found.");
        }

        public void CreateEventScript(string eventName, string messageOutput)
        {
            var content = @"//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class OnCustomEvent_DoSomethingScript : BaseScript
{
    public static void Main(string[] args)
    {
        new OnCustomEvent_DoSomethingScript().Start(args);
    }
    
    public override bool Run(string[] args)
    {
        Console.WriteLine("""");
        Console.WriteLine(""" + messageOutput + @""");
        Console.WriteLine("""");

        return !IsError;
    }
}";

            var fileName = WorkingDirectory
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + "On" + eventName + "_DoSomething.cs";

            Console.WriteLine ("Creating dummy event script:");
            Console.WriteLine ("  " + fileName);

            File.WriteAllText(fileName, content);
        }

    }
}

