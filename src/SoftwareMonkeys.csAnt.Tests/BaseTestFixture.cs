using System;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
	public abstract partial class BaseTestFixture
	{
        public DateTime Time { get;set; }
        public string TimeStamp { get;set; }
      
        public bool IsVerbose { get;set; }

        private string workingDirectory;
        public string WorkingDirectory {
            get {
                if (String.IsNullOrEmpty(workingDirectory))
                    return GetWorkingDirectory();
                return workingDirectory; }
            set { workingDirectory = value; }
        }

        public List<IScript> Scripts = new List<IScript>();

        /// <summary>
        /// A flag indicating whether to auto initialize the test.
        /// Initialization moves the test to a temporary directory, etc.
        /// </summary>
        public bool AutoInitialize = true;

        public bool IsSetUp { get; set; }

        public bool IsTornDown { get; set; }

		public BaseTestFixture ()
		{
#if DEBUG
            IsVerbose = true;
#else
            IsVerbose = false;
#endif
		}

        public BaseTestFixture (bool autoSetUp)
        {
#if DEBUG
            IsVerbose = true;
#else
            IsVerbose = false;
#endif
            if (autoSetUp)
                SetUp ();
        }
    }
}