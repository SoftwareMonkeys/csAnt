using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        IConsoleWriter Console { get; set; }
    }
}

