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

		FileNode CurrentNode { get;set; }

		bool IsError { get;set; }

		ConsoleWriter Console { get; set; }

		int Indent { get; set; }

        bool StopOnFail { get;set; }
		#endregion

        #region Parent and stack
        IScript ParentScript { get;set; }

        Stack<string> ScriptStack { get;set; }
        #endregion

        #region Construct
        void Construct(string scriptName);

        void Construct(string scriptName, IScript parentScript);
        #endregion

        #region Original directory
        string GetOriginalDirectory();

        string OriginalDirectory { get;set; }
        #endregion

		#region Imports properties
		string ImportStagingDirectory { get;set; }

		string GetImportStagingDirectory();
		#endregion

        #region Set up
        bool IsSetUp { get;set; }

        IScriptSetUpper SetUpper { get;set; }

		void SetUp();
        #endregion
        
        #region Start
		bool Start(params string[] args);
        #endregion

        #region Tear down
        bool IsTornDown { get;set; }

        IScriptTearDowner TearDowner { get;set; }

		void TearDown();
        #endregion

		#region Summaries
		List<string> Summaries { get;set; }

		void AddSummary(string text);

		void OutputSummaries();
		#endregion

		#region IO
		void MoveDirectory(string from, string to);

        void CopyDirectory(string from, string to);
        void CopyDirectory(string from, string to, bool overwrite);
        
        string GetTmpRoot();

		string GetTemporaryDirectory();
		string GetTmpDir();

		string GetTmpFile();

		void EnsureDirectoryExists(string path);

		string[] FindFiles(string directory, string[] patterns);

        FilesGrabber FilesGrabber { get;set; }

        TemporaryDirectoryCreator TemporaryDirectoryCreator { get;set; }
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

		#region Script functions
        T ActivateScript<T>(string scriptName)
            where T : IScript;

        IScript ActivateScript(string scriptName);

        IScript ActivateScriptFromFile(string scriptFilePath);

		void ExecuteScript(string scriptName);
		#endregion

		#region Time stamp
        string TimeStamp { get;set; }

        DateTime Time { get;set; }

		string GetTimeStamp();
		#endregion

		#region Start process functions
		Process StartProcess(string command, params string[] arguments);

		Process StartNewProcess(string command, params string[] arguments);
		#endregion

        #region Relocation
        IScriptRelocator Relocator { get;set; }

        void Relocate(string dir);
        #endregion

        #region Start HTTP
        void StartHttp(string dir, string host, int port, bool autoKill);
        #endregion

        #region Sync
        void Sync(string dir1, string dir2);
        #endregion

        #region Libs
        void GetLib(string name);
        #endregion

        #region Compile
        void CompileScripts();
        #endregion

        #region Build mode
        bool IsDebug { get;set; }

        string GetBuildMode();
        #endregion

        #region Git
        void GitInit();
        void GitAdd(string filePath);
        void GitCommit();
        #endregion

        #region Nodes
        void RefreshCurrentNode();
        #endregion
	
        #region Events
        void RaiseEvent(string eventName);

        string[] GetEventScripts(string eventName);
        #endregion
    }
}

