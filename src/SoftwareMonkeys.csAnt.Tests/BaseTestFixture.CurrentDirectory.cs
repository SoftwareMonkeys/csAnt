using System;
namespace SoftwareMonkeys.csAnt.Tests
{
    public partial class BaseTestFixture
    {
        public string CurrentDirectory
        {
            get { return WorkingDirectory; }
            set { WorkingDirectory = value; }
        }
    }
}

