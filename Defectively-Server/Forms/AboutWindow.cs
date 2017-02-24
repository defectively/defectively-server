using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DefectivelyServer.Forms
{
    public partial class AboutWindow : Form
    {
        public AboutWindow() {
            InitializeComponent();

            lblServerVersion.Text = $"Version {new DefectivelyServer.Version().ToMediumString()}";
            lblCoreVersion.Text = $"Version {new Defectively.Compatibility.Version().ToMediumString()}";
        }
    }
}
