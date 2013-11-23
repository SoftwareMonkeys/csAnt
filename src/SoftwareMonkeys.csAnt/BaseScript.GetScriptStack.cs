using System;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public Stack<string> GetScriptStack ()
        {
            var c = Console;

            Stack<string> stack = new Stack<string> ();

            if (c is SubConsoleWriter) {
                while (c is SubConsoleWriter) {
                    stack.Push(c.ScriptName);
                    c = ((SubConsoleWriter)c).ParentWriter;
                }
            }

            return stack;
        }
    }
}

