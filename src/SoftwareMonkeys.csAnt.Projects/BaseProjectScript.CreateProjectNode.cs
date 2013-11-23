using System;
using SoftwareMonkeys.FileNodes;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
        public void CreateProjectNode ()
        {
            CreateProjectNode(CurrentDirectory);
        }

        public void CreateProjectNode (string path)
        {
            var name = Path.GetFileNameWithoutExtension(path);
            CreateProjectNode(path, name);
        }

		public void CreateProjectNode (string location, string projectName)
        {
            CreateNode(location, projectName);
		}
	}
}

