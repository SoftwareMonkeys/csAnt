using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        IConsoleWriter ConsoleWriter { get; set; }
    }
}

