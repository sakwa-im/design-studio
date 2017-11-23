using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sakwa
{
    public partial class ExpressionForm : Form
    {
        public ExpressionForm()
        {
            InitializeComponent();
        }
        public ExpressionForm(IBaseNode node)
        {
            InitializeComponent();
            rtbExpression.Text = getExpression(node);

        }

        protected string getExpression(IBaseNode node)
        {
            if (node is IBranch)
                return (node as IBranch).Expression;

            if (node is IExpression)
                return (node as IExpression).Expression;

            return "";

        }

        public string Expression
        {
            get { return rtbExpression.Text; }
        }

    }
}
