using System;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.PackageManager.Schema
{
    public class PackageFileInfoCollection : List<PackageFileInfo>
    {
        public PackageFileInfoCollection ()
        {
        }

        public bool Contains (PackageFileInfo file)
        {
            foreach (var f in this) {
                if (f.Name == file.Name)
                    return true;
            }

            return false;
        }

        public string[] ToStringArray()
        {
            List<string> list = new List<string>();

            foreach (var file in this)
                list.Add (file.Name);

            return list.ToArray();
        }
    }
}

