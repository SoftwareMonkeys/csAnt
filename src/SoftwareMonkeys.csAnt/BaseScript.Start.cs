using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public bool Start(params string[] args)
        {
            WriteHeader();

            SetUp();

            Run(args);

            TearDown();

            WriteFooter();

            return !IsError;
        }
	}
}

