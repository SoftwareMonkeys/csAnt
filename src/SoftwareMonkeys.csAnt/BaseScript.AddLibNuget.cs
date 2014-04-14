using System;
using SoftwareMonkeys.FileNodes;
using System.IO;
using SoftwareMonkeys.csAnt.Commands;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void AddLibNuget (string name)
        {
            AddLibNuget(name, name);
        }

        public void AddLibNuget (string name, string packageName)
        {
            Console.WriteLine ("Adding library...");
            Console.WriteLine ("Name: " + name);
            Console.WriteLine ("Package name: " + packageName);

            // TODO: Check if this should be injected or an instance kept on the script class
            new LibraryManager(Nodes.State).AddNuget(name, packageName);
        }

    }
}

