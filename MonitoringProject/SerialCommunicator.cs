using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RemoteMonitoring
{
    public class SerialCommunicator : IBaseCommunicator
    {
        private SerialPort ArduinoPort = null;
        private string m_PortName;

        public bool IsConnected { get; set; }
        public string ConnectionName { get { return m_PortName; } }

        public SerialCommunicator()
        {
            IsConnected = false;
            m_PortName = "Unknown";
        }

        public bool Connect()
        {
            string[] portNames = SerialPort.GetPortNames();

            if (portNames.Length > 0)
            {
                Console.WriteLine("Available serial ports:");

                foreach (var portName in portNames)
                {
                    Debug.WriteLine(portName);
                }

                foreach (string portName in portNames)
                {
                    try
                    {
                        m_PortName = portName;
                        ArduinoPort = new SerialPort(portName, 9600);
                        ArduinoPort.Open();
                        ArduinoPort.ReadTimeout = 500;
                        ArduinoPort.WriteTimeout = 500;

                        ArduinoPort.Write("H;@");
                        string portAnswer = ArduinoPort.ReadLine();
                        Debug.WriteLine(portAnswer);

                        if (portAnswer.Contains("HelloArduino;"))
                        {
                            m_PortName = portName + " ->Arduino Port";
                            Debug.WriteLine($"Connetced to the Arduino successfully. Portname:{portName}");
                            return IsConnected = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"I couldn't open {portName} - {ex.Message}");
                        if (ArduinoPort != null && ArduinoPort.IsOpen)
                        {
                            ArduinoPort.Close();
                        }
                    }

                }
            }
            else
            {
                Debug.WriteLine("No serial ports available.");
            }
            return IsConnected = false;
        }

        public bool DisConnect()
        {
            ArduinoPort.Close();
            return IsConnected = false;
        }

        public string SendCommandAndGetAnswer(string p_Command)
        {
            try
            {
                SendCommand(p_Command);
                return GetAnswer();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                Console.WriteLine(ex);
                return error;
            }
        }

        public void SendCommand(string command)
        {
            ArduinoPort.Write(command);
        }

        public string GetAnswer()
        {
            return ArduinoPort.ReadLine();
        }
    }
}
