using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;


namespace SoftwareMonkeys.csAnt.External.Nuget
{
    [Serializable]
    [XmlType("package")]
    public class NugetPackageSpec
    {
        [XmlElement("metadata")]
        public NugetPackageSpecMetaData MetaData { get;set; }

        [XmlIgnore]
        public string FilePath { get;set; }

        [XmlIgnore]
        public NugetPackageSpecSaver Saver { get;set; }

        [XmlArray("files")]
        [XmlArrayItem("file")]
        public List<NugetPackageFile> Files  { get;set; }

        [XmlIgnore]
        public NugetPackageSpecFileNamer FileNamer { get;set; }

        public NugetPackageSpec ()
        {
            Construct();
        }

        public NugetPackageSpec (string id)
        {
            Construct();
            MetaData.ID = id;
        }

        public void Construct()
        {
            MetaData = new NugetPackageSpecMetaData();
            Files = new List<NugetPackageFile>();
            Saver = new NugetPackageSpecSaver();
            FileNamer = new NugetPackageSpecFileNamer();
        }

        public void Save()
        {
            if (String.IsNullOrEmpty(FilePath))
                FilePath = FileNamer.GetSpecFileName(Environment.CurrentDirectory, MetaData.ID);

            Saver.Save (this, FilePath);
        }

        public void AddFiles(params string[] files)
        {
            foreach (var file in files)
            {
                // TODO: Inject these objects using a creator component stored on a property
                var fileInfo = new NugetPackageFile()
                {
                    Src = file.Replace(Environment.CurrentDirectory, "")
                        .Replace(@"\", "/")
                };

                Files.Add(fileInfo);

            }
        }

    }
}

