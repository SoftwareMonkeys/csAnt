// Code courtesy of http://www.codeproject.com/Articles/3111/C-NET-Command-Line-Arguments-Parser (but customized)

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Specialized;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// Arguments class
	/// </summary>
	public class Arguments
	{
		// Variables
		private StringDictionary Parameters;

		// Constructor
		public Arguments(string[] Args)
		{
		    Parameters = new StringDictionary();
		    Regex splitter = new Regex(@"^-{1,2}|^/|=|:",
			RegexOptions.IgnoreCase|RegexOptions.Compiled);

		    Regex remover = new Regex(@"^['""]?(.*?)['""]?$",
			RegexOptions.IgnoreCase|RegexOptions.Compiled);

		    string[] parts;

		    // Valid parameters forms:
		    // {-,/,--}param{ ,=,:}((",')value(",'))
		    // Examples: 
		    // -param1 value1 --param2 /param3:"Test-:-work" 
		    //   /param4=happy -param5 '--=nice=--'
		    foreach(string txt in Args)
		    {
		    	string parameter = null;

				// Look for new parameters (-,/ or --) and a
				// possible enclosed value (=,:)
				parts = splitter.Split(txt,3);

				switch(parts.Length)
				{
					// Found a value (for the last parameter 
					// found (space separator))
					case 1:
						parameter = parts[0];

					    if(parameter != null)
					    {
						    Parameters.Add(Guid.NewGuid().ToString(), parameter);

							parameter=null;
					    }
					    // else Error: no parameter waiting for a value (skipped)
					    break;

					// Found just a parameter
					case 2:
					    // The last parameter is still waiting. 
					    // With no value, set it to true.
					    if(parameter!=null)
					    {
							if(!Parameters.ContainsKey(parameter)) 
							    Parameters.Add(parameter, "true");
					    }
					    parameter=parts[1];
					    break;

					// Parameter with enclosed value
					case 3:
					    // The last parameter is still waiting. 
					    // With no value, set it to true.
					    if(parameter != null)
					    {
							if(!Parameters.ContainsKey(parameter)) 
							    Parameters.Add(parameter, "true");
					    }

					    parameter = parts[1];

					    // Remove possible enclosing characters (",')
					    if(!Parameters.ContainsKey(parameter))
					    {
							parts[2] = remover.Replace(parts[2], "$1");
							Parameters.Add(parameter, parts[2]);
					    }
					    break;
				}
		    }
		}

		// Retrieve a parameter value if it exists 
		// (overriding C# indexer property)
		public string this [string parameter]
		{
		    get
		    {
				return(Parameters[parameter]);
		    }
		}

		public string this [int index]
		{
		    get
		    {
				var i = 0;
				foreach (var v in Parameters.Values)
				{
					if (i == index)
						return (string)v;

					i++;
				}
				return null;
		    }
		}

		public bool Contains(string parameter)
		{
			return Parameters.ContainsKey(parameter);
		}
	}
}



