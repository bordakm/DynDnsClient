namespace DynDns_Client
{
    partial class Form1
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
            startButton = new Button();
            domainField = new TextBox();
            label1 = new Label();
            passwordField = new TextBox();
            label2 = new Label();
            startWithWindowsCheckbox = new CheckBox();
            savePasswordCheckbox = new CheckBox();
            intervalMinutesInput = new NumericUpDown();
            unhidePasswordCheckbox = new CheckBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            statusLabel = new Label();
            logTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)intervalMinutesInput).BeginInit();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.Location = new Point(12, 179);
            startButton.Name = "startButton";
            startButton.Size = new Size(307, 23);
            startButton.TabIndex = 0;
            startButton.Text = "Mentés és indítás";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += StartButtonClick;
            // 
            // domainField
            // 
            domainField.Location = new Point(71, 12);
            domainField.Name = "domainField";
            domainField.PlaceholderText = "Pl. teszt.dyndns.hu";
            domainField.Size = new Size(248, 23);
            domainField.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 2;
            label1.Text = "Domain";
            // 
            // passwordField
            // 
            passwordField.Location = new Point(91, 42);
            passwordField.Name = "passwordField";
            passwordField.PasswordChar = '*';
            passwordField.Size = new Size(228, 23);
            passwordField.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 45);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 4;
            label2.Text = "Jelszó/Token";
            // 
            // startWithWindowsCheckbox
            // 
            startWithWindowsCheckbox.AutoSize = true;
            startWithWindowsCheckbox.Enabled = false;
            startWithWindowsCheckbox.Location = new Point(16, 148);
            startWithWindowsCheckbox.Name = "startWithWindowsCheckbox";
            startWithWindowsCheckbox.Size = new Size(210, 19);
            startWithWindowsCheckbox.TabIndex = 5;
            startWithWindowsCheckbox.Text = "Automatikus indulás a Windowssal";
            startWithWindowsCheckbox.UseVisualStyleBackColor = true;
            startWithWindowsCheckbox.CheckedChanged += startWithWindowsCheckbox_CheckedChanged;
            // 
            // savePasswordCheckbox
            // 
            savePasswordCheckbox.AutoSize = true;
            savePasswordCheckbox.Location = new Point(482, 149);
            savePasswordCheckbox.Name = "savePasswordCheckbox";
            savePasswordCheckbox.Size = new Size(127, 19);
            savePasswordCheckbox.TabIndex = 6;
            savePasswordCheckbox.Text = "Jelszó megjegyzése";
            savePasswordCheckbox.UseVisualStyleBackColor = true;
            // 
            // intervalMinutesInput
            // 
            intervalMinutesInput.Location = new Point(129, 108);
            intervalMinutesInput.Maximum = new decimal(new int[] { 1440, 0, 0, 0 });
            intervalMinutesInput.Minimum = new decimal(new int[] { 15, 0, 0, 0 });
            intervalMinutesInput.Name = "intervalMinutesInput";
            intervalMinutesInput.Size = new Size(57, 23);
            intervalMinutesInput.TabIndex = 8;
            intervalMinutesInput.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // unhidePasswordCheckbox
            // 
            unhidePasswordCheckbox.AutoSize = true;
            unhidePasswordCheckbox.Location = new Point(93, 71);
            unhidePasswordCheckbox.Name = "unhidePasswordCheckbox";
            unhidePasswordCheckbox.Size = new Size(93, 19);
            unhidePasswordCheckbox.TabIndex = 9;
            unhidePasswordCheckbox.Text = "Megjelenítés";
            unhidePasswordCheckbox.UseVisualStyleBackColor = true;
            unhidePasswordCheckbox.CheckedChanged += UnhidePasswordChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 110);
            label3.Name = "label3";
            label3.Size = new Size(111, 15);
            label3.TabIndex = 10;
            label3.Text = "Frissítési gyakoriság";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(192, 110);
            label4.Name = "label4";
            label4.Size = new Size(30, 15);
            label4.TabIndex = 11;
            label4.Text = "perc";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 205);
            label5.Name = "label5";
            label5.Size = new Size(47, 15);
            label5.TabIndex = 12;
            label5.Text = "Státusz:";
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(65, 205);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 15);
            statusLabel.TabIndex = 13;
            // 
            // logTextBox
            // 
            logTextBox.Location = new Point(336, 12);
            logTextBox.Multiline = true;
            logTextBox.Name = "logTextBox";
            logTextBox.PlaceholderText = "Itt fognak a naplóbejegyzések megjelenni..";
            logTextBox.ReadOnly = true;
            logTextBox.ScrollBars = ScrollBars.Vertical;
            logTextBox.Size = new Size(311, 190);
            logTextBox.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(662, 243);
            Controls.Add(logTextBox);
            Controls.Add(statusLabel);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(unhidePasswordCheckbox);
            Controls.Add(intervalMinutesInput);
            Controls.Add(startWithWindowsCheckbox);
            Controls.Add(label2);
            Controls.Add(passwordField);
            Controls.Add(label1);
            Controls.Add(domainField);
            Controls.Add(startButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Form1";
            Text = "DynDns Client";
            ((System.ComponentModel.ISupportInitialize)intervalMinutesInput).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button startButton;
        private TextBox domainField;
        private Label label1;
        private TextBox passwordField;
        private Label label2;
        private CheckBox startWithWindowsCheckbox;
        private CheckBox savePasswordCheckbox;
        private NumericUpDown intervalMinutesInput;
        private CheckBox unhidePasswordCheckbox;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label statusLabel;
        private TextBox logTextBox;
    }
}
