using System;

namespace SoftwareMonkeys.csAnt
{
    public class ScriptNotFoundException : Exception
    {
        public string ScriptName { get; set; }

        public ScriptNotFoundException (string scriptName) : base("No script was found with the name '" + scriptName + "'.") 
        {
            ScriptName = scriptName;
        }
    }
}

