using System;
using System.IO;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        /// <summary>
        /// Finds the scripts matching the provided pattern.
        /// </summary>
        /// <returns>
        /// An array of the (short) script names.
        /// </returns>
        /// <param name='pattern'>
        /// The pattern used to match script names.
        /// </param>
        public string[] FindScripts (string pattern)
        {
            return ScriptFinder.Find(pattern);
        }
    }
}

