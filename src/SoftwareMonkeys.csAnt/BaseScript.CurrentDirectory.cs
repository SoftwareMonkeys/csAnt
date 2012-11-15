using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseScript
	{
		private string currentDirectory = String.Empty;
		public string CurrentDirectory
		{
			get
			{
				if (String.IsNullOrEmpty(currentDirectory))
				{
					currentDirectory = Environment.CurrentDirectory;
				}
				return currentDirectory;
			}
			set
			{
				currentDirectory = value;
				Environment.CurrentDirectory = value;
			}
		}
	}
}
