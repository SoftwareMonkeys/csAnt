using System;
using System.IO;
using System.IO.Compression;
using SoftwareMonkeys.csAnt.IO.Compression;

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
            // TODO: Keep zipper on a property
            return new FileZipper().Unzip(zipFilePath, destinationPath, subPath);
	    }

	}
}

