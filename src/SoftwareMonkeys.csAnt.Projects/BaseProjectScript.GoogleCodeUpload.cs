using System;
using System.IO;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GoogleCodeUpload(
			string projectName,
			string filePath,
			string targetFileName
		)
		{
			var gcSecurityNode = ProjectNode.Nodes["Security"].Nodes["GoogleCode"];
			
			var username = gcSecurityNode.Properties["Username"];

			var password = gcSecurityNode.Properties["Password"];

			GoogleCodeUpload(
				projectName,
				filePath,
				targetFileName,
				username,
				password
			);

		}

		public void GoogleCodeUpload(
			string projectName,
			string filePath,
			string targetFileName,
			string username,
			string password
		)
		{
			// TODO: Check if this needs to be modified to run on Windows .NET
			var command = "mono";

			var csUploadExePath = ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
				+ "GCUpload"
				+ Path.DirectorySeparatorChar
				+ "GCUpload.exe";

			List<string> arguments = new List<string>();
			
			arguments.Add(
				"'" + csUploadExePath + "'"
			);

			arguments.Add(
				"-s:"
				+ "'" + Path.GetFileName (filePath) + "'"
			);

			arguments.Add(
				"-n:"
				+ projectName
			);

			arguments.Add(
				"-u:"
				+ username
			);

			arguments.Add(
				"-p:"
				+ password
			);
			
			arguments.Add(
				"-f:"
				+ "'" + filePath + "'" 
			);
			
			arguments.Add(
				"-t:"
				+ "'" + targetFileName + "'" 
			);

			StartProcess (
				command,
			    arguments.ToArray()
			);

		}
			
	}
}

