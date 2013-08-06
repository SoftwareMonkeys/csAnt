using System;
using NUnit.Framework;
using System.IO;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class RunTestScriptsTestFixture : BaseTestFixture
	{
		/// <summary>
		/// Runs all the scripts with the "Test_" prefix.
		/// </summary>
		[Test]
		public void Test_RunAllTestScripts()
		{
			var script = new TestScript(this);

			try
			{
				script.ExecuteScript("RunTestScripts");

				if (script.IsError)
					Assert.Fail();
			}
			catch (Exception ex)
			{
				Assert.Fail();

				Console.WriteLine (ex.ToString());
			}
		}
	}
}

