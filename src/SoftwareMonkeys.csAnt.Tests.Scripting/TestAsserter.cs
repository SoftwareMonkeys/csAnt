using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class TestAsserter
    {
        public ITestScript Script { get;set; }

        public TestAsserter (ITestScript script)
        {
            Script = script;
        }

        public void IsTrue (bool value, string message)
        {
            if (value != true) {
                Script.Error("Assertion failed. Expected true but was false. " + message);
            }
        }

        public void AreEqual(object expectedValue, object actualValue, string message)
        {
            if (!expectedValue.Equals(actualValue))
                Script.Error("Assertion failed. Expected '" + expectedValue + "' but was '" + actualValue + "'. " + message);
        }
    }
}

