using System;
using SoftwareMonkeys.FileNodes;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SoftwareMonkeys.csAnt
{
	public interface IScript : IDisposable
	{
		#region Properties
		string ScriptName { get;set; }

		string CurrentDirectory { get;set; }

		bool IsVerbose { get;set; }

		string TimeStamp { get;set; }

		FileNode CurrentNode { get;set; }

		bool IsError { get;set; }

		ConsoleWriter Console { get; set; }

		// TODO: Check if needed
		int Indent { get; set; }
		#endregion

		#region Imports properties
		string ImportedDirectory { get;set; }

		string GetImportedDirectory();
		#endregion

		#region Start
		void SetUp();

		bool Start(string[] args);

		void TearDown();
		#endregion

		#region Initialize
		void Initialize(string scriptName);

		void Initialize(string scriptName, ConsoleWriter consoleWriter);
		#endregion

		#region Summaries
		List<string> Summaries { get;set; }

		void AddSummary(string text);

		void OutputSummaries();
		#endregion

		#region IO
		void MoveDirectory(string from, string to);

		void CopyDirectory(string from, string to);

		string GetTemporaryDirectory();
		string GetTmpDir();

		string GetTmpFile();

		void EnsureDirectoryExists(string path);

		string[] FindFiles(string directory, string[] patterns);
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

		#region Execute functions
		void ExecuteScript(string scriptName);
		#endregion

		#region Time stamp functions
		string GetTimeStamp();
		#endregion

		#region Start process functions
		Process StartProcess(string command, string[] arguments);

		Process StartNewProcess(string command, string[] arguments);
		#endregion

                #region Relocation
                void Relocate(string dir);
                #endregion
	}
}

