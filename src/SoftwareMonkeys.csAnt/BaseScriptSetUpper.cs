using System;

namespace SoftwareMonkeys.csAnt
{
    public abstract class BaseScriptSetUpper : IScriptSetUpper
    {
        public IScript Script { get; set; }

        public ConsoleWriter Console { get; set; }

        public BaseScriptSetUpper(IScript script)
        {
            Script = script;
            Console = Script.Console;
        }

        public virtual void SetUp ()
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("--------------------------------------------------");
                Console.WriteLine ("");
                Console.WriteLine ("Setting up '" + Script.ScriptName + "' script...");
                Console.WriteLine ("Component: " + GetType ().Name);
                Console.WriteLine ("Type: " + Script.GetType ().Name);
                Console.WriteLine ("");
            }

            // TODO: Check if needed. All this is done during constructor
            /*if (Script.ParentScript != null) {
                Script.Time = Script.ParentScript.Time;
                Script.TimeStamp = Script.ParentScript.TimeStamp;
                Script.OriginalDirectory = Script.OriginalDirectory;
            } else {
                if (String.IsNullOrEmpty (Script.TimeStamp) || Script.Time == DateTime.MinValue) {
                    Script.Time = DateTime.Now;
                    Script.TimeStamp = Script.GetTimeStamp ();
                }
                Script.OriginalDirectory = Script.CurrentDirectory;
            }*/

            if (Script.IsVerbose) {
                Console.WriteLine ("Time: " + Script.Time.ToString ());
                Console.WriteLine ("Time stamp: " + Script.TimeStamp);
                Console.WriteLine ("Current dir: ");
                Console.WriteLine (Script.CurrentDirectory);
                Console.WriteLine ("");

                Console.WriteLine ("--------------------------------------------------");
                Console.WriteLine ("");
            }
        }
    }
}

