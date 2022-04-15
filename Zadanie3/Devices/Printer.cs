using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3.Devices
{
    public class Printer : BaseDevice, IPrinter
    {
        public int PrintCounter { get; set; }
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
        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                PrintCounter++;
                Console.WriteLine($"{DateTime.Today:dd.MM.yyyy} {DateTime.Now:T} Print: {document.GetFileName()}");
            }
        }
    }
}
