using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadanie1;

namespace Zadanie2
{
    public interface IFax
    {
        void Send(out IDocument document);
        void Receive(in IDocument document);
    }
}
