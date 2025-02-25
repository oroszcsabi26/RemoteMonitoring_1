namespace ArduinoWinformsApp
{
    partial class UCPeripheryGraph
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            SecLabel = new Label();
            minLabel = new Label();
            numericUpDownSec = new NumericUpDown();
            numericUpDownMin = new NumericUpDown();
            averageValue = new Label();
            maxValue = new Label();
            minValue = new Label();
            plotView0 = new OxyPlot.WindowsForms.PlotView();
            lblNamePeriphery = new Label();
            trackBar = new TrackBar();
            trackBarValue = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSec).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 32);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 18;
            label1.Text = "TimeScal:";
            // 
            // SecLabel
            // 
            SecLabel.AutoSize = true;
            SecLabel.Location = new Point(360, 32);
            SecLabel.Name = "SecLabel";
            SecLabel.Size = new Size(25, 15);
            SecLabel.TabIndex = 17;
            SecLabel.Text = "Sec";
            // 
            // minLabel
            // 
            minLabel.AutoSize = true;
            minLabel.Location = new Point(128, 30);
            minLabel.Name = "minLabel";
            minLabel.Size = new Size(28, 15);
            minLabel.TabIndex = 16;
            minLabel.Text = "Min";
            // 
            // numericUpDownSec
            // 
            numericUpDownSec.Location = new Point(394, 30);
            numericUpDownSec.Margin = new Padding(3, 2, 3, 2);
            numericUpDownSec.Name = "numericUpDownSec";
            numericUpDownSec.Size = new Size(131, 23);
            numericUpDownSec.TabIndex = 15;
            // 
            // numericUpDownMin
            // 
            numericUpDownMin.Location = new Point(163, 30);
            numericUpDownMin.Margin = new Padding(3, 2, 3, 2);
            numericUpDownMin.Name = "numericUpDownMin";
            numericUpDownMin.Size = new Size(131, 23);
            numericUpDownMin.TabIndex = 14;
            // 
            // averageValue
            // 
            averageValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            averageValue.AutoSize = true;
            averageValue.Location = new Point(373, 360);
            averageValue.Name = "averageValue";
            averageValue.Size = new Size(81, 15);
            averageValue.TabIndex = 13;
            averageValue.Text = "AverageValue:";
            // 
            // maxValue
            // 
            maxValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            maxValue.AutoSize = true;
            maxValue.Location = new Point(184, 360);
            maxValue.Name = "maxValue";
            maxValue.Size = new Size(61, 15);
            maxValue.TabIndex = 12;
            maxValue.Text = "MaxValue:";
            // 
            // minValue
            // 
            minValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            minValue.AutoSize = true;
            minValue.Location = new Point(14, 361);
            minValue.Name = "minValue";
            minValue.Size = new Size(59, 15);
            minValue.TabIndex = 11;
            minValue.Text = "MinValue:";
            // 
            // plotView0
            // 
            plotView0.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            plotView0.BackColor = SystemColors.ControlDark;
            plotView0.Location = new Point(14, 55);
            plotView0.Margin = new Padding(3, 2, 3, 2);
            plotView0.Name = "plotView0";
            plotView0.PanCursor = Cursors.Hand;
            plotView0.Size = new Size(794, 231);
            plotView0.TabIndex = 10;
            plotView0.Text = "plotView0";
            plotView0.ZoomHorizontalCursor = Cursors.SizeWE;
            plotView0.ZoomRectangleCursor = Cursors.SizeNWSE;
            plotView0.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // lblNamePeriphery
            // 
            lblNamePeriphery.AutoSize = true;
            lblNamePeriphery.Location = new Point(14, 8);
            lblNamePeriphery.Name = "lblNamePeriphery";
            lblNamePeriphery.Size = new Size(39, 15);
            lblNamePeriphery.TabIndex = 19;
            lblNamePeriphery.Text = "Name";
            // 
            // trackBar
            // 
            trackBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            trackBar.Location = new Point(15, 291);
            trackBar.Name = "trackBar";
            trackBar.Size = new Size(793, 45);
            trackBar.TabIndex = 20;
            trackBar.Scroll += trackBar_Scroll;
            // 
            // trackBarValue
            // 
            trackBarValue.AutoSize = true;
            trackBarValue.Location = new Point(15, 321);
            trackBarValue.Name = "trackBarValue";
            trackBarValue.Size = new Size(82, 15);
            trackBarValue.TabIndex = 21;
            trackBarValue.Text = "TrackBarValue:";
            // 
            // UCPeripheryGraph
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(trackBarValue);
            Controls.Add(trackBar);
            Controls.Add(lblNamePeriphery);
            Controls.Add(label1);
            Controls.Add(SecLabel);
            Controls.Add(minLabel);
            Controls.Add(numericUpDownSec);
            Controls.Add(numericUpDownMin);
            Controls.Add(averageValue);
            Controls.Add(maxValue);
            Controls.Add(minValue);
            Controls.Add(plotView0);
            Margin = new Padding(3, 2, 3, 2);
            Name = "UCPeripheryGraph";
            Size = new Size(823, 381);
            ((System.ComponentModel.ISupportInitialize)numericUpDownSec).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label SecLabel;
        private Label minLabel;
        private NumericUpDown numericUpDownSec;
        private NumericUpDown numericUpDownMin;
        private Label averageValue;
        private Label maxValue;
        private Label minValue;
        private OxyPlot.WindowsForms.PlotView plotView0;
        private Label lblNamePeriphery;
        private TrackBar trackBar;
        private Label trackBarValue;
    }
}
