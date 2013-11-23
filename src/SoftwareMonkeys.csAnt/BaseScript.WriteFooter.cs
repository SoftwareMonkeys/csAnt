using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public virtual void WriteFooter()
        {
            // Add the summaries from the sub script to the outer script
            if (Summaries != null) {
                foreach (string summary in Summaries) {
                    AddSummary (summary);
                }
            }
            
            Console.WriteLine ("");
            Console.WriteLine (GetIndentSpace (Indent) + "// Finished executing script: " + ScriptName);
            if (IsVerbose)
                WriteScriptStack (GetParentScriptList());
            Console.WriteLine (GetIndentSpace (Indent) + "// --------------------------------------------------");

        }
    }
}

