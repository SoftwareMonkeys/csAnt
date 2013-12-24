using System;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Summaries
        List<string> Summaries { get;set; }

        void AddSummary(string text);

        void OutputSummaries();
        #endregion
    }
}

