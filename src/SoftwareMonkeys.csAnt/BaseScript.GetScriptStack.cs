using System;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public Stack<IScript> GetScriptStack ()
        {
            // TODO: Clean up
            //var c = ConsoleWriter;

            Stack<IScript> stack = new Stack<IScript> ();

            if (ParentScript != null) {
                IScript script = this;

                while (script.ParentScript != null)
                {
                    script = script.ParentScript;

                    stack.Push(script);
                }
            }

            /*if (c is SubConsoleWriter) {
                while (c is SubConsoleWriter) {
                    stack.Push(c.ScriptName);
                    c = ((SubConsoleWriter)c).ParentWriter;
                }
            }*/

            return stack;
        }
    }
}

