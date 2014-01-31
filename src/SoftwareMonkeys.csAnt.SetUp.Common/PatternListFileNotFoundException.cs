using System;

namespace SoftwareMonkeys.csAnt.SetUp.Common
{
    public class PatternListFileNotFoundException : Exception
    {
        public PatternListFileNotFoundException (string filePath) : base(filePath)
        {
        }
    }
}

