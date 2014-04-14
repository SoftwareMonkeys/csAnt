using System;
namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        private IIndenter indenter;
        public IIndenter Indenter
        {
            get
            {
                return indenter;
            }
            set { indenter = value; }
        }
    }
}

