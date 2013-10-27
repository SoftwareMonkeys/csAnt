using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseProjectScript
	{
		/// <summary>
		/// Gets/sets the root directory of the current project.
		/// </summary>
		public string ProjectDirectory
		{
			get
			{
				return CurrentDirectory;
			}
			set
			{
				CurrentDirectory = value;
			}
		}
		
	}
}
