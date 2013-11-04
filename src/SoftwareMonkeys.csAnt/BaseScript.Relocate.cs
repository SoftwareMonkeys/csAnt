using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        /// <summary>
        /// Changes the CurrentDirectory to the new location and updates other properties.
        /// </summary>
        public void Relocate(string newLocationPath)
        {
            CurrentDirectory = Path.GetFullPath(newLocationPath);
            CurrentNode = GetCurrentNode();

            ImportedDirectory = GetImportedDirectory();
            
            Console.WriteLine ("");
            Console.WriteLine ("Relocating to:");
            Console.WriteLine (newLocationPath);
            Console.WriteLine ("");
            Console.WriteLine ("Imported directory:");
            Console.WriteLine (ImportedDirectory);
            Console.WriteLine ("");
        }
    }
}

