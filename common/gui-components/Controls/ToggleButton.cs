#define GLOSSY

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
//using System.Security.Permissions;
///using System.Windows.Automation;
//using System.Windows.Automation.Provider;

namespace sakwa
{
    public class ToggleButton : Button
//       , IRawElementProviderSimple
//       , IValueProvider
//       , IInvokeProvider
    {
/*       public const int WM_GETOBJECT = 0x3D;
       [PermissionSetAttribute(SecurityAction.Demand, Unrestricted = true)]
       protected override void WndProc(ref System.Windows.Forms.Message m)
       {
          if ((m.Msg == WM_GETOBJECT) && (m.LParam.ToInt64() == AutomationInteropProvider.RootObjectId))
          {
             m.Result = AutomationInteropProvider.ReturnRawElementProvider(Handle, m.WParam, m.LParam, (IRawElementProviderSimple)this);
             return;
          }
          base.WndProc(ref m);
       }
       #region IRawElementProviderSimple Members
       /// <summary>
       /// Retrieves an object that provides support for a control pattern on a UI Automation element.
       /// </summary>
       /// <param name="patternId">Identifier of the pattern.</param>
       /// <returns>
       /// Object that implements the pattern interface, or null if the pattern is not supported.
       /// </returns>
       object IRawElementProviderSimple.GetPatternProvider(int patternId)
       {
          if (patternId.Equals(System.Windows.Automation.ValuePatternIdentifiers.Pattern.Id ))
          {
             return this;
          }
          else if (patternId.Equals(System.Windows.Automation.InvokePatternIdentifiers.Pattern.Id))
          {
             return this;
          }
          return null;
       }

       object IRawElementProviderSimple.GetPropertyValue(int propertyId)
       {

          if (propertyId == AutomationElementIdentifiers.ControlTypeProperty.Id)
          {
             return ControlType.Button.Id;
          }
          else if (propertyId == AutomationElementIdentifiers.NameProperty.Id || propertyId == AutomationElementIdentifiers.AutomationIdProperty.Id)
          {
             return this.Name;
          }
          else if (propertyId == AutomationElementIdentifiers.IsKeyboardFocusableProperty.Id)
          {
             bool canFocus = false;
             this.Invoke(new MethodInvoker(() =>
             {
                canFocus = this.CanFocus;
             }));
             return canFocus;
          }
          else if (propertyId == AutomationElementIdentifiers.ClassNameProperty.Id)
          {
             return this.GetType().ToString();
          }
          else if (propertyId == AutomationElementIdentifiers.IsEnabledProperty.Id)
          {
             return this.Enabled;
          }
          return null; 
       }
       ProviderOptions IRawElementProviderSimple.ProviderOptions
       {
          get
          {
             return ProviderOptions.ServerSideProvider;
          }
       }
       IRawElementProviderSimple IRawElementProviderSimple.HostRawElementProvider
       {
          get
          {
             IntPtr hwnd = IntPtr.Zero;
             Invoke(new MethodInvoker(() =>
             {
                hwnd = this.Handle;
             }));
             return AutomationInteropProvider.HostProviderFromHandle(hwnd);
          }
       }

       #endregion

       #region IValueProvider Members

       // Summary:
       //     Gets a value that specifies whether the value of a control is read-only.
       //
       // Returns:
       //     true if the value is read-only; false if it can be modified.
       bool IValueProvider.IsReadOnly { get { return true; } }
       void IValueProvider.SetValue(string value) {}
       string IValueProvider.Value { get { return ((ToggleButton)this).ToggleState.ToString(); }}
       #endregion

       #region IInvokeProvider Members

       // Summary:
       //     Gets a value that specifies whether the value of a control is read-only.
       //
       // Returns:
       //     true if the value is read-only; false if it can be modified.
       void IInvokeProvider.Invoke() 
       {
          ToggleButton tb = (ToggleButton)this;
  ///        tb.Width
//Math.Abs(tb.Width - ((MouseEventArgs)e).Location.X)
          // Depending on toggle state click at the left or right half of the button
          int X = (tb.ToggleState == ToggleStates.Right) ? tb.Width / 4 : tb.Width * 3 / 4;
          tb.OnClick(new MouseEventArgs(MouseButtons.Left, 1, X, tb.Height/2, 0));
       }
       #endregion
*/
        public ToggleButton() { }
        public enum ToggleStates {Left, Right}

        [Category("ToggleButton"), RefreshProperties(RefreshProperties.All), Description("The primary text of the button")]
        public new string Text { get { return base.Text; } set { base.Text = value; } }

        [Category("ToggleButton"), RefreshProperties(RefreshProperties.All), Description("The font used to display text of the button")]
        public new Font Font { get { return base.Font; } set { base.Font = value; } }

        [Category("ToggleButton"), RefreshProperties(RefreshProperties.All), Description("The secondary text of the button")]
        public string Text2 { get { return _Text2; } set { _Text2 = value; } }
        private string _Text2 = "";

        [Category("ToggleButton"), RefreshProperties(RefreshProperties.All), Description("The Color of the toggled text of the button")]
        public Color SelectedBackColor { get { return _SelectedBackColor; } set { _SelectedBackColor = value; } }
        private Color _SelectedBackColor = SystemColors.Control;

        [Category("ToggleButton"), RefreshProperties(RefreshProperties.All), Description("The Fore Color of the toggled text of the button")]
        public Color SelectedForeColor { get { return _SelectedForeColor; } set { _SelectedForeColor = value; } }
        private Color _SelectedForeColor = Color.Black;

        [Category("ToggleButton"), RefreshProperties(RefreshProperties.All), Description("The toggle state of the button")]
        public ToggleStates ToggleState{get{return _ToggleState;} set{_ToggleState=value;}}
        private ToggleStates _ToggleState = ToggleStates.Left;

#if !GLOSSY
        [Category("ToggleButton"), RefreshProperties(RefreshProperties.All), Description("The Fore Color of the toggled text of the button")]
        public Color BorderColor { get { return _BorderColor; } set { _BorderColor = value; } }
        private Color _BorderColor = Color.Black;

        [Category("ToggleButton"), RefreshProperties(RefreshProperties.All), Description("The Radius of the rounded corners")]
        public int Radius { get { return _Radius; } set { _Radius = value; } }
        private int _Radius = 5;
#endif
        [Category("ToggleButton"), RefreshProperties(RefreshProperties.All),
        Description("The image show by the control when the left half is active")]
        public Bitmap ImageLeft
        {
            get { return _ImageLeft; }
            set
            {
                _ImageLeft = value;
                Refresh();
                //UpdateControl();

            }
        }
        private Bitmap _ImageLeft = null;
        [Category("ToggleButton"), RefreshProperties(RefreshProperties.All),
        Description("The image show by the control when the right half is active")]
        public Bitmap ImageRight
        {
            get { return _ImageRight; }
            set
            {
                _ImageRight = value;
                Refresh();
                //UpdateControl();

            }
        }
        private Bitmap _ImageRight = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pevent"></param>
        /// <history>
        /// 24-09-2013  m.roovers           COMMON-743 Sizing of the button image was wrong, reverted back to original implementation
        /// </history>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            Rectangle rL = new Rectangle(0, 0, this.Width / 2, this.Height);
            Rectangle rR = new Rectangle(this.Width / 2, 0, this.Width / 2, this.Height);
#if !GLOSSY
            pevent.Graphics.FillRectangle(new SolidBrush(BackColor), pevent.ClipRectangle);
#endif
            StringFormat fmt = new StringFormat(StringFormatFlags.NoClip);
            fmt.Alignment = StringAlignment.Center;
            fmt.LineAlignment = StringAlignment.Center;

            if (_ToggleState == ToggleStates.Left)
            {
                if (_ImageLeft != null)
                   pevent.Graphics.DrawImage(_ImageLeft, pevent.ClipRectangle);
                //   pevent.Graphics.DrawImage(_ImageLeft, pevent.ClipRectangle, pevent.ClipRectangle, GraphicsUnit.Pixel);
            }
            else
            {
                if (_ImageRight != null)
                    pevent.Graphics.DrawImage(_ImageRight, pevent.ClipRectangle);
                   //pevent.Graphics.DrawImage(_ImageRight, pevent.ClipRectangle, pevent.ClipRectangle, GraphicsUnit.Pixel);
            }

            switch (_ToggleState)
            {
                case ToggleStates.Left:
#if !GLOSSY
                    DrawRoundedBox(pevent.Graphics, rL, RoundedTypes.Left, _Radius, SelectedBackColor, _BorderColor);
#endif
                  pevent.Graphics.DrawString(Text, Font, new SolidBrush(SelectedForeColor), rL, fmt);

#if !GLOSSY
                    DrawRoundedBox(pevent.Graphics, rR, RoundedTypes.Right, _Radius, BackColor, _BorderColor);
#endif
                    pevent.Graphics.DrawString(Text2, Font, new SolidBrush(ForeColor), rR, fmt);
                    break;

                case ToggleStates.Right:
#if !GLOSSY
                    DrawRoundedBox(pevent.Graphics, rL, RoundedTypes.Left, _Radius, BackColor, _BorderColor);
#endif
                    pevent.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), rL, fmt);

#if !GLOSSY
                    DrawRoundedBox(pevent.Graphics, rR, RoundedTypes.Right, _Radius, SelectedBackColor, _BorderColor);
#endif
                    pevent.Graphics.DrawString(Text2, Font, new SolidBrush(SelectedForeColor), rR, fmt);
                    break;

            } //switch (_ToggleState)
           
        } //protected override void OnPaint(PaintEventArgs pevent)
