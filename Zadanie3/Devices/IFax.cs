using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3.Devices
{
    public interface IFax
    {
        void Send(out IDocument document);
        void Receive(in IDocument document);
    }
}
