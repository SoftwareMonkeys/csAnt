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
                if (indenter == null)
                    indenter = new Indenter();
                return indenter;
            }
            set { indenter = value; }
        }
    }
}

