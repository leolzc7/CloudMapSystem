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
    public partial class MyButton : Button
    {
        public new event EventHandler DoubleClick;
        DateTime clickTime;
        bool isClick = false;
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (isClick)
            {
                TimeSpan tspan = DateTime.Now - clickTime;
                if (tspan.Milliseconds < SystemInformation.DoubleClickTime)
                {
                    DoubleClick(this, e);
                    isClick = false;
                }
            }
            else
            {
                isClick = true;
                clickTime = DateTime.Now;
            }
        }
    }
}
