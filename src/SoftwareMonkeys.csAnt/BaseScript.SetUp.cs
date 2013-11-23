using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void SetUp ()
        {
            if (!IsSetUp) {
                SetUpper.SetUp ();
                IsSetUp = true;
            }
        }
	}
}

