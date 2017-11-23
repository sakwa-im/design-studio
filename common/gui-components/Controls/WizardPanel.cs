using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace sakwa
{
    public class WizardEventArgs
    {
        public WizardEventArgs(int stepNumber, string wizardText)
        {
            _WizardStep = stepNumber;
            _WizardText = WizardText;
        }
        public int WizardStep { get { return _WizardStep; } }
        public string WizardText { get { return _WizardText; } }
        private int _WizardStep = int.MinValue;
        private string _WizardText = "";

    }

    public delegate void WizardEventHandler(object sender, WizardEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    /// <history>
    /// 19-7-2011	m.roovers			Added IWizardBar interface implementation
    /// </history>
    public class WizardPanel : Panel, IWizardBar
    {
        public WizardPanel()
        {
            ControlAdded += new ControlEventHandler(OnControlAdded);
            ControlRemoved += new ControlEventHandler(OnControlRemoved);

        }

        [Category("WizardPanel"), RefreshProperties(RefreshProperties.All), Description("The event is raised when the wizard step is selected")]
        public event WizardEventHandler OnWizardStep;

        #region IWizardBar implementation
        int IWizardBar.CurrentStep { get { return CurrentStep; } set { CurrentStep = value; } }

        int IWizardBar.NextStep
        {
            get
            {
                IWizardStep ws = GetNextWizardStep();
                return ws != null ? ws.StepNumber : -1;

            }
        }
        int IWizardBar.PreviousStep
        {
            get
            {
                IWizardStep ws = GetPrevWizardStep();
                return ws != null ? ws.StepNumber : -1;

            }
        }
        bool IWizardBar.MoveToFirstStep()
        {
            for (int i = 1; i < _WizardSteps.Count; i++)
            {
                IWizardStep ws = _WizardSteps[i - 1];
                if (ws.Enabled)
                {
                    CurrentStep = ws.StepNumber;

                    SignalStep();

                    return true;

                } //if (ws.Enabled)
            } //for (int i = 1; i < _WizardSteps.Count; i++)

            return false;

        } //bool IWizardBar.MoveToFirstStep()
        bool IWizardBar.MoveToNextStep()
        {
            IWizardStep ws = GetNextWizardStep();
            if (ws != null)
            {
                CurrentStep = ws.StepNumber;

                SignalStep();

                return true;

            }

            return false;

        }
        bool IWizardBar.MoveToPreviousStep()
        {
            IWizardStep ws = GetPrevWizardStep();
            if(ws != null)
            {
                CurrentStep = ws.StepNumber;

                SignalStep();

                return true;

            }

            return false;

        } //bool IWizardBar.MoveToPreviousStep()
        bool IWizardBar.MoveToLastStep()
        {
            for (int i = _WizardSteps.Count; i > 0; i--)
            {
                IWizardStep ws = _WizardSteps[i - 1];
                if (ws.Enabled)
                {
                    CurrentStep = ws.StepNumber;

                    SignalStep();

                    return true;

                } //if (ws.Enabled)
            } //for (int i = 1; i < _WizardSteps.Count; i++)

            return false;

        } //bool IWizardBar.MoveToLastStep()
        bool IWizardBar.MoveToStep(int stepNumber)
        {
            IWizardStep ws = GetWizardStep(stepNumber);
            if (ws != null && ws.Enabled)
            {
                CurrentStep = ws.StepNumber;

                SignalStep();

                return true;

            } //if (ws != null && ws.Enabled)

            return false;

        } //bool IWizardBar.MoveToStep(int stepNumber)

        bool IWizardBar.StepEnabled(int stepNumber)
        {
            IWizardStep ws = GetWizardStep(stepNumber);
            return ws != null ? ws.Enabled : false;

        } //bool IWizardBar.StepEnabled( ...
        bool IWizardBar.StepEnabled(int stepNumber, bool enabled)
        {
            IWizardStep ws = GetWizardStep(stepNumber);
            if (ws != null)
                ws.Enabled = enabled;

            foreach (IWizardBar wb in _ConnectedWizardBars)
                wb.StepEnabled(stepNumber, enabled, false);

            return false;

        } //bool IWizardBar.StepEnabled( ...
        bool IWizardBar.StepEnabled(int stepNumber, bool enabled, bool signal)
        {
            IWizardStep ws = GetWizardStep(stepNumber);
            if (ws != null)
                ws.Enabled = enabled;

            if(signal)
                foreach (IWizardBar wb in _ConnectedWizardBars)
                    wb.StepEnabled(stepNumber, enabled, signal);

            return false;

        } //bool IWizardBar.StepEnabled( ...

        List<IWizardStep> IWizardBar.WizardSteps { get { return _WizardSteps; } }
        List<IWizardStep> _WizardSteps = new List<IWizardStep>();

        List<IWizardBar> IWizardBar.ConnectedWizardBars { get { return _ConnectedWizardBars; } }
        List<IWizardBar> _ConnectedWizardBars = new List<IWizardBar>();

        private IWizardStep GetWizardStep(int stepNumber)
        {
            foreach (IWizardStep ws in _WizardSteps)
                if (ws.StepNumber == stepNumber)
                    return ws;

            return null;

        } //private IWizardStep GetWizardStep(int stepNumber)
        private void SignalStep()
        {
            foreach (IWizardBar wb in _ConnectedWizardBars)
                wb.CurrentStep = _CurrentStep;

            IWizardStep ws = GetWizardStep(_CurrentStep);
            if (ws != null && OnWizardStep != null)
                OnWizardStep(this, new WizardEventArgs(ws.StepNumber, ws.Text));

        }
        private IWizardStep GetPrevWizardStep()
        {
            for (int i = _CurrentStep - 1; i > 0; i--)
            {
                IWizardStep ws = _WizardSteps[i - 1];
                if (ws.Enabled)
                    return ws;
            
            }

            return null;

        }
        private IWizardStep GetNextWizardStep()
        {
            for (int i = _CurrentStep + 1; i <= _WizardSteps.Count; i++)
            {
                IWizardStep ws = _WizardSteps[i - 1];
                if (ws.StepNumber >= _CurrentStep && ws.Enabled)
                    return ws;

            }

            return null;

        }
        #endregion

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        /// <history>
        /// 14-7-2011	m.roovers			Added
        /// </history>
        [Category("WizardPanel"), RefreshProperties(RefreshProperties.All), Description("The image shown between the wizard steps")]
        public Bitmap Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                performLayout();

            }
        }
        private Bitmap _Image = null;

        [Category("WizardPanel"), RefreshProperties(RefreshProperties.All), Description("The BackColor of the selected wizard step")]
        public Color SelectedBackColor { get { return _SelectedBackColor; } set { _SelectedBackColor = value; } }
        protected Color _SelectedBackColor = Color.DimGray;

        [Category("WizardPanel"), RefreshProperties(RefreshProperties.All), Description("The ForeColor of the selected wizard step")]
        public Color SelectedForeColor { get { return _SelectedForeColor; } set { _SelectedForeColor = value; } }
        protected Color _SelectedForeColor = Color.White;

        [Category("WizardPanel"), RefreshProperties(RefreshProperties.All), Description("The BackColor of the unselected wizard steps")]
        public Color UnselectedBackColor { get { return _UnselectedBackColor; } set { _UnselectedBackColor = value; } }
        protected Color _UnselectedBackColor = Color.White;

        [Category("WizardPanel"), RefreshProperties(RefreshProperties.All), Description("The ForeColor of the unselected wizard steps")]
        public Color UnselectedForeColor { get { return _UnselectedForeColor; } set { _UnselectedForeColor = value; } }
        protected Color _UnselectedForeColor = Color.Black;

        [Category("WizardPanel"), RefreshProperties(RefreshProperties.All), Description("The current wizard step")]
        public int CurrentStep { 
            get { return _CurrentStep; } 
            set
            { 
                _CurrentStep = value;
                Refresh();

            }
        }
        private int _CurrentStep = 1;

        [Category("WizardPanel"), RefreshProperties(RefreshProperties.All), Description("The relative size of the image in %")]
        public int RelativeImageSize
        {
            get { return _RelativeImageSize; }
            set
            {
                _RelativeImageSize = value;
                Refresh();

            }
        }
        private int _RelativeImageSize = 100;

        public bool WizardStepEnabled(int wizardStep)
        {
            foreach (Control c in Controls)
                if (c is IWizardStep && (c as IWizardStep).StepNumber == wizardStep)
                    return c.Enabled;

            return false;

        }

        public bool SetWizardStepEnable(int wizardStep, bool enabled)
        {
            foreach (Control c in Controls)
                if (c is IWizardStep && (c as IWizardStep).StepNumber == wizardStep)
                {
                    c.Enabled = enabled;
                    return c.Enabled;

                }

            return false;

        }

        private void OnStepSelected(object sender, EventArgs e)
        {
            if (sender is IWizardStep)
            {
                IWizardStep ws = sender as IWizardStep;
                if (CurrentStep != ws.StepNumber)
                {
                    CurrentStep = ws.StepNumber;
                    SignalStep();

                }
            }
        } //private void OnStepSelected(

        private void OnControlAdded(object sender, ControlEventArgs e)
        {
            if (Controls.Count > 0)
            {
                foreach (Control c in Controls)
                    c.Dock = DockStyle.None;

                if (e.Control is IWizardStep)
                {

                    IWizardStep ws = e.Control as IWizardStep;

                    ws.StepNumber = Controls.Count;
                    ws.StepSelected += new EventHandler(OnStepSelected);

                    _WizardSteps.Add(ws);

                }
            }
        }

        private void  OnControlRemoved(object sender, ControlEventArgs e)
        {
            if (e.Control is IWizardStep)
            {

                IWizardStep ws = e.Control as IWizardStep;
                _WizardSteps.Remove(ws);

            }
        }

        private void performLayout()
        {
            if (Controls.Count > 0)
            {
                int offset = Size.Width;
                for (int i = Controls.Count - 1; i >= 0; i--)
                {
                    Control c = Controls[i];
                    if (c is IWizardStep)
                    {

                        offset -= (c.Size.Width + Padding.Right);

                        c.Left = offset;

                        offset -= Padding.Left;

                        if (_Image != null)
                            offset -= (c.Height + Padding.Horizontal);

                    } //if (c is IWizardStep)
                } //for (int i = Controls.Count - 1; i >= 0; i--)

                foreach (Control c in Controls)
                    if (c is IWizardStep)
                    {
                        IWizardStep ws = c as IWizardStep;
                        if (ws.StepNumber == _CurrentStep)
                        {
                            ws.BackColor = _SelectedBackColor;
                            ws.ForeColor = _SelectedForeColor;

                        } //if (ws.StepNumber == _CurrentStep)
                        else
                        {
                            ws.BackColor = _UnselectedBackColor;
                            ws.ForeColor = _UnselectedForeColor;

                        } //else, if (ws.StepNumber == _CurrentStep)
                    } //if (c is IWizardStep)
            } //if (Controls.Count > 0)
        } //private void performLayout()

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Controls.Count > 0)
            {
                performLayout();

                if (_Image != null)
                    for (int i = 0; i < Controls.Count - 1; i++)
                    {
                        Control c = Controls[i];
                        int height = c.Height * _RelativeImageSize / 100;
                        int div = (c.Height - height) / 2;
                        e.Graphics.DrawImage(_Image, new Rectangle(c.Right + Padding.Left + div, c.Top + div, height, height));

                    } //for (int i = 0; i < Controls.Count - 1; i++)
            } //if (Controls.Count > 0)
        }

    } //public class WizardPanel
}
