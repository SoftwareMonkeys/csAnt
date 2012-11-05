using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public bool FileEquals(string filePath, string filePath2)
		{
			var fileHash1 = GetMD5HashFromFile(filePath);
			var fileHash2 = GetMD5HashFromFile(filePath2);

			return fileHash1.Equals(fileHash2);
		}

		// Function code courtesy of: http://sharpertutorials.com/calculate-md5-checksum-file/
		protected string GetMD5HashFromFile(string fileName)
		{
			FileStream file = new FileStream(fileName, FileMode.Open);
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] retVal = md5.ComputeHash(file);
			file.Close();

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < retVal.Length; i++)
			{
				sb.Append(retVal[i].ToString("x2"));
			}
			return sb.ToString();
		}
	}
}

