using System;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public List<string> Summaries { get; set; }

		public void AddSummary(string text)
		{
			if (Summaries == null)
				Summaries = new List<string>();

			Summaries.Add(text);
		}

		public void OutputSummaries ()
		{
			if (Summaries != null
			    && Summaries.Count > 0) {
				Console.WriteLine ("");
				Console.WriteLine ("Summary:");

				var i = 1;

				foreach (string summary in Summaries) {
					Console.WriteLine (i + ") " + summary);
					i++;
				}

				Console.WriteLine ("");
			}
		}
	}
}

