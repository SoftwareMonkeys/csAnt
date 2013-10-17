using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public void CopyTestProject (string sourceDirectory, string destinationDirectory)
		{
			var patterns = new string[]{
				"lib/**",
				"src/**",
				"*"
			};

			foreach (string pattern in patterns) {
				foreach (string file in Directory.GetFiles(sourceDirectory, pattern))
					Console.WriteLine (file);
			}
		}
	}
}

