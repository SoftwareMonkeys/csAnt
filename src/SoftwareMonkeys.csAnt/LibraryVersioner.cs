using System;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt
{
    public class LibraryVersioner
    {
        public LibraryVersioner ()
        {
        }

        public void SetVersion(string id, string version)
        {
            Console.WriteLine("");
            Console.WriteLine("Setting library version...");
            Console.WriteLine("");
            Console.WriteLine("Updating files:");

            UpdateNuspecFiles(id, version);
            UpdateCsProjFiles(id, version);
            Console.WriteLine("");
            Console.WriteLine("Done");
            Console.WriteLine("");
        }

        public void UpdateNuspecFiles(string id, string version)
        {
            var pkgDir = Path.GetFullPath("pkg");

            foreach (var file in Directory.GetFiles(pkgDir, "*.nuspec"))
            {
                UpdateNuspecFile(id, version, file);
            }
        }

        public void UpdateNuspecFile(string id, string version, string file)
        {
            var doc = new XmlDocument();
            doc.Load(file);
            var node = doc.SelectSingleNode("//dependency[@id='" + id + "']");

            if (node != null)
            {
                node.Attributes["version"].Value = "[" + version + "]";

                Console.WriteLine("  " + PathConverter.ToRelative(file));

                doc.Save(file);
            }
        }

        public void UpdateCsProjFiles(string id, string version)
        {
            var srcDir = Path.GetFullPath("src");

            foreach (var file in Directory.GetFiles(srcDir, "*.csproj", SearchOption.AllDirectories))
            {
                UpdateCsProjFile(id, version, file);
            }
        }

        public void UpdateCsProjFile(string id, string version, string file)
        {
            XNamespace msbuild = "http://schemas.microsoft.com/developer/msbuild/2003";

            XDocument doc = XDocument.Load(file);
            var element = doc
                .Element(msbuild + "Project")
                .Elements(msbuild + "ItemGroup")
                .Elements(msbuild + "Reference")
                .Where(
                    r => (
                        r.Attribute("Include").Value == id
                        || r.Attribute("Include").Value.EndsWith("." + id)
                    )
                ).SingleOrDefault();

            if (element != null)
            {
                var elements = element.Elements().ToArray();

                if (elements.Length > 0)
                {
                    var hintPathNode = elements[0];

                    var path = hintPathNode.Value;

                    var parts = path.Split('\\');

                    for (int i = 0; i < parts.Length; i++)
                    {
                        var part = parts[i];

                        var matches = part.StartsWith(id)
                            || part.EndsWith("." + id);

                        if (matches)
                        {
                            part = id + "." + version;

                            parts[i] = part;

                            break;
                        }
                    }

                    path = String.Join("\\", parts);

                    hintPathNode.Value = path;

                    Console.WriteLine("  " + PathConverter.ToRelative(file));

                    doc.Save(file);
                }
            }
        }
    }
}

