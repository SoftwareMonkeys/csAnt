using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
	public interface IScript
	{
		string CurrentDirectory { get;set; }

		FileNode CurrentNode { get;set; }

		bool IsError { get;set; }

		bool Start(string[] args);

		#region IO
		void MoveDirectory(string from, string to);

		string GetTemporaryDirectory();
		string GetTmpDir();

		string GetTmpFile();
		#endregion

		#region Downloads
		string Download(string downloadUrl, string localDestination);

		void DownloadAndUnzip(string zipFileUrl, string localDestination);

		void DownloadAndUnzip(string zipFileUrl, string zipFileLocalPath, string localDirectory, string subPath, bool force);
		#endregion

		#region Zip functions
		void Zip(string[] filePatterns, string zipFile);
		
		int Unzip(string zipFile, string destination);

		int Unzip(string zipFile, string destination, string subPath);
		#endregion

		#region Error functions
		void Error(string message);
		#endregion
	}
}