#if !GLOSSY
        public enum RoundedTypes { None, Left, Top, Right, Bottom, Full }
        private void DrawRoundedBox(Graphics g, Rectangle bounds, RoundedTypes type, int radius, Color fillColor, Color borderColor)
        {
            GraphicsPath path = new GraphicsPath();
            Pen pen = new Pen(borderColor);

            pen.EndCap = pen.StartCap = LineCap.Round;
            switch (type)
            {
                case RoundedTypes.Left:
                    /*Left top*/    path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                    /*Right side*/  path.AddLine(bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
                    /*Left bottom*/ path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
                    break;

                case RoundedTypes.Top:
                    /*Left top*/    path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                    /*Right top*/   path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
                    /*Bottom side*/ path.AddLine(bounds.X + bounds.Width, bounds.Y + bounds.Height, bounds.X, bounds.Y + bounds.Height);
                    break;

                case RoundedTypes.Right:
                    /*Right top*/   path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
                    /*Right bottom*/path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius, radius, radius, 0, 90);
                    /*Left side*/   path.AddLine(bounds.X, bounds.Y + bounds.Height, bounds.X, bounds.Y);
                    break;

                case RoundedTypes.Bottom:
                    /*Top side*/    path.AddLine(bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y);
                    /*Right bottom*/path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius, radius, radius, 0, 90);
                    /*Left bottom*/ path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
                    break;

                case RoundedTypes.Full:
                    /*Left top*/    path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                    /*Right top*/   path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
                    /*Right bottom*/path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius, radius, radius, 0, 90);
                    /*Left bottom*/ path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
                    break;

                case RoundedTypes.None:
                    /*Top side*/    path.AddLine(bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y);
                    /*Right side*/  path.AddLine(bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
                    /*Bottom side*/ path.AddLine(bounds.X + bounds.Width, bounds.Y + bounds.Height, bounds.X, bounds.Y + bounds.Height);
                    /*Left side*/   path.AddLine(bounds.X, bounds.Y + bounds.Height, bounds.X, bounds.Y);
                    break;

            }
            path.CloseAllFigures();
            path.Flatten();

            g.SmoothingMode = SmoothingMode.HighQuality;

            g.FillPath(new SolidBrush(fillColor), path);
            g.DrawPath(pen, path);

        }
#endif
        protected override void OnClick(EventArgs e)
        {
//            Point p = PointToClient(Control.MousePosition);
           Point p = ((MouseEventArgs)e).Location;
            _ToggleState = (p.X < Width / 2) ? ToggleStates.Left : ToggleStates.Right;
            
            base.OnClick(e);

            Refresh();

        } //protected override void OnClick(EventArgs e)

    } //public class ToggleButton

}
