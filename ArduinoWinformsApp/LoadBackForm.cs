using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using RemoteMonitoring;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoWinformsApp
{
    public partial class LoadBackForm : Form
    {
        CheckBox chbxPeriheries;
        List<UCPeripheryGraph> m_Graphs;
        private ArduinoUno m_ArduinoUno;
        string? m_path;

        public LoadBackForm()
        {
            InitializeComponent();
            m_Graphs = new List<UCPeripheryGraph>();
            m_ArduinoUno = new ArduinoUno();
        }


        public void CheckBoxButtonDraw(PeripheryState p_peripheryState)
        {
            int cnt = 0;

            if (panelCheckBoxButtons.Controls != null && panelCheckBoxButtons.Controls.Count > 0)
            {
                panelCheckBoxButtons.Controls.Clear();
            }

            foreach (var periphery in p_peripheryState.MicrocontrollerPins)
            {
                chbxPeriheries = new CheckBox();
                panelCheckBoxButtons.Controls.Add(chbxPeriheries);
                chbxPeriheries.Location = new System.Drawing.Point(20, 20 + (int)(cnt * 2.5 * chbxPeriheries.Font.Size));
                chbxPeriheries.Text = periphery.Name;

                cnt++;
                chbxPeriheries.CheckedChanged += CheckBoxButton_CheckedChanged;
            }
        }

        internal void LoadPlot(List <PeripheryState> peripheryStateList)
        {
            foreach (UCPeripheryGraph iGraph in m_Graphs)
            {
                iGraph.SetPlotData(peripheryStateList);
            }
        }

        public void GetPath(string p_path)
        {
            m_path = p_path;
        }

        private void CheckBoxButton_CheckedChanged(object? sender, EventArgs e)
        {
            CheckBox chbxPeriphery = sender as CheckBox;
            if (chbxPeriphery != null)
            {
                if (chbxPeriphery.Checked)
                {
                    if (!m_Graphs.Exists(c => c.PeripheryName == chbxPeriphery.Text))
                    {
                        UCPeripheryGraph ucpGraph = new UCPeripheryGraph();
                        ucpGraph.SetName(chbxPeriphery.Text);
                        m_Graphs.Add(ucpGraph);
                        loadDataPnlGraphs.Controls.Add(ucpGraph);
                    }
                }
                else
                {
                    UCPeripheryGraph ucpGraph = m_Graphs.Where(c => c.PeripheryName == chbxPeriphery.Text).FirstOrDefault();
                    if (ucpGraph != null)
                    {
                        m_Graphs.Remove(ucpGraph);
                        loadDataPnlGraphs.Controls.Remove(ucpGraph);
                    }
                }
                LoadPlot(m_ArduinoUno.LoadBackState());
            }
        }
    }
}
