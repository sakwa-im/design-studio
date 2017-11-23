using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;

namespace sakwa
{
    public partial class ucConfirmDropModel : UserControl
    {
        public ucConfirmDropModel(IDecisionTree tree, bool isModel = true)
        {
            InitializeComponent();

            if(!isModel)
            {
                label2.Text = label2.Text.Replace(UI_Constants.ModelName, UI_Constants.TemplateName);
                counterPanel1.Text = counterPanel1.Text.Replace(UI_Constants.ModelName, UI_Constants.TemplateName);
            }
        }
    }
}
