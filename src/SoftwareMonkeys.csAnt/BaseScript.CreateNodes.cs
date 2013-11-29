using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public virtual void CreateNodes()
        {
            CreateNode();

            RefreshCurrentNode();
        }
    }
}

