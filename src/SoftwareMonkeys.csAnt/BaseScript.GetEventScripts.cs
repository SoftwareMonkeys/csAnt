using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public string[] GetEventScripts (string eventName)
        {
            var pattern = String.Format ("On{0}_*", eventName);

            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Getting '" + eventName + "' event scripts.");
                Console.WriteLine ("Using pattern: " + pattern);
                Console.WriteLine ("");
            }

            var eventScripts = FindScripts (
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

