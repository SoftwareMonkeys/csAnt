using System;

namespace SoftwareMonkeys.csAnt
{
    public class ErrorWriter
    {
        public ErrorWriter ()
        {
        }

        public void WriteError()
        {
            Console.WriteLine ("");
            Console.WriteLine (Indenter.GetIndentSpace() + "// -------------------- !!! Error !!! --------------------");
            Console.WriteLine (Indenter.GetIndentSpace() + "// Script: " + ScriptName);

            // TODO: Move to a property
            new ScriptStackWriter().Write(GetScriptStack());

            Console.WriteLine (Indenter.GetIndentSpace() + message);
            Console.WriteLine (Indenter.GetIndentSpace() + "// -------------------------------------------------------");
            Console.WriteLine ("");

            // TODO: Reimplement
            //AddSummary("Error in script '" + ScriptName + "': " + message);

            if (StopOnFail)
                Environment.Exit(0);
        }
    }
}

