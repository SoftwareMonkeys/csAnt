using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public virtual void WriteFooter()
        {
            Console.WriteLine ("");
            Console.WriteLine (GetIndentSpace (Indent) + "// Finished executing script: " + ScriptName);
            if (IsVerbose)
                WriteScriptStack (GetScriptStack ());
            Console.WriteLine (GetIndentSpace (Indent) + "// --------------------------------------------------");

        }
    }
}

