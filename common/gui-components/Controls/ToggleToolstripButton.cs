using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sakwa
{
    public class ToggleToolstripButton : ToolStripButton
    {
        public ToggleToolstripButton() : base()
        {
            CheckOnClick = true;
        }

        [CategoryAttribute("ToggleToolstripButton")]
        [Description("Defines the button images for the checked state.")]
        public virtual Image CheckedImage
        {
            get { return _CheckedImage; }
            set
            {
                Image = _CheckedImage = value;
            }
        }
        [CategoryAttribute("ToggleToolstripButton")]
        [Description("Defines the button images for the un-checked state.")]
        public virtual Image UncheckedImage { get { return _UncheckedImage; } set { _UncheckedImage = value; } }

        protected override void OnCheckedChanged(EventArgs e)
        {
            Image = Checked ? _CheckedImage : _UncheckedImage;
        }

        protected Image _CheckedImage = null;
        protected Image _UncheckedImage = null;

    }
}
