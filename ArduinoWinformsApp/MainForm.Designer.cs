namespace ArduinoWinformsApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnConnect = new Button();
            label1 = new Label();
            tbxAnswares = new TextBox();
            tbxCommand = new TextBox();
            tbxconnectinfo = new TextBox();
            label3 = new Label();
            btnSendData = new Button();
            arduinoRadioButton = new RadioButton();
            SimulatorButton = new RadioButton();
            btnDisConnect = new Button();
            label2 = new Label();
            streamTimer = new System.Windows.Forms.Timer(components);
            LoadBackData = new Button();
            LedBtnClick = new Button();
            showLiveData = new Button();
            SuspendLayout();
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(11, 8);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(165, 29);
            btnConnect.TabIndex = 0;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 205);
            label1.Name = "label1";
            label1.Size = new Size(154, 20);
            label1.TabIndex = 1;
            label1.Text = "SerialPort Actual Data";
            // 
            // tbxAnswares
            // 
            tbxAnswares.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbxAnswares.Location = new Point(11, 232);
            tbxAnswares.Multiline = true;
            tbxAnswares.Name = "tbxAnswares";
            tbxAnswares.ReadOnly = true;
            tbxAnswares.Size = new Size(592, 371);
            tbxAnswares.TabIndex = 2;
            // 
            // tbxCommand
            // 
            tbxCommand.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbxCommand.Location = new Point(11, 153);
            tbxCommand.Name = "tbxCommand";
            tbxCommand.Size = new Size(592, 27);
            tbxCommand.TabIndex = 3;
            // 
            // tbxconnectinfo
            // 
            tbxconnectinfo.Location = new Point(11, 73);
            tbxconnectinfo.Name = "tbxconnectinfo";
            tbxconnectinfo.Size = new Size(380, 27);
            tbxconnectinfo.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 51);
            label3.Name = "label3";
            label3.Size = new Size(166, 20);
            label3.TabIndex = 7;
            label3.Text = "Connection information";
            // 
            // btnSendData
            // 
            btnSendData.Location = new Point(11, 117);
            btnSendData.Name = "btnSendData";
            btnSendData.Size = new Size(253, 29);
            btnSendData.TabIndex = 8;
            btnSendData.Text = "Send Data to Device";
            btnSendData.UseVisualStyleBackColor = true;
            btnSendData.Click += btnSendCommand_Click;
            // 
            // arduinoRadioButton
            // 
            arduinoRadioButton.AutoSize = true;
            arduinoRadioButton.Location = new Point(447, 48);
            arduinoRadioButton.Margin = new Padding(3, 4, 3, 4);
            arduinoRadioButton.Name = "arduinoRadioButton";
            arduinoRadioButton.Size = new Size(83, 24);
            arduinoRadioButton.TabIndex = 9;
            arduinoRadioButton.TabStop = true;
            arduinoRadioButton.Text = "Arduino";
            arduinoRadioButton.UseVisualStyleBackColor = true;
            arduinoRadioButton.CheckedChanged += arduinoRadioButton_CheckedChanged;
            // 
            // SimulatorButton
            // 
            SimulatorButton.AutoSize = true;
            SimulatorButton.Location = new Point(447, 81);
            SimulatorButton.Margin = new Padding(3, 4, 3, 4);
            SimulatorButton.Name = "SimulatorButton";
            SimulatorButton.Size = new Size(94, 24);
            SimulatorButton.TabIndex = 10;
            SimulatorButton.TabStop = true;
            SimulatorButton.Text = "Simulator";
            SimulatorButton.UseVisualStyleBackColor = true;
            SimulatorButton.CheckedChanged += SimulatorButton_CheckedChanged;
            // 
            // btnDisConnect
            // 
            btnDisConnect.Location = new Point(211, 8);
            btnDisConnect.Margin = new Padding(3, 4, 3, 4);
            btnDisConnect.Name = "btnDisConnect";
            btnDisConnect.Size = new Size(181, 31);
            btnDisConnect.TabIndex = 11;
            btnDisConnect.Text = "DisConnect";
            btnDisConnect.UseVisualStyleBackColor = true;
            btnDisConnect.Click += btnDisConnect_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(443, 17);
            label2.Name = "label2";
            label2.Size = new Size(172, 20);
            label2.TabIndex = 12;
            label2.Text = "Choose a Communicator";
            // 
            // streamTimer
            // 
            streamTimer.Interval = 1000;
            streamTimer.Tick += StreamTimer_Tick;
            // 
            // LoadBackData
            // 
            LoadBackData.Location = new Point(421, 195);
            LoadBackData.Margin = new Padding(3, 4, 3, 4);
            LoadBackData.Name = "LoadBackData";
            LoadBackData.Size = new Size(183, 31);
            LoadBackData.TabIndex = 13;
            LoadBackData.Text = "LoadBackData";
            LoadBackData.UseVisualStyleBackColor = true;
            LoadBackData.Click += LoadBackData_Click;
            // 
            // LedBtnClick
            // 
            LedBtnClick.Location = new Point(211, 195);
            LedBtnClick.Margin = new Padding(3, 4, 3, 4);
            LedBtnClick.Name = "LedBtnClick";
            LedBtnClick.Size = new Size(181, 31);
            LedBtnClick.TabIndex = 14;
            LedBtnClick.Text = "LedOn";
            LedBtnClick.UseVisualStyleBackColor = true;
            LedBtnClick.Click += LedBtnClick_Click;
            // 
            // showLiveData
            // 
            showLiveData.Location = new Point(449, 118);
            showLiveData.Name = "showLiveData";
            showLiveData.Size = new Size(155, 29);
            showLiveData.TabIndex = 15;
            showLiveData.Text = "showLiveData";
            showLiveData.UseVisualStyleBackColor = true;
            showLiveData.Click += showLiveData_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(616, 619);
            Controls.Add(showLiveData);
            Controls.Add(LedBtnClick);
            Controls.Add(LoadBackData);
            Controls.Add(label2);
            Controls.Add(btnDisConnect);
            Controls.Add(SimulatorButton);
            Controls.Add(arduinoRadioButton);
            Controls.Add(btnSendData);
            Controls.Add(label3);
            Controls.Add(tbxconnectinfo);
            Controls.Add(tbxCommand);
            Controls.Add(tbxAnswares);
            Controls.Add(label1);
            Controls.Add(btnConnect);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnConnect;
        private Label label1;
        private TextBox tbxAnswares;
        private TextBox tbxCommand;
        private TextBox tbxconnectinfo;
        private Label label3;
        private Button btnSendData;
        private RadioButton arduinoRadioButton;
        private RadioButton SimulatorButton;
        private Button btnDisConnect;
        private Label label2;
        private System.Windows.Forms.Timer streamTimer;
        private Button LoadBackData;
        private Button LedBtnClick;
        private Button showLiveData;
    }
}
