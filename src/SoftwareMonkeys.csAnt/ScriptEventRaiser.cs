using System;
namespace SoftwareMonkeys.csAnt
{
    public class ScriptEventRaiser
    {
        public ScriptExecutor Executor { get;set; }

        public IScript ParentScript { get;set; }

        public bool IsVerbose { get;set; }

        public ScriptFinder Finder { get;set; }
        
        public ScriptEventRaiser ()
        {
            Executor = new ScriptExecutor();
            Finder = new ScriptFinder();
        }

        public ScriptEventRaiser (IScript parentScript)
        {
            Executor = new ScriptExecutor(parentScript);
            ParentScript = parentScript;
            Finder = new ScriptFinder();
        }

        public void Raise(string eventName)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Raising '" + eventName + "' event");
            Console.WriteLine ("");

            var eventScripts = GetEventScripts(eventName);

            foreach (var script in eventScripts) {
                Executor.Execute(script);
            }
        }
        
        public string[] GetEventScripts (string eventName)
        {
            var pattern = String.Format ("On{0}_*", eventName);

            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Getting '" + eventName + "' event scripts.");
                Console.WriteLine ("Using pattern: " + pattern);
                Console.WriteLine ("");
            }

            var eventScripts = Finder.Find (
                pattern
            );

            if (IsVerbose) {
                Console.WriteLine ("Found " + eventScripts.Length + " scripts:");
                foreach (var script in eventScripts)
                    Console.WriteLine ("  " + script);
            }

            return eventScripts;
        }
    }
}

