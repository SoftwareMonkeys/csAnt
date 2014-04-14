using System;
namespace SoftwareMonkeys.csAnt
{
    public interface IIndenter
    {
        int Indent { get;set; }

        string GetIndentSpace();
        string GetIndentSpace(int indent);
    }
}

