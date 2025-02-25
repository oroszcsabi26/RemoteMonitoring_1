using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RemoteMonitoring
{
    public abstract class MicrocontrollerPin : ICloneable
    {
        public string Name { get; set; }

        public MicrocontrollerPin(string name)
        {
            Name = name;
        }

        public abstract string CreateReadCommand();
        public abstract string CreateWriteCommand();

        public abstract void SetValue(string p_Value);
        public abstract string GetValueString();
        public abstract double GetValueDouble();

        public virtual object Clone()
        {
            throw new NotImplementedException();
        }
    }

    public class DigitalInput : MicrocontrollerPin, ICloneable
    {
        public bool Value { get; set; }

        public DigitalInput(string name) : base(name)
        {
            
        }
        public override object Clone()
        {
            DigitalInput clonedInput = new DigitalInput(this.Name)
            {
                Value = this.Value
            };

            return clonedInput;
        } 

        public override string CreateReadCommand()
        {
            //Name = "RD0;"
            string command = $"R{Name};";
            return command;
        }

        public override string CreateWriteCommand()
        {
            throw new NotImplementedException("CreateWriteCommand");
        }

        public override void SetValue(string p_Value)
        {
            if (p_Value == "0")
            {
                Value = false;
            }
            else
            {
                Value = true;
            }
        }

        public override string GetValueString()
        {
            return Value ? "1" : "0";
        }

        public override double GetValueDouble()
        {
            return Value ? 1 : 0;
        }
    }

    public class DigitalOutput : MicrocontrollerPin, ICloneable
    {
        public bool Value { get; set; }
        public DigitalOutput(string name) : base(name)
        {

        }

        public override object Clone()
        {
            DigitalOutput clonedOutput = new DigitalOutput(this.Name)
            {
                Value = this.Value
            };

            return clonedOutput;
        }

        public override string CreateReadCommand()
        {
            string command = $"R{Name};";
            return command;
        }

        public override string CreateWriteCommand()
        {
            //WD00 or WD01
            string command = $"W{Name}{GetValueString()};";
            return command;
        }

        public override void SetValue(string p_Value)
        {
            if (p_Value == "0")
            {
                Value = false;
            }
            else
            {
                Value = true;
            }
        }

        public override string GetValueString()
        {
            return Value ? "1" : "0";
        }

        public override double GetValueDouble()
        {
            return Value ? 1 : 0;
        }
    }

    public class SensorInput : MicrocontrollerPin, ICloneable
    {
        public double Value { get; set; }
        public SensorInput(string name) : base(name)
        {

        }

        public override object Clone()
        {
            SensorInput clonedSensorInput = new SensorInput(this.Name)
            {
                Value = this.Value
            };

            return clonedSensorInput;
        }

        public override void SetValue(string p_Value)
        {
            if (p_Value.Contains('.'))
            {
                p_Value = p_Value.Replace('.', ',');
            }

            if (double.TryParse(p_Value, out double oValue))
            {
                Value = oValue;
            }
            else
            {
                Value = 0;
            }
        }

        public override string GetValueString()
        {
            return Value.ToString("0.00");
        }


        public override double GetValueDouble()
        {
            return Value;
        }

        public override string CreateReadCommand()
        {
            string command = $"R{Name};";
            return command;
        }

        public override string CreateWriteCommand()
        {
            throw new NotImplementedException("CreateWriteCommand");
        }
    }
}
