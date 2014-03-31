using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public partial class BaseTestFixture
    {
        public virtual void Construct()
        {
            Nodes = new NodeManager();
        }
    }
}

