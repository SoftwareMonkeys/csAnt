using System;

namespace SoftwareMonkeys.csAnt.IO
{
    public class FileFinderNotInitializedException : Exception
    {
        public FileFinderNotInitializedException () : base("The FileFinder has not been initialized. Call InitializeFileFinder(IFileFinder).")
        {
        }
    }
}

