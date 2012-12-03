using System;
namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public bool isError = false;
		public bool IsError {
			get {
				return isError;
			}
			set {
				isError = value;
			}
		}

	}
}

