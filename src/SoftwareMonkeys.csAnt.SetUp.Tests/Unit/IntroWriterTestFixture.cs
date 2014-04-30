using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.Tests;

namespace SoftwareMonkeys.csAnt.SetUp.Tests
{
    [TestFixture]
    public class IntroWriterTestFixture : BaseUnitTestFixture
    {
        [Test]
        public void Test_Write()
        {
            new IntroWriter().Write();
        }
    }
}

