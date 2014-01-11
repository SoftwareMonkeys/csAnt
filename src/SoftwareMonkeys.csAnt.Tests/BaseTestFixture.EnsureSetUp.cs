using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public partial class BaseTestFixture
    {
        public void EnsureSetUp()
        {
            if (!IsSetUp)
                SetUp();
        }
    }
}

