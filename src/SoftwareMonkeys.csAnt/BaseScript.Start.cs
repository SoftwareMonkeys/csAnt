using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public bool Start (params string[] args)
        {
            Arguments = new Arguments(args);

            WriteHeader(args);

            SetUp();

            Run(args);

            TearDown();

            WriteFooter();

            return !IsError;
        }
	}
}

