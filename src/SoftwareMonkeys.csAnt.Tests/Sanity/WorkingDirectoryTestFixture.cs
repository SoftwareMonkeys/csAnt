using System;
using System.IO;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests.Sanity
{
	/// <summary>
	/// 
	/// </summary>
	[TestFixture]
	public class WorkingDirectoryTestFixture : BaseSanityTestFixture
	{
		public WorkingDirectoryTestFixture()
		{
			// Enable auto initialization so the results of initialization can be checked.
			AutoInitialize = true;
		}
		
		[Test]
		public void Test_WorkingDirectoryProperty_ValueIsCorrect()
		{
			Console.WriteLine("");
			Console.WriteLine("Working directory:");
			Console.WriteLine(WorkingDirectory);
			Console.WriteLine("");
			
			var key = Path.DirectorySeparatorChar
				+ "csAnt";
			
			Assert.IsTrue(WorkingDirectory.EndsWith(key), "Invalid working directory. Does not end with '/csAnt/' as expected.");
		}
	}
}
