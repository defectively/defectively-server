﻿using System.Windows.Forms;
using Defectively;

namespace DefectivelyServer.Forms
{
    public partial class LoginWindow : Form
    {
        private Storage.Localization.Values Lcl;
        private Storage.Database.Values Database;

        public bool IsTimeoutLogin;

        public LoginWindow() {
            InitializeComponent();
            Load += LoginWindow_Load;
            Shown += LoginWindow_Shown;
            Closing += LoginWindow_Closing;
            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, System.EventArgs e) {
            if (Storage.Database.Helper.AccountExists(Database, tbxAccountId.Text)) {
                var Account = Storage.Database.Helper.GetAccount(Database, tbxAccountId.Text);
                if (Account.Password == Cryptography.ComputeHash(tbxPassword.Text)) {
                    if (Storage.Database.Helper.AccountHasLuvaValue(Database, Account, "defectively.canControlServer")) {
                        if (IsTimeoutLogin) {
                            ((MainWindow) Owner).ConsoleLocked = false;
                            ((MainWindow) Owner).tmrLoginTimeout.Start();
                            IsTimeoutLogin = false;
                            Close();
                        } else {
                            var MWindow = new MainWindow();
                            MWindow.Show();
                            Hide();
                        }
                    } else {
                        MessageBox.Show("Not Allowed", "Defectively", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                } else {
                    MessageBox.Show("Password Incorrect", "Defectively", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else {
                MessageBox.Show("Account Unknown", "Defectively", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (IsTimeoutLogin) {
                e.Cancel = true;
                if (MessageBox.Show("Do you really want to close?", "Defectively", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    ((MainWindow) Owner).StopServer();
                    Application.Exit();
                }
            }
        }

        private void LoginWindow_Shown(object sender, System.EventArgs e) {
            if (!Storage.Configuration.Helper.GetConfig().ConsoleRequiresAuthentification) {
                var Window = new MainWindow();
                Window.Show();
                Hide();
            }

            Database = Storage.Database.Helper.GetDatabase();

            if (Database?.Accounts == null) {
                var Window = new GettingStartedWindow();
                Window.Show();
                Hide();
            }
        }

        private void LoginWindow_Load(object sender, System.EventArgs e) {
            if (!Storage.Configuration.Helper.Exists())
                Storage.Configuration.Helper.CreateDefault();
            if (!Storage.Database.Helper.Exists())
                Storage.Database.Helper.CreateDefault();
        }

        public void ShowTimeoutDialog(Form owner) {
            IsTimeoutLogin = true;
            ShowDialog(owner);
        }
    }
}
