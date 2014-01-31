using System;
using System.IO;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
	    public int Unzip(string zipFilePath, string destinationPath)
	    {
			return Unzip(
				zipFilePath,
			    destinationPath,
				"/"
			);
		}

	    public int Unzip (string zipFilePath, string destinationPath, string subPath)
		{
            var cmd = new UnzipCommand(
                this,
                zipFilePath,
                destinationPath,
                subPath
            );

            Execute (cmd);

            return cmd.TotalFiles;
	    }

	}
}

