using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GitAddRemote(string name, string path)
		{
			Console.WriteLine("");
			Console.WriteLine("Adding git remote...");
			Console.WriteLine("Current directory:" + name);
			Console.WriteLine("Name:" + name);
			Console.WriteLine("Path:" + path);
			Console.WriteLine("");

			Git ("remote add " + name + " \"" + path + "\"");
		}
		
		public void GitAddRemoteToDirectory(string directory, string name, string path)
		{
			Console.WriteLine("");
			Console.WriteLine("Adding git remote...");
			Console.WriteLine("Current directory:" + directory);
			Console.WriteLine("Name:" + name);
			Console.WriteLine("Path:" + path);
			Console.WriteLine("");

			GitDirectory (directory, "remote add " + name + " \"" + path + "\"");
		}
	}
}

