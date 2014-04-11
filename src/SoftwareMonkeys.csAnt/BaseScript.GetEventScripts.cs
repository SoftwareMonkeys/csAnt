using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public string[] GetEventScripts (string eventName)
        {
            return EventRaiser.GetEventScripts(eventName);
        }
    }
}

