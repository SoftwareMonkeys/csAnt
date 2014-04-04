using System;
using SoftwareMonkeys.csAnt.Imports;


namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        private Importer importer;
        public Importer Importer
        {
            get {
                if (importer == null)
                    importer = new Importer();
                return importer; }
            set { importer = value; }
        }
    }
}

