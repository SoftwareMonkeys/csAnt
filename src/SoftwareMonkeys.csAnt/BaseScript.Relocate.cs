using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        /// <summary>
        /// Changes the CurrentDirectory to the new location and updates other properties.
        /// </summary>
        public virtual void Relocate (string path)
        {
            Relocator.Relocate(path);


        }
    }
}

