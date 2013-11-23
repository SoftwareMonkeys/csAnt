using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class HelloWorldTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_HelloWorld()
        {
            Console.WriteLine ("Hello world!");
        }
    }
}

