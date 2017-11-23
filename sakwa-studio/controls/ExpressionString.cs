using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace sakwa
{
    public class ExpressionString
    {
        public ExpressionString() { }
        public ExpressionString(IDecisionTree tree)
        {
            _DecisionTree = tree;
        }
        public ExpressionString(IDecisionTree tree, string text)
        {
            _DecisionTree = tree;
            _Text = text;

            ParseRawText();

        }

        public string Text
        {
            get
            {
                string result = "";

                foreach (TextElement elem in _Elements)
                    result += elem.ToString();

                return result;
            }
            set
            {
            }
        }
        public string RawText
        {
            get
            {
                string result = "";

                foreach (TextElement elem in _Elements)
                    result += elem.ToRawString();

                return result;

            }
            set
            {
                if(_Text != value)
                {
                    _Text = value;
                    ParseRawText();

                }
            }
        }
        public TextElement TextElementOnCharPosition(int index)
        {
            foreach (TextElement elem in _Elements)
                if (elem.Intersect(index))
                    return elem;

            return null;

        }
        public IDecisionTree IDecisionTree
        {
            get { return _DecisionTree; }
            set { _DecisionTree = value; }
        }
        public List<TextElement> TextElements {  get { return _Elements; } }

        public void RichTextBoxFormat(ExpressionTextBox rtb)
        {
            foreach (TextElement elem in _Elements)
                elem.RichTextBoxFormat(rtb);
        }
        protected void ParseRawText()
        {
            string refPattern = @"[({]?[0-9A-Fa-f]{8}-([0-9A-Fa-f]{4}-){3}[0-9A-Fa-f]{12}[)}]?";
            _Elements.Clear();

            Regex regex = new Regex(refPattern);
            MatchCollection matches = regex.Matches(_Text);
            if (matches.Count > 0)
            {
                int index = 0;
                int rawIndex = 0;
                int size = 0;
                foreach (Match m in matches)
                {
                    if (m.Index > rawIndex)
                    {
                        size = m.Index - rawIndex;
                        _Elements.Add(new PlainTextElement(
                            _Text.Substring(rawIndex, size), index));

                        index += size;
                        rawIndex += size;

                    }

                    IBaseNode node = _DecisionTree.GetNodeByReference(m.Value.Substring(1, m.Value.Length - 2));
                    _Elements.Add(new BaseNodeElement(node, index));

                    rawIndex += m.Value.Length;
                    index += node.Name.Length;

                }

                size = _Text.Length - rawIndex;
                if (size > 0)
                {
                    _Elements.Add(new PlainTextElement(
                        _Text.Substring(rawIndex, size), index));
                }
            }
            else
                _Elements.Add(new PlainTextElement(_Text, 0));

        }

        protected List<TextElement> _Elements = new List<TextElement>();
        protected IDecisionTree _DecisionTree = null;
        protected string _Text = "";

        public class TextElement
        {
            public TextElement() { }

            public int index = 0;
            public int startPos = -1;
            public int length = 0;

            public override string ToString()
            {
                return base.ToString();
            }
            public virtual string ToRawString()
            {
                return "";
            }
            public virtual bool Intersect(int pos)
            {
                return false;
            }
            public virtual void RichTextBoxFormat(ExpressionTextBox rtb)
            {
            }

        }
        public class PlainTextElement : TextElement
        {
            public PlainTextElement() { }
            public PlainTextElement(string text, int startPos)
            {
                this.startPos = startPos;
                this.text = text;
                this.length = text.Length;

            }

            public string text = "";

            public override string ToString()
            {
                return text;
            }
            public override string ToRawString()
            {
                return text;
            }
            public override bool Intersect(int pos)
            {
                return pos >= startPos && pos <= startPos + text.Length;
            }
            public override void RichTextBoxFormat(ExpressionTextBox rtb)
            {
                rtb.Text += ToString();
            }

        }
        public class BaseNodeElement : TextElement
        {
            public BaseNodeElement() { }
            public BaseNodeElement(IBaseNode baseNode, int startPos)
            {
                this.startPos = startPos;
                this.baseNode = baseNode;
                this.length = baseNode.Name.Length;

            }

            public IBaseNode baseNode = null;

            public override string ToString()
            {
                return baseNode.Name;
            }
            public override string ToRawString()
            {
                return "{" + baseNode.Reference + "}";
            }
            public override bool Intersect(int pos)
            {
                return pos >= startPos && pos <= startPos + baseNode.Name.Length;
            }
            public override void RichTextBoxFormat(ExpressionTextBox rtb)
            {
                Color tempColor = rtb.SelectionColor;
                Color tempBackColor = rtb.SelectionBackColor;

                //rtb.Select(startPos, 0);
                rtb.Select(startPos, baseNode.Name.Length);

                rtb.SelectionColor = rtb.BaseNodeColor;
                rtb.SelectionBackColor = rtb.BaseNodeBackColor;

                rtb.SelectedText = ToString();

                //rtb.Select(startPos, baseNode.Name.Length);

                rtb.SelectionColor = tempColor;
                rtb.SelectionBackColor = tempBackColor;


            }
        }

    }
}
