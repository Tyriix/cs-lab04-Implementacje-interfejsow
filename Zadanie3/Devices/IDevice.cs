using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    public interface IDevice
    {
        enum State
        {
            on,
            off
        };

        void PowerOn();
        void PowerOff();
        State GetState();

        int Counter { get; }
    }
}
