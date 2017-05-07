using System.Reflection;
using System.Windows.Forms;
using Defectively.Compatibility;

namespace DefectivelyServer.Forms
{
    public partial class AboutWindow : Form
    {
        public AboutWindow() {
            InitializeComponent();

            lblServerVersion.Text = $"Version {VersionHelper.GetFullStringFromAssembly(Assembly.GetExecutingAssembly())}\n({Assembly.GetExecutingAssembly().GetName().Version.ToString(4)})";
            lblCoreVersion.Text = $"Version {VersionHelper.GetFullStringFromCore()}";
        }
    }
}
