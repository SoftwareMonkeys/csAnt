using System;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Unit;

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

