using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class BaseScriptingTestFixture : BaseTestFixture
    {
        public BaseScriptingTestFixture ()
        {
        }
        
        public virtual ITestScript GetTestScript ()
        {
            return GetTestScript("TestScript");
        }
        
        public virtual ITestScript GetTestScript (bool isVerbose)
        {
            return GetTestScript("TestScript", null, isVerbose);
        }
        
        public virtual ITestScript GetTestScript (string scriptName)
        {
            return GetTestScript(scriptName, null, IsVerbose);
        }
        
        public virtual ITestScript GetTestScript (string scriptName, IScript parentScript)
        {
            return GetTestScript(scriptName, parentScript, IsVerbose);
        }

        public virtual ITestScript GetTestScript(string scriptName, IScript parentScript, bool isVerbose)
        {
            return new TestScriptCreator(WorkingDirectory, isVerbose).Create(scriptName, parentScript);
        }
    }
}

