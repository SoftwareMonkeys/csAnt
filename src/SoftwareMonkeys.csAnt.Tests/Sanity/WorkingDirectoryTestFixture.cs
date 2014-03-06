using System;
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
		}
		
		[Test]
		public void Test_WorkingDirectoryProperty_ValueIsCorrect()
		{
			Console.WriteLine("");
			Console.WriteLine("Working directory:");
			Console.WriteLine(WorkingDirectory);
			Console.WriteLine("");
			
			Assert.IsTrue(WorkingDirectory.EndsWith("/csAnt/"), "Invalid working directory. Does not end with '/csAnt/' as expected.");
		}
	}
}
