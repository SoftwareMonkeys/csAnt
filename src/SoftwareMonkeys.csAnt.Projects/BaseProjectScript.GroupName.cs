﻿using System;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		/// <summary>
		/// Gets the name of the group/company/organization that the project belongs to.
		/// The name comes from the "Name" property in the [GroupName].node file, in the directory one step above the project directory.
		/// </summary>
		public string GroupName
		{
			get
            {
                if (CurrentNode != null && CurrentNode.Properties.ContainsKey ("Group"))
                    return CurrentNode.Properties ["Group"];
                else {
                    if (GroupNode == null)
                        throw new Exception ("GroupNode is null");
                    return GroupNode.Name;
                }
            }
		}
	}
}
