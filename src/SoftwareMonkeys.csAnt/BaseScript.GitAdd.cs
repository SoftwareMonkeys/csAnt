using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GitAdd(string file)
		{
			Console.WriteLine ("Adding file:");
			Console.WriteLine (file);

			Git ("add", file);
		}
	}
}

