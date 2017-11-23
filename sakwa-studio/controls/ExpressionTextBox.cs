using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace sakwa
{
    public class ExpressionTextBox : RichTextBox
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ExpressionTextBox));

        public ExpressionTextBox() : base()
        {
            AllowDrop = true;

            DragOver += handle_DragOver;
            DragDrop += handle_DragDrop;
            KeyDown += handle_KeyDown;
            KeyPress += handle_KeyPress;

        }

        private void handle_KeyPress(object sender, KeyPressEventArgs e)
        {
            ExpressionString.TextElement elem = _Expression.TextElementOnCharPosition(SelectionStart);
            if (elem is ExpressionString.BaseNodeElement)
                e.Handled = true;
        }

        private void handle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                ExpressionString.TextElement elem = _Expression.TextElementOnCharPosition(SelectionStart);
                if (elem is ExpressionString.BaseNodeElement)
                    e.Handled = true;
            }
        }

        private void handle_DragOver(object sender, DragEventArgs e)
        {
            Point insertPoint = PointToClient(new Point(e.X, e.Y));
            int index = GetCharIndexFromPosition(insertPoint);
            ExpressionString.TextElement elem = _Expression.TextElementOnCharPosition(index);

            if (getTreeNodeFromDragEventArgs(e) != null && 
                (elem == null || 
                 !(elem is ExpressionString.BaseNodeElement)))
                e.Effect = DragDropEffects.Copy;

        }
        private IBaseNode getTreeNodeFromDragEventArgs(DragEventArgs e)
        {
            string[] formats = e.Data.GetFormats();
            object obj = e.Data.GetData(formats[0]);
            return obj != null
                ? obj as IBaseNode
                : null;
        }
        private void handle_DragDrop(object sender, DragEventArgs e)
        {
            IBaseNode baseNode = getTreeNodeFromDragEventArgs(e);
            if (baseNode != null)
            {
                Point insertPoint = PointToClient(new Point(e.X, e.Y));
                Point lastChar = GetPositionFromCharIndex(Text.Length);
                log.Debug(lastChar.ToString());

                int index = GetCharIndexFromPosition(lastChar);
                if (index < 0)
                    index = Text.Length;

                Select(index, 0);

                Color tempColor = SelectionColor;
                Color tempBackColor = SelectionBackColor;
                SelectionColor = BaseNodeColor;
                SelectionBackColor = BaseNodeBackColor;
                SelectedText = baseNode.Name;
                SelectionColor = tempColor;
                SelectionBackColor = tempBackColor;

                Select(index + baseNode.Name.Length, 0);

            }
        }
        private void RenderExpressionString()
        {
            foreach(ExpressionString.TextElement elem in _Expression.TextElements)
            {
                if(elem is ExpressionString.BaseNodeElement)
                {
                    Select(elem.startPos, 0);

                    Color tempColor = SelectionColor;
                    Color tempBackColor = SelectionBackColor;
                    SelectionColor = BaseNodeColor;
                    SelectionBackColor = BaseNodeBackColor;
                    SelectedText = elem.ToString();
                    SelectionColor = tempColor;
                    SelectionBackColor = tempBackColor;

                    Select(elem.startPos + elem.length, 0);

                }
                else
                {
                    Text += elem.ToString();
                }

                Refresh();

            }
        }

        [CategoryAttribute("ExpressionTextBox")]
        [Description("Defines the color of the variable or domainobject")]
        public Color BaseNodeColor { get; set; }
        [CategoryAttribute("ExpressionTextBox")]
        [Description("Defines the background color of the variable or domainobject")]
        public Color BaseNodeBackColor { get; set; }

        [CategoryAttribute("ExpressionTextBox")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
        [CategoryAttribute("ExpressionTextBox")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [Localizable(true)]
        public virtual string RawText
        {
            get { return expression.RawText; }
            set
            {
                if (expression.RawText != value)
                {
                    _Expression.RawText = value;
                    //RenderExpressionString();
                    _Expression.RichTextBoxFormat(this);

                }
            }
        }


        public IDecisionTree IDecisionTree
        {
            get { return _DecisionTree; }
            set
            {
                _DecisionTree = value;
                if (_Expression == null)
                    _Expression = new ExpressionString(_DecisionTree);
                else
                    _Expression.IDecisionTree = _DecisionTree;

            }
        }

        protected ExpressionString expression
        {
            get
            {
                if (_Expression == null)
                    _Expression = new ExpressionString(_DecisionTree);

                return _Expression;

            }
        }
        protected ExpressionString _Expression = null;
        protected IDecisionTree _DecisionTree = null;

    }
}
