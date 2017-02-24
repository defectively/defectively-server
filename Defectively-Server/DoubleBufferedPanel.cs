using System.Windows.Forms;
using DefectivelyServer.Forms;

namespace DefectivelyServer
{
    public partial class DoubleBufferedPanel : Panel
    {
        public MainWindow.AccountState State { get; set; }

        public DoubleBufferedPanel() : base() {
            DoubleBuffered = true;
        }
    }
}
