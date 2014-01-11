using System;

namespace SoftwareMonkeys.csAnt
{
    public interface IConsoleWriter : IDisposable
    {
        string ScriptName { get;set; }

        string Output { get;set; }

        void Write(string message);

        void WriteLine(string message);
        void WriteLine();

        void AppendOutput(string message);
    }
}

