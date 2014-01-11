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
		public string CurrentDirectory {
            get {
                if (String.IsNullOrEmpty (currentDirectory)) {
                    currentDirectory = Environment.CurrentDirectory;
                }
                return currentDirectory;
            }
            set {
                if (IsVerbose) {
                    Console.WriteLine ("");
                    Console.WriteLine ("Setting current directory:");
                    Console.WriteLine (value);
                    Console.WriteLine ("");
                }

                if (!currentDirectory.Equals(value))
                    OnCurrentDirectoryChanged();

                currentDirectory = value;
                Environment.CurrentDirectory = value;
            }
		}
	}
}
