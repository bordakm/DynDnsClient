using DynDns_Client.TrayOnlyWinFormsDemo;
using Microsoft.Win32;
using System.Net;
using System.Reflection;

namespace DynDns_Client
{
    public partial class Form1 : Form
    {
        public Globals Globals { get; set; }
        public event Action? SaveAndStartTriggered;
        public event Action? StopTriggered;
        public Form1(Globals globals)
        {
            InitializeComponent();
            //this.Icon = new Icon("favicon.ico");
            var iconStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DynDns_Client.favicon.ico");
            Icon = iconStream != null ? new Icon(iconStream) : null;
            this.Globals = globals;
            passwordField.Text = globals.Password;
            domainField.Text = globals.Domain;
            savePasswordCheckbox.Checked = globals.SavePassword;
            intervalMinutesInput.Value = globals.IntervalMinutes;
            startWithWindowsCheckbox.Checked = globals.StartWithWindows;
            UpdateLogs();
            UpdateControlsFromTimerStatus();
        }
        public void UpdateControlsFromTimerStatus()
        {
            if (Globals.TimerOn)
            {
                startButton.Text = "Leállítás";
                statusLabel.Text = "Fut";
                domainField.Enabled = false;
                passwordField.Enabled = false;
                intervalMinutesInput.Enabled = false;
                startWithWindowsCheckbox.Enabled = false;
            }
            else
            {
                startButton.Text = "Mentés és indítás";
                statusLabel.Text = "Stop";
                domainField.Enabled = true;
                passwordField.Enabled = true;
                intervalMinutesInput.Enabled = true;
                startWithWindowsCheckbox.Enabled = true;
            }
        }

        private void UnhidePasswordChanged(object sender, EventArgs e)
        {
            passwordField.PasswordChar = unhidePasswordCheckbox.Checked ? (char)0 : '*';
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            if (Globals.TimerOn)
            {
                // turned off
                Globals.TimerOn = false;
                StopTriggered?.Invoke();
            }
            else
            {
                // turned on
                Globals.Domain = domainField.Text;
                Globals.Password = passwordField.Text;
                Globals.IntervalMinutes = int.Parse(intervalMinutesInput.Value.ToString());
                Globals.SavePassword = savePasswordCheckbox.Checked;
                Globals.StartWithWindows = startWithWindowsCheckbox.Checked;
                Globals.TimerOn = true;
                SaveAndStartTriggered?.Invoke();
            }
            UpdateControlsFromTimerStatus();
        }

        public void UpdateLogs()
        {
            logTextBox.Text = Globals.LogData;

            logTextBox.SelectionStart = logTextBox.Text.Length;
            logTextBox.ScrollToCaret();
        }

        private void startWithWindowsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey? rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rk != null)
            {
                if (startWithWindowsCheckbox.Checked)
                    rk.SetValue("DynDnsClient", Application.ExecutablePath);
                else
                    rk.DeleteValue("DynDnsClient", false);
            }
        }
    }
}
