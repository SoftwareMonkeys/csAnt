using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests.Scripting.Sanity
{
    [TestFixture]
    public class TestScriptStackDetectorTestFixture : BaseScriptingSanityTestFixture
    {
        [Test]
        public void Test_Detect_3()
        {
            var script1 = GetTestScript("Test1");
            var script2 = GetTestScript("Test2", script1);
            var script3 = GetTestScript("Test3", script2);

            var stack = new TestScriptStackDetector(script3).Detect();

            Assert.AreEqual(2, stack.Count);

            Assert.AreEqual(script1.ScriptName, stack.ToArray()[0]);
            Assert.AreEqual(script2.ScriptName, stack.ToArray()[1]);
        }

        [Test]
        public void Test_Detect_1()
        {
            var script1 = GetDummyScript();

            var stack = new TestScriptStackDetector(script1).Detect();

            Assert.AreEqual(0, stack.Count);
        }
        
        [Test]
        public void Test_Detect_3_OneNormalScript()
        {
            var script1 = GetTestScript("Test1");
            var script2 = GetDummyScript("Test2", script1); // A dummy script isn't the same as a test script and won't be treated the same
            var script3 = GetTestScript("Test3", script2);

            var stack = new TestScriptStackDetector(script3).Detect();

            Assert.AreEqual(1, stack.Count);
            Assert.AreEqual(script1.ScriptName, stack.ToArray()[0]);
        }
    }
}

