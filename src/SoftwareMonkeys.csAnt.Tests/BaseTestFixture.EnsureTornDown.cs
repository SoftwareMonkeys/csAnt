using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public partial class BaseTestFixture
    {
        public void EnsureTornDown()
        {
            if (!IsTornDown)
                TearDown();
        }
    }
}

