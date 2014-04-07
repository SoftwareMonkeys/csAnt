using System;
namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        private Indenter indenter;
        public Indenter Indenter
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

