using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using Defectively;
using Defectively.Compatibility;
using Defectively.Extension;
using Defectively.UI;
using DefectivelyServer.EventArguments;
using DefectivelyServer.Internal;
using DefectivelyServer.Management;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace DefectivelyServer.Forms
{
    public partial class MainWindow : Form
    {
        public enum AccountState
        {
            Offline,
            Online,
            Banned
        }

        private Server Server;

        public bool ConsoleLocked { get; set; }
        private bool ServerIsRunning;
        private int Timeout { get; set; }

        private int YCoordinate;

        private Storage.Localization.Values Lcl;

        public MainWindow() {
            InitializeComponent();
            Load += MainWindow_Load;
            Closing += MainWindow_Closing;
            btnStartServer.Click += BtnStartServer_Click;
            tbxInput.KeyDown += (sender, e) => {
                if (e.KeyCode == Keys.Enter) {
                    btnSend_Click(btnSend, new EventArgs());
                }
            };
            btnSend.Click += btnSend_Click;
            lblAssembly.Text = $"Defectively Server Version {VersionHelper.GetFullStringFromAssembly(Assembly.GetExecutingAssembly())} / Defectively Version {VersionHelper.GetFullStringFromCore()}";
            lblAssembly.Click += (sender, e) => {
                var Window = new AboutWindow();
                Window.ShowDialog();
            };

            tmrLoginTimeout.Tick += OnTimeout;

            cmsToolbar.RenderMode = ToolStripRenderMode.Professional;
            cmsToolbar.Renderer = new ToolStripProfessionalRenderer(new ToolStripColorTable());
        }

        private void OnTimeout(object sender, EventArgs e) {
            Timeout++;
            if (Timeout == Storage.Configuration.Helper.GetConfig().CATimeoutTime) {
                tmrLoginTimeout.Stop();
                var Window = new LoginWindow();
                Window.ShowTimeoutDialog(this);
            }
        }

        private void btnSend_Click(object sender, EventArgs e) {
            ListenerManager.InvokeEvent(Event.ConsoleInputReceived, tbxInput.Text);


            // DEMO
            if (tbxInput.Text == "/lockdown") {
                Server.Lockdown = !Server.Lockdown;
            }

            if (tbxInput.Text == "/n") {
                var Notification = new Notification("Notification Provider", "Clicking this notification will do absolutely nothing.", 2000, Empty);
                Server.ShowNotification(Notification);
            }

            if (tbxInput.Text == "/p") {
                var Punishment = new Punishment {
                    Id = Helpers.GenerateRandomId(6),
                    AccountId = "vainamo",
                    CreatorId = "server",
                    EndDate = new DateTime(2017, 5, 10, 15, 30, 0),
                    Reason = "DEMO",
                    Type = Enumerations.PunishmentType.Mute
                };

                PunishmentManager.Register(Punishment);
            }

            tbxInput.Clear();
        }

        private void Empty() { }

        private void MainWindow_Load(object sender, EventArgs e) {
            if (Storage.Configuration.Helper.GetConfig().ConsoleAuthentificationTimeout) {
                tmrLoginTimeout.Start();
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            StopServer();

            ntiNotification.ShowBalloonTip(2000, "Defectively Server", "Thanks for using Defectively.", ToolTipIcon.None);
            ntiNotification = null;

            Application.Exit();
        }

        private void BtnStartServer_Click(object sender, EventArgs e) {
            if (!ServerIsRunning) {
                if (StartServer()) {
                    btnStartServer.Text = "Stop Server";
                    SetTaskbarBackground(TaskbarProgressBarState.Normal);
                    RefreshAccountList();
                }
            } else {
                StopServer();
                btnStartServer.Text = "Start Server";
            }
        }

        private bool StartServer() {
            rtbConsole.Clear();

            Server = new Server { NotificationProvider = ntiNotification };

            Eskaemo.BeginSession();

            ExtensionPool.RegisterServer(Server);
            Server.Started += Server_Started;
            Server.ConsoleColorChanged += Server_ConsoleColorChanged;
            Server.ConsoleMessageReceived += Server_ConsoleMessageReceived;
            Server.FormCreated += Server_FormCreated;
            Server.AccountListChanged += Server_AccountListChanged;
            ServerIsRunning = Server.Start();

            if (ServerIsRunning) {
                this.Text += $" - {Server.Config.MetaServerName}";
                ntiNotification.ShowBalloonTip(2000, "Defectively Server", $"The server is up and running on {lblServerAddress.Text}.", ToolTipIcon.None);
            }

            return ServerIsRunning;
        }

        private void Server_AccountListChanged(object sender, EventArgs e) {
            this.Invoke(new Action(() => {
                pnlAccounts.Controls.Clear();
                YCoordinate = 0;
                var Database = Server.Database;
                foreach (var Rank in Database.Ranks) {
                    var Header = GetHeaderPanel(Rank.Name, ColorTranslator.FromHtml(Rank.Color));
                    Header.Location = new Point(0, YCoordinate);
                    YCoordinate += 20;
                    pnlAccounts.Controls.Add(Header);
                    var Accounts = Database.Accounts.FindAll(a => a.RankId == Rank.Id);
                    foreach (var Account in Accounts) {
                        var Item = GetItemPanel(Account.Name, ColorTranslator.FromHtml(Rank.Color), Account.Online);
                        Item.Location = new Point(0, YCoordinate);
                        YCoordinate += 51;
                        pnlAccounts.Controls.Add(Item);
                    }
                    YCoordinate -= 1;
                }
            }));
        }

        private void Server_FormCreated(object sender, FormCreatedEventArgs e) {
            e.Form.Show();
        }

        private void Server_Started(object sender, StartEventArgs e) {
            lblServerAddress.Text = e.IPAddress;
        }

        private void Server_ConsoleMessageReceived(object sender, ConsoleMessageEventArgs e) {
            this.Invoke(new Action(() => {
                rtbConsole.AppendText(e.Message);
                rtbConsole.ScrollToCaret();
            }));
        }

        private void Server_ConsoleColorChanged(object sender, ConsoleColorEventArgs e) {
            this.Invoke(new Action(() => {
                rtbConsole.SelectionStart = rtbConsole.TextLength;
                rtbConsole.SelectionLength = 0;
                rtbConsole.SelectionColor = e.Foreground;
                rtbConsole.SelectionBackColor = e.Background;
            }));
        }

        public void StopServer() {
            try {
                Server.Stop();
            } catch { }
            this.Text = "Defectively Server";
            ServerIsRunning = false;
            SetTaskbarBackground(TaskbarProgressBarState.Error);
        }

        public void SetTaskbarBackground(TaskbarProgressBarState color) {
            if (TaskbarManager.IsPlatformSupported) {
                TaskbarManager tbm = TaskbarManager.Instance;
                tbm.SetProgressValue(100, 100);
                tbm.SetProgressState(color);
            }
            switch (color) {
            case TaskbarProgressBarState.Normal:
                pnlStatus.BackColor = ColorTranslator.FromHtml("#07D159");
                lblStatus.ForeColor = lblAssembly.ForeColor = lblServerAddress.ForeColor = Color.Black;
                lblStatus.Text = "Running";
                break;
            case TaskbarProgressBarState.Paused:
                pnlStatus.BackColor = ColorTranslator.FromHtml("#FF6600");
                lblStatus.ForeColor = lblAssembly.ForeColor = lblServerAddress.ForeColor = Color.White;
                lblStatus.Text = "Warning";
                break;
            case TaskbarProgressBarState.Error:
                pnlStatus.BackColor = ColorTranslator.FromHtml("#FC3539");
                lblStatus.ForeColor = lblAssembly.ForeColor = lblServerAddress.ForeColor = Color.White;
                lblStatus.Text = "Stopped";
                break;
            }
            Focus();
        }

        public void RefreshAccountList() {
            Server_AccountListChanged(this, new EventArgs());
        }

        private Panel GetHeaderPanel(string header, Color background) {
            var Panel = new Panel();
            Panel.Width = pnlAccounts.Width - 17;
            Panel.Height = 20;
            Panel.BackColor = background;
            var Label = new Label();
            Label.Text = header;
            Label.ForeColor = Color.White;
            Label.AutoSize = false;
            Label.TextAlign = ContentAlignment.MiddleCenter;
            Label.Dock = DockStyle.Fill;
            Panel.Controls.Add(Label);
            return Panel;
        }

        private Panel GetItemPanel(string accountName, Color background, bool online) {
            var Panel = new Panel();
            Panel.Width = pnlAccounts.Width - 17;
            Panel.Height = 50;
            Panel.BackColor = Color.Gainsboro;
            var Label = new Label();
            Label.ForeColor = Color.White;
            Label.Padding = new Padding(3);
            Label.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            Label.Text = accountName;
            Label.AutoSize = true;
            Label.BackColor = background;
            Label.Location = new Point(15, 15);
            Panel.Controls.Add(Label);
            var Status = new DoubleBufferedPanel();
            Status.Size = new Size(20, 20);
            Status.Location = new Point(Panel.Width - 35, 15);



            // TODO
            //var Banned = PunishmentManager.CheckForRecords(accountName, Enumerations.PunishmentType.Bann, Enumerations.PunishmentType.BannTemporarily) != "-1";
            //if (Banned) {
            //    Status.State = AccountState.Banned;
            //} else {
            //    Status.State = online ? AccountState.Online : AccountState.Offline;
            //}


            Status.State = online ? AccountState.Online : AccountState.Offline;

            Status.Paint += StatusElementPaint;
            Panel.Controls.Add(Status);
            if (online && Server.Connections.Count > 0) {
                var Label2 = new Label();
                Label2.ForeColor = Color.DimGray;
                Label2.Padding = new Padding(3);
                Label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                try {
                    Label2.Text = $"in {Server.Connections.Find(c => c.Owner.Name == accountName).Channel.Name}";
                } catch { }
                Label2.AutoSize = true;
                Label2.BackColor = Color.White;
                Label2.Location = new Point(Label.Width + 21, 15);
                Panel.Controls.Add(Label2);

                if (Label2.Width > Status.Left - Label.Right - 21) {
                    Label2.AutoSize = false;
                    Label2.Height = 21;
                    Label2.Width = Status.Left - Label.Right - 21;
                    Label2.AutoEllipsis = true;
                }
            } else {
                Status.State = AccountState.Offline;
            }
            return Panel;
        }

        private void StatusElementPaint(object sender, PaintEventArgs e) {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            SolidBrush Brush;
            var Panel = (DoubleBufferedPanel) sender;
            if (Panel.State == AccountState.Online) {
                Brush = new SolidBrush(ColorTranslator.FromHtml("#1ED760"));
            } else if (Panel.State == AccountState.Offline) {
                Brush = new SolidBrush(Color.DimGray);
            } else {
                Brush = new SolidBrush(ColorTranslator.FromHtml("#FC3539"));
            }
            e.Graphics.FillEllipse(Brush, new RectangleF(0, 0, 19F, 19F));
            Brush.Dispose();
        }
    }
}