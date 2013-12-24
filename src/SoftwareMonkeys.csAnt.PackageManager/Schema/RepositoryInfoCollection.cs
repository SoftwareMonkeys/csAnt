using System;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.PackageManager.Schema.Repository
{
    [Serializable]
    [XmlType("Repositories")]
    public class RepositoryInfoCollection : List<RepositoryInfo>
    {
        public RepositoryInfo this [string name] {
            get {
                foreach (var repo in this)
                    if (repo.Name == name)
                        return repo;
                return null;
            }
        }

        public RepositoryInfoCollection ()
        {
        }

        public bool Contains (string query)
        {
            var repo = from r in this
                where r.Name == query
                    || r.Path == query
                select r;

            return repo != null;
        }
    }
}

