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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;
using System.Reflection;
using OxyPlot.WindowsForms;

namespace ArduinoWinformsApp
{
    public partial class UCPeripheryGraph : UserControl
    {
        private List<PeripheryState> m_peripheryStatesList;
        private List<PeripheryState> m_peripheryStatesListForScroll;
        private List<double> m_S0ValuesList;
        private LineSeries? m_GraphDataSeries;
        private bool m_OnlineState;

        public string PeripheryName { get; set; }

        public UCPeripheryGraph()
        {
            InitializeComponent();
            m_peripheryStatesList = new List<PeripheryState>();
            m_peripheryStatesListForScroll = new List<PeripheryState>();
            m_S0ValuesList = new List<double>();
            InitializeOnlinePlot();
            numericUpDownMin.Value = 1;
            numericUpDownSec.Value = 0;
            m_OnlineState = true;
        }
        public void InitializeOnlinePlot()
        {
            var plotModel0 = new PlotModel { Title = "Time(min) - Value" };
            var dateTimeAxis0 = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "HH:mm:ss",
            };

            var series0 = new LineSeries();
            plotModel0.Series.Add(series0);
            plotModel0.Axes.Add(dateTimeAxis0);
            plotView0.Model = plotModel0;
            m_GraphDataSeries = plotView0.Model.Series[0] as LineSeries;
        }

        public void SetName(string p_Name)
        {
            PeripheryName = p_Name;
            lblNamePeriphery.Text = p_Name;
        }

        public void SetPlotData(List<PeripheryState> p_peripheryStateList)
        {
            //visszairni
            //m_peripheryStatesListForScroll = p_peripheryStateList;

            foreach (PeripheryState periphery in p_peripheryStateList)
            {
                m_peripheryStatesListForScroll.Add(periphery);
            }
            if (m_OnlineState)
            {
                //ClearGraph();
                m_S0ValuesList.Clear();
                DrawGraphs(m_peripheryStatesListForScroll);
            }
        }

        private void DrawGraphs(List<PeripheryState> p_peripheryStateList)
        {
            var plotModel0 = plotView0.Model;
            var series0 = plotModel0.Series[0] as LineSeries;

            SetPeripheryListData(p_peripheryStateList);
        }

        private void SetPeripheryListData(List<PeripheryState> p_peripheryStateList)
        {
            MicrocontrollerPin? selectedPin;
            List<DataPoint> graphPoints = new List<DataPoint>();

            foreach (PeripheryState periphery in p_peripheryStateList)
            {
                selectedPin = periphery.MicrocontrollerPins.Where(p => p.Name == PeripheryName).FirstOrDefault();
                if (selectedPin != null)
                {
                    graphPoints.Add(new DataPoint(DateTimeAxis.ToDouble(periphery.TimeStamp), selectedPin.GetValueDouble()));
                    //Add new data
                    if (m_OnlineState)
                    {
                        //Remove old data
                        DateTime minTime =
                        p_peripheryStateList.Last().TimeStamp.AddSeconds(-(double)(numericUpDownMin.Value * 60 + numericUpDownSec.Value));
                        m_GraphDataSeries.Points.RemoveAll(p => DateTimeAxis.ToDateTime(p.X) < minTime);
                        m_peripheryStatesList.RemoveAll(p => p.TimeStamp < minTime);
                        plotView0.Model.InvalidatePlot(true);
                        plotView0.Refresh();
                    }
                }
            }
            plotView0.SuspendLayout();
            m_GraphDataSeries.Points.AddRange(graphPoints);
            RefresNumericalValues(m_GraphDataSeries.Points.Select(dp => dp.Y).ToList());
            plotView0.Model.InvalidatePlot(true);
            plotView0.Refresh();
            plotView0.ResumeLayout(false);

            trackBar.Maximum = m_peripheryStatesListForScroll.Count() - 1;
            trackBar.Minimum = 0;
            trackBar.Width = plotView0.Width - 1;
            trackBar.TickFrequency = trackBar.Maximum;
        }

        private void RefresNumericalValues(List<double> p_pinValuesList)
        {
            double pinMaxValue = p_pinValuesList.Any() ? p_pinValuesList.Max() : 0.0;
            double pinMinValue = p_pinValuesList.Any() ? p_pinValuesList.Min() : 0.0;
            double pinAverageValue = p_pinValuesList.Any() ? p_pinValuesList.Average() : 0.0;

            minValue.Text = "Min:" + pinMinValue.ToString();
            maxValue.Text = "Max:" + pinMaxValue.ToString();
            averageValue.Text = "Avg:" + pinAverageValue.ToString("0.00");
        }

        public void ClearGraph()
        {
            var plotModel0 = plotView0.Model;
            var series0 = plotModel0.Series[0] as LineSeries;
            series0.Points.Clear();

            m_S0ValuesList.Clear();
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            ClearGraph();

            int trackBarStateID = trackBar.Value;
            m_OnlineState = (trackBarStateID == trackBar.Maximum);

            DateTime dateFrom = m_peripheryStatesListForScroll[trackBarStateID].TimeStamp;
            DateTime dateTo = dateFrom.AddMinutes((int)numericUpDownMin.Value).AddSeconds((int)numericUpDownSec.Value);
            trackBarValue.Text = $"TrackBarValue: {trackBarStateID.ToString()} ---> {dateFrom.ToString()} <===> {dateTo.ToString()}";
            List<PeripheryState> timeRangedState = new List<PeripheryState>();
            //Todo:atirni linq-ra
            foreach (PeripheryState iPeriphery in m_peripheryStatesListForScroll)
            {
                if (iPeriphery.TimeStamp > dateFrom && iPeriphery.TimeStamp < dateTo)
                {
                    timeRangedState.Add(iPeriphery);
                }
            }
            DrawGraphs(timeRangedState);
        }
    }
}
