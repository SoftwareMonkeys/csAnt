using System;
using SoftwareMonkeys.FileNodes;
using System.IO;
using SoftwareMonkeys.csAnt.Commands;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void AddLibUrl(string name, string zipFileUrl)
		{
			AddLibUrl(name, zipFileUrl, String.Empty);
		}

		public void AddLibUrl (string name, string zipFileUrl, string subPath)
		{
			Console.WriteLine ("Adding library...");
			Console.WriteLine ("Name: " + name);
			Console.WriteLine ("Url: " + zipFileUrl);
			Console.WriteLine ("Sub path: " + subPath);

            // TODO: Check if this should be injected or an instance kept on the script class
            new LibraryManager(Nodes.State).AddUrl(name, zipFileUrl, subPath);
		}

	}
}

