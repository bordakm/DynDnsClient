using DynDns_Client.TrayOnlyWinFormsDemo;
using System.Net;
using System.Reflection;
using System.Resources;

namespace DynDns_Client
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool result;
            var mutex = new System.Threading.Mutex(true, "DynDnsClient", out result);

            if (!result)
            {
                MessageBox.Show("Another instance is already running. (Check system tray)");
                return;
            }
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            Application.Run(new MyCustomApplicationContext());

            GC.KeepAlive(mutex);                // mutex shouldn't be released - important line
        }
    }


    namespace TrayOnlyWinFormsDemo
    {
        public class Globals
        {
            public bool TimerOn { get; set; }
            public string Domain { get; set; } = "";
            public string Password { get; set; } = "";
            public bool SavePassword { get; set; }
            public int IntervalMinutes { get; set; }
            public bool StartWithWindows { get; set; }
            public string LogData { get; set; } = "";
        }
        public class MyCustomApplicationContext : ApplicationContext
        {
            private NotifyIcon trayIcon;
            private Globals globals;
            private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            public MyCustomApplicationContext()
            {
                globals = new Globals
                {
                    TimerOn = bool.Parse(RegistryUtilities.ReadRegistry(RegistryUtilities.TimerOn) ?? "false"),
                    Domain = RegistryUtilities.ReadRegistry(RegistryUtilities.Domain) ?? "",
                    IntervalMinutes = int.Parse(RegistryUtilities.ReadRegistry(RegistryUtilities.Interval) ?? "15"),
                    Password = RegistryUtilities.ReadRegistry(RegistryUtilities.Password) ?? "",
                    SavePassword = bool.Parse(RegistryUtilities.ReadRegistry(RegistryUtilities.SavePassword) ?? "false"),
                    StartWithWindows = bool.Parse(RegistryUtilities.ReadRegistry(RegistryUtilities.StartWithWindows) ?? "false")
                };
                if (globals.IntervalMinutes < 15) globals.IntervalMinutes = 15;
                var iconStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("DynDns_Client.favicon.ico");
                trayIcon = new NotifyIcon()
                {
                    //Icon = new Icon("favicon.ico"),
                    Icon = iconStream != null ? new Icon(iconStream) : null,
                    ContextMenuStrip = new ContextMenuStrip()
                    {
                        Items = { new ToolStripMenuItem("Megnyitás", null, Open), new ToolStripMenuItem("Kilépés", null, Exit) }
                    },

                    Visible = true
                };
                trayIcon.DoubleClick += TrayIcon_Click;

                timer.Tick += TimerTick;
                if (globals.TimerOn)
                {
                    timer.Interval = globals.IntervalMinutes * 60 * 1000;
                    timer.Start();
                    TimerTick(null, null);
                }

                var args = Environment.GetCommandLineArgs();
                if (args.Length >= 2 && (args.Contains("hidden") || args.Contains("-hidden") || args.Contains("--hidden") || args.Contains("/hidden")))
                {
                    // hidden mode
                }
                else
                {
                    Open(null, null);
                }
            }

            private void TrayIcon_Click(object? sender, EventArgs e)
            {
                Open(sender, e);
            }

            private void TimerTick(object? sender, EventArgs? e)
            {
                Console.WriteLine(globals.TimerOn);
                if (globals.TimerOn)
                {
                    globals.LogData += GetCurrentShortDateTime() + " IP cím lekérés..." + Environment.NewLine;
                    UpdateLogs();
                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://localhost:8080");
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://myip.dyndns.hu/");
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    string content = new StreamReader(response.GetResponseStream()).ReadToEnd();

                    globals.LogData += GetCurrentShortDateTime() + $" Lekért IP: {content}" + Environment.NewLine;
                    UpdateLogs();

                    globals.LogData += GetCurrentShortDateTime() + $" IP beállítása: {content}" + Environment.NewLine;
                    UpdateLogs();

                    HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create($"https://dyndns.hu/update?hostname={globals.Domain}&username={globals.Domain}&myip={content}&password={globals.Password}");
                    try
                    {
                        HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
                        string content2 = new StreamReader(response2.GetResponseStream()).ReadToEnd();
                        if (content2 == "good" || content2 == "nochg\n")
                        {
                            globals.LogData += GetCurrentShortDateTime() + $" IP beállítása sikeres" + Environment.NewLine;
                            UpdateLogs();
                        }
                        else
                        {
                            globals.LogData += GetCurrentShortDateTime() + $" IP beállítása sikertelen, ismeretlen hiba" + Environment.NewLine;
                            UpdateLogs();
                        }

                    }
                    catch (WebException ex)
                    {

                        if (ex.Response is HttpWebResponse && (ex.Response as HttpWebResponse).StatusCode == HttpStatusCode.Unauthorized)
                        {
                            globals.LogData += GetCurrentShortDateTime() + $" IP beállítása sikertelen: hibás domain vagy jelszó" + Environment.NewLine;
                            UpdateLogs();
                        }
                        else
                        {
                            globals.LogData += GetCurrentShortDateTime() + $" IP beállítása sikertelen: ismeretlen hiba" + Environment.NewLine;
                            UpdateLogs();
                        }


                    }


                }
            }
            private void UpdateLogs()
            {
                FormCollection frm = Application.OpenForms;
                if (frm.Count > 0)
                {
                    if (frm[0] is Form1)
                    {
                        ((Form1)frm[0]!).UpdateLogs();
                    }
                }
            }

            void Exit(object? sender, EventArgs e)
            {
                trayIcon.Visible = false;
                Application.Exit();
            }

            void Open(object? sender, EventArgs? e)
            {
                //trayIcon.Visible = false;
                var f1 = new Form1(globals);
                f1.SaveAndStartTriggered += F1_SaveAndStartTriggered;
                f1.SaveTriggered += F1_SaveTriggered; ;
                f1.StopTriggered += F1_StopTriggered;
                FormCollection frm = Application.OpenForms;

                if (frm.Count == 0) f1.ShowDialog();
                else
                {
                    if (frm[0]?.WindowState == FormWindowState.Minimized)
                    {
                        frm[0]!.WindowState = FormWindowState.Normal;
                    }
                    frm[0]?.Activate();
                }
                //Application.Run();
            }

            private void F1_SaveTriggered()
            {
                SaveToRegistry();
            }

            private void F1_StopTriggered()
            {
                SaveToRegistry();
                timer.Stop();
            }

            private void F1_SaveAndStartTriggered()
            {
                SaveToRegistry();

                timer.Interval = globals.IntervalMinutes * 60 * 1000;
                timer.Start();
                TimerTick(null, null);
            }

            void SaveToRegistry()
            {
                RegistryUtilities.WriteRegistry(RegistryUtilities.Domain, globals.Domain);
                RegistryUtilities.WriteRegistry(RegistryUtilities.Interval, globals.IntervalMinutes.ToString());
                RegistryUtilities.WriteRegistry(RegistryUtilities.Password, globals.Password);
                RegistryUtilities.WriteRegistry(RegistryUtilities.SavePassword, globals.SavePassword.ToString());
                RegistryUtilities.WriteRegistry(RegistryUtilities.TimerOn, globals.TimerOn.ToString());
                RegistryUtilities.WriteRegistry(RegistryUtilities.StartWithWindows, globals.StartWithWindows.ToString());
            }

            private string GetCurrentShortDateTime()
            {
                var now = DateTime.Now;
                return $"{now.Year}.{now.Month.ToString().PadLeft(2, '0')}.{now.Day.ToString().PadLeft(2, '0')} {now.Hour.ToString().PadLeft(2, '0')}:{now.Minute.ToString().PadLeft(2, '0')}:{now.Second.ToString().PadLeft(2, '0')}";

            }
        }
    }
}