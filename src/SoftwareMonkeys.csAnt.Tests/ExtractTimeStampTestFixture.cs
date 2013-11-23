using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class ExtractTimeStampTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_ExtractTimeStamp()
        {
            var timeStamp = "2013-11-20--9-22-12";

            var dir = "/home/user/Projects/Group/ProjectName.tmp/" + timeStamp + "/ProjectName";

            var output = ExtractTimeStamp(dir);

            Assert.AreEqual(timeStamp, output, "Invalid time stamp.");
        }
    }
}

