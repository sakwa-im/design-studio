using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace sakwa
{
    public delegate void OnCounterChanged(EventArgs e);


    /// <summary>
    /// Composite control containing an: Image, Text and Counter
    /// </summary>
    /// <history>
    /// 5-5-2011	m.roovers			Added
    /// </history>
    public class CounterPanel : RoundedPanel, IWizardStep, IMessageFilter
    {
        public static int WM_MOUSEMOVE      = 0x0200;
        public static int WM_LBUTTONDOWN    = 0x0201;
        public static int WM_LBUTTONUP      = 0x0202;
        public static int WM_LBUTTONDBLCLK  = 0x0203;

        public static int WM_RBUTTONDOWN    = 0x0204;
        public static int WM_RBUTTONUP      = 0x0205;
        public static int WM_RBUTTONDBLCLK  = 0x0206;

        public static int WM_MOUSEHOVER     = 0x02A1;

        /// <summary>
        /// Initializes a new instance of the <see cref="CounterPanel"/> class.
        /// </summary>
        /// <history>
        /// 5-5-2011	m.roovers			Added
        /// </history>
        public CounterPanel()
        {
            ResizeRedraw = true;
            _Corners = RoundedTypes.None;
            Height = 20;
            
            InitializeControl();
            UpdateControl();

            if (!DesignMode)
                base.MouseClick += new MouseEventHandler(OnMouseClick);

            #region Capture mouse click of composition
            //if (!DesignMode)
            //{
            //    Application.RemoveMessageFilter(this);
            //    Application.AddMessageFilter(this);
            //}
            #endregion

        }

        void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (StepSelected != null)
                StepSelected(this, new EventArgs());

        }

        #region IWizardStep implementation
        int IWizardStep.StepNumber { get { return _StepNumber; } set { _StepNumber = value; } }
        string IWizardStep.Text { get { return base.Text; } set { base.Text = value; } }

        [Category("CounterPanel"), RefreshProperties(RefreshProperties.All), Description("The event is raised when the step is selected")]
        public event EventHandler StepSelected;

        Color IWizardStep.BackColor { get { return FillColor; } set { FillColor = value; } }
        Color IWizardStep.ForeColor { get { return ForeColor; } set { ForeColor = value; } }

        bool IWizardStep.Enabled { get { return Enabled; } set { Enabled = value; } }

        protected int _StepNumber = 0;
        #endregion

        #region IMessageFilter implementation
        bool IMessageFilter.PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_LBUTTONDOWN)
            {
                Control c = Control.FromHandle(m.HWnd);
                if (DoHandle(c))
                {
                    if (StepSelected != null)
                        StepSelected(this, new EventArgs());

                    return true;

                }
            }

            return false;

        }

        private bool DoHandle(Control ctrl)
        {
            if (ctrl == null) return false;
            if (ctrl.Handle == this.Handle) return true;
            if (ctrl.Parent == null) return false;

            return DoHandle(ctrl.Parent);
        }
        #endregion

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        /// <history>
        /// 5-5-2011	m.roovers			Added
        /// </history>
        [Category("CounterPanel"), RefreshProperties(RefreshProperties.All), Description("The image show by the control")]
        public Bitmap Image {
            get { return _Image; }
            set
            {
                _Image = value;
                UpdateControl();

            }
        }
        private Bitmap _Image = null;

        /// <summary>
        /// Gets or sets the counter.
        /// </summary>
        /// <value>
        /// The counter.
        /// </value>
        /// <history>
        /// 5-5-2011	m.roovers			Added
        /// </history>
        [Category("CounterPanel"), RefreshProperties(RefreshProperties.All), Description("The value of the counter")]
        public int Counter { 
            get { return _Counter; } 
            set
            {
                _Counter = value >= 0 ? value : 0;
                UpdateControl();

            }
        }
        private int _Counter = 0;

        [Category("CounterPanel"), RefreshProperties(RefreshProperties.All), Description("Determines if counter is visible")]
        public bool CounterVisible { 
            get { return _CounterVisible; } 
            set
            {
                _CounterVisible = value;
                UpdateControl();

            }
        }
        private bool _CounterVisible = false;

        /// <summary>
        /// This is te Text of the composit Lable control of this control.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/>.</returns>
        /// <history>
        /// 5-5-2011	m.roovers			Added
        /// </history>
        [Category("CounterPanel"), RefreshProperties(RefreshProperties.All), Description("The Text of the Control")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Browsable(true)]
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                UpdateControl();

            }
        }
        [Category("CounterPanel"), RefreshProperties(RefreshProperties.All), Description("Counter AccessibleName")]
        public string CounterAccessibleName
        {
            get { return _lblCounter.AccessibleName; }
            set { _lblCounter.AccessibleName = value; }
        }

        private PictureBox _picImage = new PictureBox();
        private Label _lblText = new Label();
        private Label _lblCounter = new Label();

        /// <summary>
        /// Initializes the control.
        /// Populates the Controls collection with the three composite controls
        /// </summary>
        /// <history>
        /// 5-5-2011	m.roovers			Added
        /// 24-09-2013  m.roovers           COMMON-743 Changed the SizeMode so it 
        /// </history>
        private void InitializeControl()
        {
            _picImage.Name = "PictureBox";
            _picImage.Left = Margin.Left;
            _picImage.Top = Margin.Top;
            _picImage.Width = _picImage.Height = Height - Margin.Vertical;
            //_picImage.SizeMode = PictureBoxSizeMode.StretchImage;
            _picImage.SizeMode = PictureBoxSizeMode.Zoom;
            _picImage.BackColor = Color.Transparent;

            _picImage.MouseClick += new MouseEventHandler(OnMouseClick);

            Controls.Add(_picImage);

            _lblText.Name = "Label";
            _lblText.Top = Margin.Top;
            _lblText.Left = _picImage.Right + Margin.Horizontal;
            _lblText.AutoSize = true;
            _lblText.Height = _picImage.Height;
            _lblText.TextAlign = ContentAlignment.MiddleLeft;
            _lblText.BackColor = Color.Transparent;
            _lblText.MouseClick +=new MouseEventHandler(OnMouseClick);

            Controls.Add(_lblText);

            _lblCounter.Name = "Counter";
            _lblCounter.Top = Margin.Top;
            _lblCounter.Left = _lblText.Right + Margin.Horizontal;
            _lblCounter.AutoSize = true;
            _lblCounter.Height = _picImage.Height;
            _lblCounter.TextAlign = ContentAlignment.MiddleLeft;
            _lblCounter.BackColor = Color.Transparent;
            _lblCounter.MouseClick += new MouseEventHandler(OnMouseClick);

            Controls.Add(_lblCounter);

            Width = _lblCounter.Right + Margin.Right;

        }

        /// <summary>
        /// Sizes and positions the composite controls of the control.
        /// </summary>
        /// <history>
        /// 5-5-2011	m.roovers			Added
        /// </history>
        private void UpdateControl()
        {
            _picImage.Width = _picImage.Height = Height - Margin.Vertical;
            _picImage.Image = _Image;

            _picImage.Visible = _picImage.Image != null;
            _lblText.Left = _picImage.Image != null ? _picImage.Right + Margin.Horizontal : Margin.Left;
            _lblText.Height = _lblCounter.Height = _picImage.Height;

            _lblText.Text = base.Text;
            _lblCounter.Left = _lblText.Right + Margin.Horizontal;
            _lblCounter.Text = _CounterVisible || _Counter > 0 ? _Counter.ToString() : "";

            if (_lblCounter.Right + Margin.Right != Width)
                Width = _lblCounter.Right + Margin.Right;

            //_lblCounter.AccessibleName = _lblText.Text;

        }

        /// <summary>
        /// Called when the Font of the control is changed.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        /// <history>
        /// 5-5-2011	m.roovers			Added
        /// </history>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            UpdateControl();
        }
        /// <summary>
        /// Called when the Text of the control is changed.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        /// <history>
        /// 5-5-2011	m.roovers			Added
        /// </history>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            UpdateControl();
        }
        /// <summary>
        /// Called when the control is resized.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        /// <history>
        /// 5-5-2011	m.roovers			Added
        /// </history>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateControl();

        }

        public OnCounterChanged OnCounterChanged { get { return _OnCounterChanged; } set { _OnCounterChanged = value; } }
        private OnCounterChanged _OnCounterChanged = null;

    }
}
