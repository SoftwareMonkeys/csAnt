using System;
namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        private ScriptEventRaiser eventRaiser;
        public ScriptEventRaiser EventRaiser
        {
            get
            {
                if (eventRaiser == null)
                    eventRaiser = new ScriptEventRaiser(this);
                return eventRaiser;
            }
            set { eventRaiser = value; }
        }
    }
}

