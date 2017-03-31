using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DefectivelyServer.Forms
{
    public partial class ExtensionWindow : Form
    {
        public ExtensionWindow() {
            InitializeComponent();

            dbpContainer.Paint += OnDbpContainerPaint;
        }

        private void OnDbpContainerPaint(object sender, PaintEventArgs e) {
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            var Latest = Color.CornflowerBlue;
            for (int i = 0; i < 4; i++) {
                e.Graphics.FillRectangle(new SolidBrush(Latest), 0, i * 100, dbpContainer.Width, 100);
                Latest = ControlPaint.Dark(Latest, 0.1F);
            }

            BringToFront();
        }
    }
}
