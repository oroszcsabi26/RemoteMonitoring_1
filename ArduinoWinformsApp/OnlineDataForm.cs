using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
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
    public partial class OnlineDataForm : Form
    {
        CheckBox chbxPeriheries;
        List<UCPeripheryGraph> m_Graphs;

        public OnlineDataForm()
        {
            InitializeComponent();
            m_Graphs = new List<UCPeripheryGraph>();
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

        internal void UpdatePlot(PeripheryState peripheryState)
        { 
            foreach (UCPeripheryGraph iGraph in m_Graphs)
            {
                iGraph.SetPlotData(new List<PeripheryState>() { peripheryState });
            }
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
                        flwpnlGraphs.Controls.Add(ucpGraph);
                    }
                }
                else
                {
                    UCPeripheryGraph ucpGraph = m_Graphs.Where(c => c.PeripheryName == chbxPeriphery.Text).FirstOrDefault();
                    if (ucpGraph != null)
                    {
                        m_Graphs.Remove(ucpGraph);
                        flwpnlGraphs.Controls.Remove(ucpGraph);
                    }
                }
            }
        }
    }
}
