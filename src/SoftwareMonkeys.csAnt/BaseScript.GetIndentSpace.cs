using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string GetIndentSpace()
		{
			return GetIndentSpace(Indent);
		}

		public string GetIndentSpace (int indent)
		{
			var indentSpace = String.Empty;

			for (int i = 0; i < indent; i++) {
				indentSpace += "    ";
			}

			return indentSpace;
		}
	}
}

