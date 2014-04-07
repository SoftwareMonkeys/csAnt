using System;
using System.Collections.Generic;
using System.Text;


namespace SoftwareMonkeys.csAnt
{
    public class ScriptStackWriter
    {
        public Indenter Indenter { get;set; }
        
        public ScriptStackWriter ()
        {
            Indenter = new Indenter();
        }

        public ScriptStackWriter (Indenter indenter)
        {
            Indenter = indenter;
        }
        
        public void Write (Stack<IScript> stack)
        {
            var builder = new StringBuilder ();

            if (stack.Count > 0) {
                builder.Append (Indenter.GetIndentSpace () + "// Script stack: ");

                int i = 0;

                foreach (var s in stack) {
                    if (i > 0)
                        builder.Append (", ");

                    builder.Append (s.ScriptName);
                
                    i++;
                }
            
                builder.Append (Environment.NewLine);

                Console.Write (builder.ToString ());
            }
        }
    }
}

