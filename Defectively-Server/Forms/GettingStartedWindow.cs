using System;
using System.Drawing;
using System.Windows.Forms;

namespace DefectivelyServer.Forms
{
    public partial class GettingStartedWindow : Form
    {
        private Color SuccessColor = ColorTranslator.FromHtml("#2EBD59");

        public GettingStartedWindow() {
            InitializeComponent();

            btnSpecifyPassword.Click += OnSpecifyPasswordClick;
            btnCreateAccount.Click += OnCreateAccountClick;
            btnCreateGroup.Click += OnCreateGroupClick;
            this.Closing += (sender, e) => e.Cancel = true;
            btnCancel.Click += (sender, e) => Application.Exit();
        }

        private void OnSpecifyPasswordClick(object sender, EventArgs e) {
            btnContinue.Enabled = true;
            btnSpecifyPassword.Enabled = false;
            btnSpecifyPassword.Text = "Done";
            lblStep3.ForeColor = SuccessColor;
        }

        private void OnCreateAccountClick(object sender, EventArgs e) {
            using (var Window = new CreateAccountWindow()) {
                if (Window.ShowDialog() == DialogResult.OK) {
                    btnSpecifyPassword.Enabled = true;
                    btnCreateAccount.Enabled = false;
                    btnCreateAccount.Text = "Done";
                    lblStep2.ForeColor = SuccessColor;
                }
            }
        }

        private void OnCreateGroupClick(object sender, EventArgs e) {
            using (var Window = new CreateGroupWindow()) {
                if (Window.ShowDialog() == DialogResult.OK) {
                    btnCreateAccount.Enabled = true;
                    btnCreateGroup.Enabled = false;
                    btnCreateGroup.Text = "Done";
                    lblStep1.ForeColor = SuccessColor;
                }
            }
        }
    }
}
