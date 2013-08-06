using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GitInit()
		{
			Console.WriteLine ("Initializing repository:");
			Console.WriteLine ("Path: " + CurrentDirectory);

			Git ("init");
		}
		
		public void GitInit(string path)
		{
			Console.WriteLine ("Initializing repository:");
			Console.WriteLine ("Path: " + path);

			CurrentDirectory = path;

			Git ("init");
		}
	}
}

