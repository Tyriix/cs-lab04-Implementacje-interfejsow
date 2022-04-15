using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3.Devices
{
    public interface IScanner : IDevice
    {
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }
}
