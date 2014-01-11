using System;
using System.Collections.Generic;
using SoftwareMonkeys.csAnt.IO;
using System.IO;

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
            // TODO: Reorganize code
            if (parentScript != null) {
                //Script.InitializePackageManager (parentScript.Packages); // TODO: Remove if not needed
                Script.InitializeFileFinder(parentScript.FileFinder);
            }
            else
                Script.InitializeFileFinder(new FileFinder());

            // Construct the property values first so script can be used
            ConstructBasicProperties(scriptName, parentScript);

            ConstructConsoleWriter(scriptName, parentScript);

            ConstructScriptStack();

            OutputHeader();

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

        public void ConstructConsoleWriter (string scriptName, IScript parentScript)
        {
            // If the parent script has a console writer then use it
            if (parentScript != null && parentScript.ConsoleWriter != null) {
                Script.ConsoleWriter = Script.ParentScript.ConsoleWriter;
            } else { // Otherwise construct a new one
                Script.ConsoleWriter = new ConsoleWriter(Console.Out, "logs", scriptName);
            }
            
            Console.SetOut ((TextWriter)Script.ConsoleWriter);
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

        // TODO: Remove if not needed. Should be obsolete
        /*public void ConstructFilesGrabber()
        {
            // Files grabber
            Script.FilesGrabber = new FilesGrabber (
                Script.OriginalDirectory,
                Script.CurrentDirectory
                );
        }*/

        public void ConstructTDC()
        {
            // Temporary directory creator
            Script.TemporaryDirectoryCreator = new TemporaryDirectoryCreator (
                Script.CurrentDirectory,
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

