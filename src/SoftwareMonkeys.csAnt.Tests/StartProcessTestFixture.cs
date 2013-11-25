using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class StartProcessTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_StartProcess()
		{
			var script = GetDummyScript();

            script.StartProcess ("echo", "Hello world!");

			Assert.IsTrue(script.Console.Output.Contains ("Hello world!"), "The output is incorrect.");
		}
	}
}

