using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
        private string importStagingDirectory;
		public string ImportStagingDirectory {
            get {
                if (String.IsNullOrEmpty (importStagingDirectory))
                    importStagingDirectory = GetImportStagingDirectory ();
                return importStagingDirectory;
            }
            set { importStagingDirectory = value; }
        }
	}
}

