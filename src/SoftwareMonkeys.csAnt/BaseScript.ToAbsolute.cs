using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string ToAbsolute (string relativePath)
        {
            return PathConverter.ToAbsolute(relativePath);
		}
        
        public string ToAbsolute (string basePath, string relativePath)
        {
            return PathConverter.ToAbsolute(relativePath);
        }
	}
}

