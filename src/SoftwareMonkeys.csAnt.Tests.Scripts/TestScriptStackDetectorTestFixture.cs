using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class TestScriptStackDetectorTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_Detect_3()
        {
            var script1 = new TestScript("Test1", this);
            var script2 = new TestScript("Test2", this);
            var script3 = new TestScript("Test3", this);

            script2.ParentScript = script1;
            script3.ParentScript = script2;

            var stack = new TestScriptStackDetector(script3).Detect();

            Assert.AreEqual(2, stack.Count);
            Assert.AreEqual(script1.ScriptName, stack.ToArray()[0]);
            Assert.AreEqual(script2.ScriptName, stack.ToArray()[1]);
        }

        [Test]
        public void Test_Detect_1()
        {
            var script1 = new TestScript("Test1", this);

            var stack = new TestScriptStackDetector(script1).Detect();

            Assert.AreEqual(0, stack.Count);
        }
        
        [Test]
        public void Test_Detect_3_OneNormalScript()
        {
            var script1 = new TestScript("Test1", this);
            var script2 = new Script("Test2");
            var script3 = new TestScript("Test3", this);

            script2.ParentScript = script1;
            script3.ParentScript = script2;

            var stack = new TestScriptStackDetector(script3).Detect();

            Assert.AreEqual(1, stack.Count);
            Assert.AreEqual(script1.ScriptName, stack.ToArray()[0]);
        }
    }
}

