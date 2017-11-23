using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace sakwa
{
    public interface IWizardBar
    {
        int CurrentStep { get; set; }

        int NextStep { get; }
        int PreviousStep { get; }

        bool MoveToFirstStep();
        bool MoveToNextStep();
        bool MoveToPreviousStep();
        bool MoveToLastStep();
        bool MoveToStep(int stepNumber);

        bool StepEnabled(int stepNumber);
        bool StepEnabled(int stepNumber, bool enabled);
        bool StepEnabled(int stepNumber, bool enabled, bool signal = true);

        List<IWizardStep> WizardSteps { get; }

        List<IWizardBar> ConnectedWizardBars { get; }

    } //public interface IWizardBar

    public interface IWizardStep
    {
        int StepNumber { get; set; }
        string Text { get; set; }

        Color BackColor { get; set; }
        Color ForeColor { get; set; }

        event EventHandler StepSelected;

        bool Enabled { get; set; }

    } //public interface IWizardStep

}
