using System;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class TestScriptStackDetector
    {
        public IScript Script { get; set; }

        public TestScriptStackDetector(IScript script)
        {
            Script = script;
        }

        public Stack<string> Detect ()
        {
            var s = Script;

            Stack<string> stack = new Stack<string> ();

            if (s.ParentScript != null) {
                s = s.ParentScript;
            
                Console.WriteLine ("");
                Console.WriteLine ("Detecting test script stack...");


                while (s != null) {
                    Console.WriteLine ("  " + s.ScriptName);
                    if (s.GetType ().GetInterface("ITestScript") != null) {
                        Console.WriteLine ("    Is test script. Adding to stack.");
                        stack.Push (s.ScriptName);
                    } else
                        Console.WriteLine ("    Is not test script. Not adding to stack.");

                    s = s.ParentScript;
                }

                Console.WriteLine ("");
            }

            return stack;
        }
    }
}

