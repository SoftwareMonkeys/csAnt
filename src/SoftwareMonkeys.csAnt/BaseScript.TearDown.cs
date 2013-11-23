using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void TearDown ()
        {
            if (!IsTornDown) {
                TearDowner.TearDown ();
                IsTornDown = true;
            }
		}
	}
}

