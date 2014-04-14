using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public virtual void WriteHeader (string[] arguments)
        {
            Console.WriteLine ("");
            Console.WriteLine (Indenter.GetIndentSpace (Indent) + "// --------------------------------------------------");
            Console.WriteLine (Indenter.GetIndentSpace (Indent) + "// Executing script: " + ScriptName);
            WriteArguments(arguments);

            if (IsVerbose)
            {
                Console.WriteLine (Indenter.GetIndentSpace (Indent) + "// Directory: " + CurrentDirectory);

                // If the current directory is different from the original directory then output the original directory
                if (CurrentDirectory != OriginalDirectory)
                    Console.WriteLine (Indenter.GetIndentSpace (Indent) + "// Original directory: " + OriginalDirectory);
            
                Console.WriteLine (Indenter.GetIndentSpace (Indent) + "// Time stamp: " + TimeStamp);

                // TODO: Move to a property
                new ScriptStackWriter().Write (GetScriptStack ());
                Console.WriteLine (Indenter.GetIndentSpace (Indent) + "// Is verbose: " + IsVerbose);
            }

            Console.WriteLine ("");
        }

        public void WriteArguments(string[] arguments)
        {
            if (arguments.Length > 0)
            {
                Console.WriteLine (Indenter.GetIndentSpace (Indent) + "// Arguments:");
                foreach (var arg in arguments)
                {
                    Console.WriteLine(Indenter.GetIndentSpace (Indent) + "//  " + arg);
                }
            }
        }
    }
}

