using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public virtual void WriteFooter ()
        {
            Console.WriteLine ("");
            Console.WriteLine (Indenter.GetIndentSpace (Indent) + "// Finished script: " + ScriptName);
            if (IsVerbose)
            {
                // TODO: Move to a property
                new ScriptStackWriter().Write (GetScriptStack ());
            }
            Console.WriteLine (Indenter.GetIndentSpace (Indent) + "// --------------------------------------------------");

        }
    }
}

