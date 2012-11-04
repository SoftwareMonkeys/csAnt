using System;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// 
	/// </summary>
	public class GroupNodeNotFoundException : Exception
	{
		public GroupNodeNotFoundException() : base("No group node found. Ensure a [GroupName].node file exists in the group/company directory (the directory one step up from the project).")
		{
		}
	}
}
