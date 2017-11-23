using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sakwa
{
    public class SelectableFlowControl : FlowLayoutPanel
    {
        public SelectableFlowControl() : base()
        {
            this.SetStyle(ControlStyles.Selectable, true);
        }
    }
}
