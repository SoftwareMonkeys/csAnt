using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void SetUp ()
        {
            if (!IsSetUp) {
                if (SetUpper == null)
                    throw new Exception("SetUpper property has not yet been set.");

                SetUpper.SetUp ();
                IsSetUp = true;
            }
        }
	}
}

