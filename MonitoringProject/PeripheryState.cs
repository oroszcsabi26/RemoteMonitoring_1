using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteMonitoring
{
    public class PeripheryState : ICloneable
    {
        public List<MicrocontrollerPin> MicrocontrollerPins { get; set; } = new List<MicrocontrollerPin>();

        public DateTime TimeStamp { get; set; }

        public object Clone()
        {
            PeripheryState cloned = new PeripheryState();

            foreach (MicrocontrollerPin pin in MicrocontrollerPins)
            {
                cloned.TimeStamp = DateTime.Now;
                cloned.MicrocontrollerPins.Add((MicrocontrollerPin)pin.Clone());
            }
            return cloned;
        }
    }
}
