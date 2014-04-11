using System;
using System.Collections.Generic;


namespace SoftwareMonkeys.csAnt
{
    public class ScriptExecutor : IScriptExecutor
    {
        public IScriptActivator Activator { get;set; }

        public IScript ParentScript { get;set; }

        private string workingDirectory;
        public string WorkingDirectory
        {
            get
            {
                if (String.IsNullOrEmpty(workingDirectory))
                    return Environment.CurrentDirectory;
                else
                    return workingDirectory;
            }
            set { workingDirectory = value; }
        }
        
        public ScriptExecutor ()
        {
            Activator = new ScriptActivator();
        }

        public ScriptExecutor (string workingDirectory)
        {
            Activator = new ScriptActivator();
            WorkingDirectory = workingDirectory;
        }

        public ScriptExecutor (IScript parentScript)
        {
            ParentScript = parentScript;
            Activator = new ScriptActivator(parentScript);
        }

        public IScript Execute(string scriptName)
        {
            return Execute(scriptName, new string[]{});
        }
        
        public IScript Execute (string scriptName, params string[] args)
        {
            var script = Activator.ActivateScript(scriptName);
         
            return Execute(script, args);
        }

        public IScript Execute (IScript script, params string[] args)
        {
            script.SetUp();

            script.Start (args);

            script.TearDown();

            // TODO: Reimplement summaries
            // Add the summaries from the sub script to the outer script
            /*if (script.Summaries != null) {
                foreach (string summary in script.Summaries) {
                    AddSummary (summary);
                }
            }*/

            // TODO: Reimplement
            // If the target script ran into an error then recognize that error in the current script
            //IsError = script.IsError;

            // TODO: Is this needed?
            //ConsoleWriter.AppendOutput (script.ConsoleWriter.Output);

            return script;
        }

    }
}

