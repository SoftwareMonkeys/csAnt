using System;

namespace SoftwareMonkeys.csAnt
{
    public interface IConsoleWriter
    {
        void Write(string message);

        void WriteLine(string message);
    }
}

