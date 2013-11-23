using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public partial class BaseTestFixture
    {
        public virtual string GetWorkingDirectory()
        {
            var tdc = new TemporaryDirectoryCreator (
                Environment.CurrentDirectory,
                TimeStamp,
                IsVerbose
            );

            return tdc.GetTmpDir ();
        }
    }
}

