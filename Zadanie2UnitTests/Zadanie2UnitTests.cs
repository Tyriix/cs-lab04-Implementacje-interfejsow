using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Zadanie1;
using Zadanie2;

namespace Zadanie2UnitTests
{
    public class ConsoleRedirectionToStringWriter : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleRedirectionToStringWriter()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOutput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }

    [TestClass]
    public class UnitTestMultifuntionalDevice
    {
        [TestMethod]
        public void Multifunctional_GetState_StateOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, multifunctionalDevice.GetState());
        }
        [TestMethod]
        public void Multifunctional_GetState_StateOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, multifunctionalDevice.GetState());
        }
        [TestMethod]
        public void Multifunctional_Print_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        // weryfikacja, czy po wywo³aniu metody `Send` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Send`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Multifunctional_Send_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Send(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Send"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
        // weryfikacja, czy po wywo³aniu metody `Send` i w³¹czonej kopiarce w napisie pojawia siê s³owo `Send`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Multifunctional_Send_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Send(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Send"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
        // weryfikacja, czy po wywo³aniu metody `Receive` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Receive`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Multifunctional_Receive_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Receive(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Receive"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
        // weryfikacja, czy po wywo³aniu metody `Send` i w³¹czonej kopiarce w napisie pojawia siê s³owo `Receive`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Multifunctional_Receive_DeviceOn()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Receive(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Receive"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
        // weryfikacja, czy po wywo³aniu metody `Print` i wy³¹czonej kopiarce w napisie NIE pojawia siê s³owo `Print`
        // wymagane przekierowanie konsoli do strumienia StringWriter
        [TestMethod]
        public void Multifunctional_Print_DeviceOff()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multifunctionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
        [TestMethod]
        public void Multifunctional_PrintCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            multifunctionalDevice.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            multifunctionalDevice.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            // 3 wydruków, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(3, multifunctionalDevice.PrintCounter);
        }

        [TestMethod]
        public void Multifunctional_ScanCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            multifunctionalDevice.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            // 2 skany, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(2, multifunctionalDevice.ScanCounter);
        }

        [TestMethod]
        public void Multifunctional_PowerOnCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.PowerOn();

            IDocument doc1;
            multifunctionalDevice.Scan(out doc1);
            IDocument doc2;
            multifunctionalDevice.Scan(out doc2);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);

            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Scan(out doc1);
            multifunctionalDevice.PowerOn();

            // 3 w³¹czenia
            Assert.AreEqual(3, multifunctionalDevice.Counter);
        }
        [TestMethod]
        public void Multifunctional_SendCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            IDocument doc1;
            multifunctionalDevice.Send(out doc1);
            IDocument doc2;
            multifunctionalDevice.Send(out doc2);
            IDocument doc4;
            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Send(out doc3);
            multifunctionalDevice.Send(out doc4);
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Send(out doc1);
            multifunctionalDevice.PowerOn();

            // 4 wys³ania, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(4, multifunctionalDevice.SendCounter);
        }
        [TestMethod]
        public void Multifunctional_ReceiveCounter()
        {
            var multifunctionalDevice = new MultifunctionalDevice();
            multifunctionalDevice.PowerOn();

            IDocument doc1 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Receive(in doc1);
            IDocument doc2 = new PDFDocument("test.pdf");
            multifunctionalDevice.Receive(in doc2);
            IDocument doc4 = new TextDocument("tekscik.txt");
            IDocument doc3 = new ImageDocument("aaa.jpg");
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Receive(in doc3);
            multifunctionalDevice.PowerOff();
            multifunctionalDevice.Print(in doc3);
            multifunctionalDevice.Receive(in doc1);
            multifunctionalDevice.PowerOn();
            multifunctionalDevice.Receive(in doc4);
            // 4 odebrania, gdy urz¹dzenie w³¹czone
            Assert.AreEqual(4, multifunctionalDevice.ReceiveCounter);
        }
    }

}
