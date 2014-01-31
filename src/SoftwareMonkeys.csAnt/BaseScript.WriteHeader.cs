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

            if (IsVerbose)
            {
                Console.WriteLine (GetIndentSpace (Indent) + "// Directory: " + CurrentDirectory);

                // If the current directory is different from the original directory then output the original directory
                if (CurrentDirectory != OriginalDirectory)
                    Console.WriteLine (GetIndentSpace (Indent) + "// Original directory: " + OriginalDirectory);
            
                Console.WriteLine (GetIndentSpace (Indent) + "// Time stamp: " + TimeStamp);

                WriteScriptStack (GetScriptStack ());
                Console.WriteLine (GetIndentSpace (Indent) + "// Is verbose: " + IsVerbose);
            }

            Console.WriteLine ("");
        }
    }
}

