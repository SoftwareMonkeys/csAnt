using System;
using SoftwareMonkeys.FileNodes;


namespace SoftwareMonkeys.csAnt
{
    public class LibraryManager
    {
        public LibraryGetter Getter { get;set; }

        public LibraryUrlAdder UrlAdder { get;set; }
        public LibraryNugetAdder NugetAdder { get;set; }

        public IFileNodeState Nodes { get;set; }

        public LibraryManager (
            IFileNodeState nodes
        )
        {
            Nodes = nodes;
            UrlAdder = new LibraryUrlAdder(nodes);
            NugetAdder = new LibraryNugetAdder(nodes);
            Getter = new LibraryGetter(nodes);
        }

        public void Get(string name)
        {
            Get (name, false);
        }

        public void Get(string name, bool force)
        {
            Getter.Get(name, force);
        }

        public void AddUrl(string name, string zipFileUrl)
        {
            AddUrl(name, zipFileUrl, string.Empty);
        }

        public void AddUrl(string name, string zipFileUrl, string subPath)
        {
            UrlAdder.Add(name, zipFileUrl, subPath);
        }

        public void AddNuget(string name)
        {
            AddNuget(name, name);
        }

        public void AddNuget(string name, string packageName)
        {
            NugetAdder.Add(name, packageName);
        }
    }
}

