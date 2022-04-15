using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    public interface IPrinter : IDevice
    {
        void Print(in IDocument document);
    }
}
