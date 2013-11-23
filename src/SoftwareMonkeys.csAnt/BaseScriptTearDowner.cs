using System;

namespace SoftwareMonkeys.csAnt
{
    public abstract class BaseScriptTearDowner : IScriptTearDowner
    {
        public IScript Script { get;set; }

        public ConsoleWriter Console { get;set; }

        public BaseScriptTearDowner(IScript script)
        {
            Script = script;
            Console = Script.Console;
        }

        public virtual void TearDown ()
        {
            if (!Script.IsTornDown) {
                if (Script.IsVerbose) {
                    Console.WriteLine ("");
                    Console.WriteLine ("Tearing down '" + Script.ScriptName + "' script...");
                    Console.WriteLine ("Script type: '" + Script.GetType ().Name);
                    Console.WriteLine ("Component: '" + GetType ().Name + "'");
                    Console.WriteLine ("");
                }
            } else {
                if (Script.IsVerbose)
                {
                    Console.WriteLine ("");
                    Console.WriteLine ("'" + Script.ScriptName + "' script has already been torn down. Not tearing down again.");
                    Console.WriteLine ("");
                }
            }
        }
    }
}

