namespace Zadanie3.Devices
{
    public class Copier : BaseDevice
    {
        public new int Counter { get; set; }
        public int PrintCounter { get; set; }
        public int ScanCounter { get; set; }
        public Printer Printer { get; set; }
        public Scanner Scanner { get; set; }

        public Copier()
        {
            Printer = new Printer();
            Scanner = new Scanner();
        }
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
                Scanner.PowerOn();
                ScanCounter++;
                Scanner.Scan(out document, formatType);
                Scanner.PowerOff();
            }
            else
            {
                document = null;
            }
        }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                Printer.PowerOn();
                PrintCounter++;
                Printer.Print(in document);
                Printer.PowerOff();
            }
        }

        public void ScanAndPrint()
        {
            if (state == IDevice.State.on)
            {
                Scan(out var document, IDocument.FormatType.JPG);
                Print(in document);
            }
        }
    }
}
