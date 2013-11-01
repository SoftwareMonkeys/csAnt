using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GitAdd(string file)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Adding file to git:");
			Console.WriteLine (file);
			Console.WriteLine ("");

			Git ("add", file);
		}
		
		public void GitAddToDirectory(string path, string file)
		{
			
			Console.WriteLine ("");
			Console.WriteLine ("Adding file to git:");
			Console.WriteLine (file);
			Console.WriteLine ("");

			GitDirectory (path, "add", file);
		}
	}
}

