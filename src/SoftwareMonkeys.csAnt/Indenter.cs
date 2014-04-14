using System;
using System.Text;


namespace SoftwareMonkeys.csAnt
{
    public class Indenter : IIndenter
    {
        public int Indent { get;set; }
        
        public Indenter (int indent)
        {
            Indent = indent;
        }

        public Indenter ()
        {
        }

        public string GetIndentSpace()
        {
            return GetIndentSpace(Indent);
        }

        public string GetIndentSpace (int indent)
        {
            var indentSpaceBuilder = new StringBuilder();

            for (int i = 0; i < indent; i++) {
                indentSpaceBuilder.Append("    ");
            }

            return indentSpaceBuilder.ToString();
        }
    }
}

