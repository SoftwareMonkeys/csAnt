using System;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public List<string> Summaries = new List<string>();

		public void AddSummary(string text)
		{
			Summaries.Add(text);
		}
	}
}

