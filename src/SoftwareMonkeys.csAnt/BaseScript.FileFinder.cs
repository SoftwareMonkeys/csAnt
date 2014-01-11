using System;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        private IFileFinder fileFinder;
        public IFileFinder FileFinder {
            get {
                if (fileFinder == null)
                    throw new FileFinderNotInitializedException ();
                return fileFinder;
            }
        }
    }
}

