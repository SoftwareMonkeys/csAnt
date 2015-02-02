using System;
using System.IO;
using SoftwareMonkeys.csAnt.Commands;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
        public FileBackup FileBackup = new FileBackup();

		public string BackupFile(string relativeFilePath)
		{
            return FileBackup.Backup (relativeFilePath);
		}
	}
}

