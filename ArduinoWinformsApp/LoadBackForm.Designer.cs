namespace ArduinoWinformsApp
{
    partial class LoadBackForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            loadDataPnlGraphs = new FlowLayoutPanel();
            panelCheckBoxButtons = new Panel();
            SuspendLayout();
            // 
            // loadDataPnlGraphs
            // 
            loadDataPnlGraphs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            loadDataPnlGraphs.AutoScroll = true;
            loadDataPnlGraphs.Location = new Point(11, 45);
            loadDataPnlGraphs.Name = "loadDataPnlGraphs";
            loadDataPnlGraphs.Size = new Size(944, 355);
            loadDataPnlGraphs.TabIndex = 7;
            // 
            // panelCheckBoxButtons
            // 
            panelCheckBoxButtons.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panelCheckBoxButtons.Location = new Point(961, 45);
            panelCheckBoxButtons.Name = "panelCheckBoxButtons";
            panelCheckBoxButtons.Size = new Size(184, 355);
            panelCheckBoxButtons.TabIndex = 6;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1157, 445);
            Controls.Add(loadDataPnlGraphs);
            Controls.Add(panelCheckBoxButtons);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel loadDataPnlGraphs;
        private Panel panelCheckBoxButtons;
    }
}