using System;
using System.Collections.Generic;
using System.IO;
using CSScriptLibrary;
using System.Reflection;
using System.Text;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseScript
	{
		public void ExecuteScript(string scriptName)
		{
			ExecuteScript(scriptName, new string[]{});
		}
		
		public void ExecuteScript (string scriptName, params string[] args)
        {
            var script = ActivateScript(scriptName);
         
            ExecuteScript(script, args);
        }

        public IScript ExecuteScript (IScript script, params string[] args)
        {
            script.Start (args);

            // Add the summaries from the sub script to the outer script
            if (script.Summaries != null) {
                foreach (string summary in script.Summaries) {
                    AddSummary (summary);
                }
            }

            // If the target script ran into an error then recognize that error in the current script
            IsError = script.IsError;

            Console.AppendOutput (script.Console.Output);

            return script;
        }

		public void WriteScriptStack (Stack<string> stack)
        {
            var builder = new StringBuilder ();

            if (stack.Count > 0) {
                builder.Append (GetIndentSpace (Indent) + "// Script stack: ");

                int i = 0;

                foreach (var s in stack) {
                    if (i > 0)
                        builder.Append (", ");

                    if (String.IsNullOrEmpty (s))
                        throw new Exception ("Item is null or empty.");

                    builder.Append (s);
				
                    i++;
                }
			
                builder.Append (Environment.NewLine);

                Console.Write (builder.ToString ());
            }
        }
    }
}
