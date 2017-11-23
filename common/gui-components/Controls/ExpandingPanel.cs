using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace sakwa
{
    public class ExpandingPanel : Panel
    {
        public ExpandingPanel()
        {
            ResizeRedraw = true;
            InitializeControl();
            UpdateControl();

        }

        [Category("ExpandingPanel"), RefreshProperties(RefreshProperties.All), Description("The height of the always visible part")]
        public int TopHeight
        {
            get { return _TopHeight; }
            set
            {
                _TopHeight = value >= 0 ? value : 0;
                UpdateControl();

            }
        }
        private int _TopHeight = 30;

        [Category("ExpandingPanel"), RefreshProperties(RefreshProperties.All), Description("The height of the expanded panel")]
        public int ExpandedHeight
        {
            get { return _ExpandedHeight; }
            set
            {
                _ExpandedHeight = value >= 0 ? value : 0;
                UpdateControl();

            }
        }
        private int _ExpandedHeight = 60;

        [Category("ExpandingPanel"), RefreshProperties(RefreshProperties.All), Description("The BackColor of the top panel")]
        public Color TopBackColor { get { return _TopPanel.BackColor; } set { _TopPanel.BackColor = value; } }
        
        [Category("ExpandingPanel"), RefreshProperties(RefreshProperties.All), Description("Colapse or expand")]
        public bool Colapsed
        {
            get
            {
                return Height == _TopPanel.Height;
            }
            set
            {
                if (!value)
                    Height = _ExpandedHeight;
                else
                    Height = _TopPanel.Height;
            }
        } //public bool Colapsed

        [Category("ExpandingPanel"), RefreshProperties(RefreshProperties.All), Description("Collapse or expand AccessibleName")]
        public string CollapseButtonAccessibleName
        {
            get { return _btnExpandColapse.AccessibleName; }
            set { _btnExpandColapse.AccessibleName = value; }
        }
        [Category("ExpandingPanel"), RefreshProperties(RefreshProperties.All), Description("Usr button AccessibleName")]
        public string UserButtonAccessibleName
        {
            get { return _btnUser.AccessibleName; }
            set { _btnUser.AccessibleName = value; }
        }
        //private int _Height = 0;

        //[Category("ExpandingPanel")]
        //[Browsable(false)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public new Control.ControlCollection Controls { get { return _MainPanel.Controls; } }

        /// <summary>
        /// This is te Text of the composit Lable control of this control.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/>.</returns>
        /// <history>
        /// 5-5-2011	m.roovers			Added
        /// </history>
        [Category("ExpandingPanel"), RefreshProperties(RefreshProperties.All), Description("The text in the top panel")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Browsable(true)]
        public override string Text
        {
            get { return _Text.Text; }
            set
            {
                _Text.Text = value;
                UpdateControl();

            }
        }

        [Category("ExpandingPanel"), RefreshProperties(RefreshProperties.All), Description("The top panel adds a button with this text run-time")]
        public string UserButtonText
        {
            get
            {
                return _UserButtonText;
            }
            set
            {
                if (_UserButtonText != value)
                {
                    _UserButtonText = value;
                    UpdateControl();

                }
            }
        }
        private string _UserButtonText = "";
        [Category("ExpandingPanel")]
        [Description("Occurs when the User Button is clicked")]
        public event EventHandler UserButtonClick;

        [Description("Size of the User Button")]
        [Category("ExpandingPanel")]
        public Size UserButtonSize
        {
            get { return _btnUser.Size; }
            set
            {
                _btnUser.Size = value;
                UpdateControl();
            }
        }

        [Category("ExpandingPanel"), RefreshProperties(RefreshProperties.All), Description("The Font of the top panel")]
        public Font TopTextFont { get { return _Text.Font; } set { _Text.Font = value; } }
 
        private Panel _TopPanel = new Panel();
        //private Panel _MainPanel = new Panel();
        private Button _btnExpandColapse = new Button();
        private Button _btnUser = new Button();

        private Label _Text = new Label();
        //private bool _MoveControls = true;

        private void InitializeControl()
        {
            //base.Controls.Add(_MainPanel);
            //_MainPanel.Dock = DockStyle.Fill;

            base.Controls.Add(_TopPanel);
            _TopPanel.Dock = DockStyle.Top;
            _TopPanel.Height = _TopHeight;
            _TopPanel.BackColor = Color.White;

            _btnExpandColapse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnExpandColapse.Width = 20;
            _btnExpandColapse.Height = 20;
            _btnExpandColapse.Top = (_TopHeight - _btnExpandColapse.Height) / 2;
            _btnExpandColapse.Left = _TopPanel.Width - (_btnExpandColapse.Width + _btnExpandColapse.Top);
            _TopPanel.Controls.Add(_btnExpandColapse);

            _btnExpandColapse.Click += new EventHandler(_btnExpandColapse_Click);

            _btnUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnUser.Top = _btnExpandColapse.Top;
            _btnUser.Width = _btnUser.Height = 20;
            _btnUser.Left = _btnExpandColapse.Left - (_btnExpandColapse.Top * 2 + _btnUser.Width);
            _btnUser.Visible = false;

            _btnUser.Click += new EventHandler(_btnUser_Click);
            _TopPanel.Controls.Add(_btnUser);

            _Text.Top = _Text.Left = _btnExpandColapse.Top;
            _Text.AutoSize = true;
            _TopPanel.Controls.Add(_Text);
            
        }

        void _btnUser_Click(object sender, EventArgs e)
        {
            if (UserButtonClick != null)
                UserButtonClick(sender, e);

        } //void _btnUser_Click(

        void _btnExpandColapse_Click(object sender, EventArgs e)
        {
            Colapsed = !Colapsed;
            UpdateControl();
        }

        private void UpdateControl()
        {
            _btnExpandColapse.Text = Colapsed ? "+" : "-";

            _btnUser.Text = _UserButtonText;
            _btnUser.Visible = _UserButtonText != "";
            _btnUser.Left = _btnExpandColapse.Left - (_btnExpandColapse.Top + _btnUser.Width);

        }

    }
}
