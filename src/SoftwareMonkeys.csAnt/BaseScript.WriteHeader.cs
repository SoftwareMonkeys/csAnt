using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public virtual void WriteHeader ()
        {
            Console.WriteLine ("");
            Console.WriteLine (GetIndentSpace (Indent) + "// --------------------------------------------------");
            Console.WriteLine (GetIndentSpace (Indent) + "// Executing script: " + ScriptName);
            Console.WriteLine (GetIndentSpace (Indent) + "// Directory: " + CurrentDirectory);

            if (IsVerbose)
                Console.WriteLine (GetIndentSpace (Indent) + "// Original directory: " + OriginalDirectory);
            
            Console.WriteLine (GetIndentSpace (Indent) + "// Time stamp: " + TimeStamp);

            if (IsVerbose) {
                WriteScriptStack (GetScriptStack ());
                Console.WriteLine (GetIndentSpace (Indent) + "// Is verbose: " + IsVerbose);
            }

            Console.WriteLine ("");
        }
    }
}

