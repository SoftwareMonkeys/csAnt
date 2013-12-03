using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void RaiseEvent (string eventName)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Raising '" + eventName + "' event");
            Console.WriteLine ("");

            var eventScripts = GetEventScripts(eventName);

            foreach (var script in eventScripts) {
                ExecuteScript(script);
            }
        }
    }
}

