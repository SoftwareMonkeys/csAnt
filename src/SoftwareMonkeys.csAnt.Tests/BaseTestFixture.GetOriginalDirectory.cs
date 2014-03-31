using System;
namespace SoftwareMonkeys.csAnt.Tests
{
    public partial class BaseTestFixture
    {
        public string GetOriginalDirectory()
        {
            // TODO: See if there's a simpler/faster way to get the original directory without creating a whole script object for it
            return GetDummyScript().OriginalDirectory;
        }
    }
}

