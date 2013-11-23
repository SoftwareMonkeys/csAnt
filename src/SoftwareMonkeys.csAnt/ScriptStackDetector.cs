using System;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
    // TODO: Combine this with TestScriptStackDetector to reduce code duplication
    public class ScriptStackDetector
    {
        public IScript Script { get; set; }

        public ScriptStackDetector(IScript script)
        {
            Script = script;
        }

        public Stack<string> Detect ()
        {
            var s = Script;

            Stack<string> stack = new Stack<string> ();

            if (s.ParentScript != null) {
                s = s.ParentScript;
            
                if (Script.IsVerbose) {
                    Console.WriteLine ("");
                    Console.WriteLine ("Detecting script stack...");
                }


                while (s != null) {
                    if (Script.IsVerbose)
                        Console.WriteLine ("  " + s.ScriptName);

                    stack.Push (s.ScriptName);
                    s = s.ParentScript;
                }

                Console.WriteLine ("");
            }

            return stack;
        }
    }
}

