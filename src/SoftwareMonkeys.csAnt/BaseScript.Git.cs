using System;
using SoftwareMonkeys.csAnt.SourceControl.Git;


namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        private Gitter git;
        public Gitter Git
        {
            get {
                if (git == null)
                    git = new Gitter();
                return git; }
            set { git = value; }
        }
    }
}

