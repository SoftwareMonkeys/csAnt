using System;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
    public abstract class BaseScriptConstructor : IScriptConstructor
    {
        public IScript Script { get; set; }

        public BaseScriptConstructor (
            IScript script
        )
        {
            Script = script;
        }
        
        public virtual void Construct (string scriptName)
        {
            Construct (scriptName, null);
        }

        public virtual void Construct (string scriptName, IScript parentScript)
        {
            // Construct the property values first so script can be used
            ConstructBasicProperties(scriptName, parentScript);

            // Construct the console next so it can be used
            ConstructConsole();

            ConstructScriptStack();

            OutputHeader();

            ConstructFilesGrabber();

            ConstructTDC();

            ConstructLifecycle();

            ConstructRelocator();

            OutputFooter();
        }

        public void ConstructBasicProperties(string scriptName, IScript parentScript)
        {
            Script.ScriptName = scriptName;

            Script.ParentScript = parentScript;

            // Set the time and time stamp as early as possible
            if (Script.ParentScript != null) {
                Script.Time = Script.ParentScript.Time;
                Script.TimeStamp = Script.ParentScript.TimeStamp;
                Script.OriginalDirectory = Script.ParentScript.OriginalDirectory;
                Script.IsVerbose = Script.ParentScript.IsVerbose;
                Script.IsDebug = Script.ParentScript.IsDebug;
            } else {
                Script.Time = DateTime.Now;
                Script.TimeStamp = Script.GetTimeStamp ();
                Script.OriginalDirectory = Script.CurrentDirectory;
                
#if DEBUG
                Script.IsDebug = true;
                Script.IsVerbose = true;
#endif
            }
        }

        public void ConstructConsole ()
        {
            if (Script.IsVerbose)
                Console.WriteLine ("Setting console writer:");

            if (Script.ParentScript != null) {
                Script.Console = new SubConsoleWriter (Script.ParentScript.Console, Script.ScriptName);
                if (Script.IsVerbose)
                    Console.WriteLine ("  SubConsoleWriter");
            } else {
                Script.Console = new ConsoleWriter ("logs", Script.ScriptName);
                if (Script.IsVerbose)
                    Console.WriteLine ("  ConsoleWriter");
            }
        }

        public void ConstructScriptStack()
        {
            // Script stack
            Script.ScriptStack = GetScriptStack();
        }

        public virtual void ConstructLifecycle()
        {
            // Set-upper
            Script.SetUpper = new ScriptSetUpper (Script);

            // Tear-downer
            Script.TearDowner = new ScriptTearDowner (Script);
        }

        public void ConstructFilesGrabber()
        {
            // Files grabber
            Script.FilesGrabber = new FilesGrabber (Script);
        }

        public void ConstructTDC()
        {
            // Temporary directory creator
            Script.TemporaryDirectoryCreator = new TemporaryDirectoryCreator (
                Script,
                Script.TimeStamp,
                Script.IsVerbose
            );
        }

        public void ConstructRelocator()
        {
            // Relocator
            Script.Relocator = new ScriptRelocator (Script);
        }

        public void OutputHeader ()
        {
            
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("--------------------------------------------------");
                Console.WriteLine ("");
                Console.WriteLine ("Constructing '" + Script.ScriptName + "' script...");
                Console.WriteLine ("Script type: '" + Script.GetType ().Name + "'");
                Console.WriteLine ("Component: " + GetType ().Name);
                Console.WriteLine ("");
                Console.WriteLine ("Time: " + Script.Time.ToString ());
                Console.WriteLine ("Time stamp: " + Script.TimeStamp);
                Console.WriteLine ("");
                Console.WriteLine ("Current directory:");
                Console.WriteLine (Script.CurrentDirectory);
                Console.WriteLine ("Original directory:");
                Console.WriteLine (Script.OriginalDirectory);
                Console.WriteLine ("");
            }

        }

        public void OutputFooter()
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("--------------------------------------------------");
                Console.WriteLine ("");
            }
        }

        public Stack<string> GetScriptStack()
        {
            // TODO: Inject this instance
            return new ScriptStackDetector(Script).Detect();
        }
    }
}

