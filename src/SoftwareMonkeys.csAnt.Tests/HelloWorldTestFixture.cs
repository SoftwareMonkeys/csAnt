using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class HelloWorldTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_HelloWorldScript()
		{
			var script = GetTestScript();

			script.Grabber.GrabOriginalScriptingFiles();

			script.ExecuteScript("HelloWorld");

			Assert.IsFalse(script.IsError);
		}
	}
}

