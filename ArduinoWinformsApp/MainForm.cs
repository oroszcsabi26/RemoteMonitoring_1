using RemoteMonitoring;
using System.Diagnostics;
using System.Net.Security;
using System.Text;

namespace ArduinoWinformsApp
{
    public partial class MainForm : Form
    {
        private IBaseCommunicator m_Communicator;
        private ArduinoUno m_ArduinoUno;
        private LoadBackForm m_form2;
        private OnlineDataForm m_form3;
        private List<PeripheryState> m_PreviusState;
        Stopwatch stopwatch;
        string[] arduinoAnswerSplit;
        double referenceTemp = 22.0;
        public MainForm()
        {
            InitializeComponent();
            m_Communicator = new SimulatorCommunicator();
            m_ArduinoUno = new ArduinoUno();
            m_PreviusState = new List<PeripheryState>();
            m_form2 = new LoadBackForm();
            stopwatch = new Stopwatch();
            m_ArduinoUno.AddDigitalOutput("O0");
            m_ArduinoUno.AddDigitalInput("I0");
            m_ArduinoUno.AddSensorInput("S0");
            m_ArduinoUno.AddSensorInput("S1");
        }

        private void arduinoRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            m_Communicator = new SerialCommunicator();
        }

        private void SimulatorButton_CheckedChanged(object sender, EventArgs e)
        {
            m_Communicator = new SimulatorCommunicator();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (m_Communicator.Connect())
            {
                streamTimer.Enabled = true;
                tbxconnectinfo.Text = $"Connected to the port: {m_Communicator.ConnectionName}";
            }
            else
            {
                streamTimer.Enabled = false;
                tbxconnectinfo.Text = "Can't connect to the port";
            }
        }

        private void btnDisConnect_Click(object sender, EventArgs e)
        {
            streamTimer.Enabled = false;
            m_Communicator.DisConnect();
            tbxconnectinfo.Text = $"DisConnected from the Device";
        }

        private void btnSendCommand_Click(object sender, EventArgs e)
        {
            string userInput = tbxCommand.Text;
            if (m_Communicator.IsConnected)
            {
                string answer = m_Communicator.SendCommandAndGetAnswer(userInput);
                m_ArduinoUno.SetPinsValue(answer);

                if (userInput.Contains("WO01"))
                {
                    LedBtnClick.Text = "LedOff";
                }
                else
                {
                    LedBtnClick.Text = "LedOn";
                }
            }
        }

        private void StreamTimer_Tick(object sender, EventArgs e)
        {
            if (!stopwatch.IsRunning)
            {
                stopwatch.Start();
            }

            if (m_Communicator.IsConnected)
            {
                string answer = m_Communicator.SendCommandAndGetAnswer(m_ArduinoUno.GetReadCommand());

                arduinoAnswerSplit = answer.Split(";");
                if (arduinoAnswerSplit[2].Contains('.'))
                {
                    arduinoAnswerSplit[2] = arduinoAnswerSplit[2].Replace('.', ',');
                }

                if (Double.Parse(arduinoAnswerSplit[2]) > referenceTemp)
                {
                    m_Communicator.SendCommandAndGetAnswer("WO01;@");
                }
                else if (LedBtnClick.Text != "LedOff")
                {
                    m_Communicator.SendCommandAndGetAnswer("WO00;@");
                }

                m_ArduinoUno.SetPinsValue(answer);

                if (stopwatch.ElapsedMilliseconds >= 6000)
                {
                    m_ArduinoUno.SaveArduinoData();
                    stopwatch.Restart();
                }
                tbxAnswares.Text += answer + Environment.NewLine;
                tbxAnswares.SelectionStart = tbxAnswares.Text.Length;
                tbxAnswares.ScrollToCaret();

                m_form3?.UpdatePlot(m_ArduinoUno.GetArduinoPeripheryState());
            }
            else
            {
                MessageBox.Show("The communicator is disconnected!");
            }
        }

        private void LoadBackData_Click(object sender, EventArgs e)
        {
            if (!m_Communicator.IsConnected)
            {
                string m_path = AppDomain.CurrentDomain.BaseDirectory;
                m_form2.CheckBoxButtonDraw(m_ArduinoUno.GetArduinoPeripheryState());
                //m_form2.LoadPlot(m_ArduinoUno.LoadBackState(m_path));
                m_form2.GetPath(m_path);
                m_form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please click disconnect befor LoadBack!");
            }
        }

        private void LedBtnClick_Click(object sender, EventArgs e)
        {
            if (m_Communicator.IsConnected && LedBtnClick.Text == "LedOn")
            {
                string answer = m_Communicator.SendCommandAndGetAnswer("WO01;@");
                LedBtnClick.Text = "LedOff";
                m_ArduinoUno.SetPinsValue(answer);
            }
            else if (m_Communicator.IsConnected && LedBtnClick.Text == "LedOff")
            {
                string answer = m_Communicator.SendCommandAndGetAnswer("WO00;@");
                LedBtnClick.Text = "LedOn";
                m_ArduinoUno.SetPinsValue(answer);
            }
        }

        private void showLiveData_Click(object sender, EventArgs e)
        {
            m_form3 = new OnlineDataForm();
            m_form3.CheckBoxButtonDraw(m_ArduinoUno.GetArduinoPeripheryState());
            m_form3.ShowDialog();
            m_form3 = null;
        }
    }
}
