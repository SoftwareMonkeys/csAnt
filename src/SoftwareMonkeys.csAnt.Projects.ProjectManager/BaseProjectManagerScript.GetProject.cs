using System;
using System.Linq.Dynamic;

namespace SoftwareMonkeys.csAnt.Projects.ProjectManager
{
	public partial class BaseProjectManagerScript
	{
		public void GetProject(
			string groupName,
			string projectName
		)
		{
			var accessNode = GetNode ("Access");

			if (accessNode != null)
			{
				if (accessNode.Nodes.Count == 0)
				{
					Console.WriteLine ("No access nodes found.");
				}
				else
				{
					foreach (var node in accessNode.Nodes.Values)
					{
						throw new NotImplementedException();
					}
				}
			}
		}
	}
}

