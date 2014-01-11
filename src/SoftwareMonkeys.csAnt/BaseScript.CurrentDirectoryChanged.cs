using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public event EventHandler<EventArgs> CurrentDirectoryChanged;

        public virtual void OnCurrentDirectoryChanged()
        {
            if (CurrentDirectoryChanged != null)
                CurrentDirectoryChanged(this, EventArgs.Empty);
        }
    }
}

