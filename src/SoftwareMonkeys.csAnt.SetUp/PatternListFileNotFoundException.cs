using System;

namespace SoftwareMonkeys.csAnt.SetUp
{
    public class PatternListFileNotFoundException : Exception
    {
        public PatternListFileNotFoundException (string filePath) : base(filePath)
        {
        }
    }
}

