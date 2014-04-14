using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void RaiseEvent (string eventName)
        {
            EventRaiser.Raise(eventName);
        }
    }
}

