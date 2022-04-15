using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadanie3.Documents;

namespace Zadanie3.Devices
{
    public class Scanner : BaseDevice, IScanner
    {
        public int ScanCounter { get; set; }
        public new int Counter { get; set; }
        public new void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                Counter++;
                base.PowerOn();
            }
        }
        public new void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                base.PowerOff();
            }
        }
        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            if (state == IDevice.State.on)
            {
                ScanCounter++;
                if (formatType == IDocument.FormatType.PDF)
                {
                    document = new PDFDocument($"PDFScan{ScanCounter}.pdf");
                }
                else if (formatType == IDocument.FormatType.JPG)
                {
                    document = new ImageDocument($"ImageScan{ScanCounter}.jpg");
                }
                else
                {
                    document = new TextDocument($"TextScan{ScanCounter}.txt");
                }
                Console.WriteLine($"{DateTime.Today:dd.MM.yyyy} {DateTime.Now:T} Scan: {document.GetFileName()}");
            }
            else
            {
                document = null;
            }
        }
    }
}
