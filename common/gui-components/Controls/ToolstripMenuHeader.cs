using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sakwa
{
    public class ToolstripMenuHeader : ToolStripMenuItem
    {
        public ToolstripMenuHeader() : base()
        {
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(BackColor), e.ClipRectangle);
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), e.ClipRectangle);

        }
    }
}
