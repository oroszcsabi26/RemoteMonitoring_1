using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteMonitoring
{
    public class SimulatorCommunicator : IBaseCommunicator
    {
        private int[] m_PinOutputState;
        public bool IsConnected { get; set; }

        public string ConnectionName { get { return "Simulator COM"; } }

        public SimulatorCommunicator()
        {
            m_PinOutputState = new int[16];
        }

        public bool Connect()
        {
            return IsConnected = true;
        }

        public bool DisConnect()
        {
            return IsConnected = false;
        }

        public string SendCommandAndGetAnswer(string p_Command)
        {
            //RS0; RS1; RO0; RI0; (WO00; WO01;) + @
            string answer = "";
            Random random = new Random();
            string[] userInputSplit = p_Command.Split(';');

            for (int i = 0; i < userInputSplit.Length - 1; i++)
            {
                char command = userInputSplit[i][0];
                char pinType = userInputSplit[i][1];
                int pinNumber = userInputSplit[i][2] - '0';
                int pinValue = 0;

                if (userInputSplit[i].Length >= 4)
                {
                    pinValue = userInputSplit[i][3] - '0';
                }

                switch (command)
                {
                    case 'R':
                        {
                            if (pinType == 'I')
                            {
                                int randomButtonState = random.Next(0, 2);
                                answer += randomButtonState.ToString() + ';';
                            }
                            else if (pinType == 'O')
                            {
                                int randomButtonState = m_PinOutputState[pinValue];
                                answer += randomButtonState.ToString() + ';';
                            }
                            else if (pinType == 'S')
                            {
                                if (pinNumber == 0)
                                {
                                    double randomTemp = Math.Round(random.NextDouble() * (30 - (-10)) + (-10), 2);
                                    answer += randomTemp.ToString() + ';';
                                }
                                else if (pinNumber == 1)
                                {
                                    int randomLight = random.Next(0, 20);
                                    answer += randomLight.ToString() + ';';
                                }
                                else if (pinNumber > 1)
                                {
                                    int randomSensorVal = random.Next(0, 30);
                                    answer += randomSensorVal.ToString() + ';';
                                }
                            }
                        }
                        break;
                    //WO01;
                    case 'W':
                        {
                            if (pinType == 'O')
                            {
                                m_PinOutputState[pinNumber] = pinValue;
                                answer += pinValue.ToString() + ';';
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return answer;
        }
    }
}