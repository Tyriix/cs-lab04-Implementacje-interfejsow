using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    public interface IDocument
    {
        enum FormatType { TXT, PDF, JPG }
        FormatType GetFormatType();
        string GetFileName();
    }
}
