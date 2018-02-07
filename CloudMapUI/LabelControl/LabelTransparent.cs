using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudMapUI
{
    public partial class LabelTransparent : Control
    {
        private Label label;
        public LabelTransparent(Label label)
        {
            this.label = label;
            Bounds = label.Bounds;
            Location = label.Location;
            ClientSize = label.ClientSize;
            
        }
        public LabelTransparent()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;
            if (label.TextAlign == ContentAlignment.TopLeft)
            {
                gfx.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor),ClientRectangle);
            }
            else if (label.TextAlign == ContentAlignment.TopCenter)
            {
                SizeF size = gfx.MeasureString(label.Text, label.Font);
                int left = label.Width / 2 - (int)size.Width / 2;
                var rect = new Rectangle(ClientRectangle.Left + left, ClientRectangle.Top, (int)size.Width, ClientRectangle.Height);
                gfx.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), rect);
            }
            else if(label.TextAlign == ContentAlignment.TopRight)
            {
                SizeF size = gfx.MeasureString(label.Text, label.Font);
                int left = label.Width - (int)size.Width + label.Left;
                var rect = new Rectangle(ClientRectangle.Left + left, ClientRectangle.Top, (int)size.Width, ClientRectangle.Height);
                gfx.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), rect);
            }
            base.OnPaint(e);
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }
    }
}
