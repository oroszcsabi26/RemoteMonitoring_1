namespace ArduinoWinformsApp
{
    partial class OnlineDataForm
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
            panelCheckBoxButtons = new Panel();
            flwpnlGraphs = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // panelCheckBoxButtons
            // 
            panelCheckBoxButtons.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panelCheckBoxButtons.Location = new Point(988, 12);
            panelCheckBoxButtons.Name = "panelCheckBoxButtons";
            panelCheckBoxButtons.Size = new Size(184, 399);
            panelCheckBoxButtons.TabIndex = 4;
            // 
            // flwpnlGraphs
            // 
            flwpnlGraphs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flwpnlGraphs.AutoScroll = true;
            flwpnlGraphs.Location = new Point(12, 12);
            flwpnlGraphs.Name = "flwpnlGraphs";
            flwpnlGraphs.Size = new Size(970, 399);
            flwpnlGraphs.TabIndex = 5;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 423);
            Controls.Add(flwpnlGraphs);
            Controls.Add(panelCheckBoxButtons);
            Name = "Form3";
            Text = "Form3";
            ResumeLayout(false);
        }

        #endregion
        private Panel panelCheckBoxButtons;
        private FlowLayoutPanel flwpnlGraphs;
    }
}