using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        /// <summary>
        /// Refreshes/reloads the current node.
        /// </summary>
        public void RefreshCurrentNode()
        {
            CurrentNode = GetCurrentNode();
        }
    }
}

