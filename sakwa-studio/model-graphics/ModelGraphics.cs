using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace sakwa
{
    public class GraphBaseImpl : IGraphBase
    {
        public GraphBaseImpl(int xLevel, int yLevel)
        {
            HorizontalOffset = xLevel;
            VerticalOffset = yLevel;
            Index++;
        }
        int IGraphBase.Index { get { return Index; } set { Index = value; } }
        string IGraphBase.Label { get { return Label; } set { Label = value; } }
        void IGraphBase.Draw(Graphics g) { Draw(g); }
        int IGraphBase.HorizontalSpacing { get { return HorizontalSpacing; } set { HorizontalSpacing = value; } }
        int IGraphBase.VerticalSpacing { get { return VerticalSpacing; } set { VerticalSpacing = value; } }
        int IGraphBase.VerticalOffset { get { return VerticalOffset; } set { VerticalOffset = value; } }
        int IGraphBase.HorizontalOffset { get { return HorizontalOffset; } set { HorizontalOffset = value; } }
        int IGraphBase.Weight { get { return Weight; } set { Weight = value; } }
        void IGraphBase.AdjustHorizontalOffset(int offset) { AdjustHorizontalOffset(offset); }

        Point IGraphBase.StartPoint
        {
            get { return ConnectedTo != null ? ConnectedTo.EndPoint : StartPoint(); }
        }
        Point IGraphBase.EndPoint { get { return EndPoint(); } }
        IGraphBase IGraphBase.ConnectedTo { get { return ConnectedTo; } set { ConnectedTo = value; } }
        bool IGraphBase.ContainsPoint(Point location) { return ContainsPoint(location); }

        IBaseNode IGraphBase.BaseNode { get { return BaseNode; } set { BaseNode = value; } }

        public Point Origin = new Point(0, 0);
        public Size Size = new Size(0, 0);

        protected virtual void Draw(Graphics g) {}
        protected virtual void AdjustHorizontalOffset(int offset)
        {
            if (offset <= HorizontalOffset)
                HorizontalOffset++;
        }
        protected virtual Point StartPoint()
        {
            return new Point(HorizontalOffset * HorizontalSpacing, VerticalOffset * VerticalSpacing);
        }
        protected virtual Point EndPoint()
        {
            return new Point(
                HorizontalOffset * HorizontalSpacing + HorizontalSpacing,
                VerticalOffset * VerticalSpacing + VerticalSpacing);
        }
        protected virtual bool ContainsPoint(Point location)
        {
            return false;
        }


        protected string Label = "";
        protected int VerticalOffset = 0;
        protected int HorizontalOffset = 1;

        //This value is in percentage
        protected int Weight = 100;

        //These values represent pixels
        protected int HorizontalSpacing = UI_Constants.HorizontalSpacing;
        protected int VerticalSpacing = UI_Constants.VerticalSpacing;

        protected int Index = 0;
        protected IGraphBase ConnectedTo = null;

        protected IBaseNode BaseNode = null;

        protected IGraphBase Interface {  get { return this as IGraphBase;  } }

    }
    public class ModelStart : GraphBaseImpl
    {
        public ModelStart(int xLevel, int yLevel) : base(xLevel, yLevel) { }
        protected override Point StartPoint()
        {
            return new Point(
                HorizontalOffset * HorizontalSpacing + HorizontalSpacing,
                VerticalOffset * VerticalSpacing);
        }
        protected override Point EndPoint()
        {
            return new Point(
                HorizontalOffset * HorizontalSpacing + HorizontalSpacing,
                VerticalOffset * VerticalSpacing + VerticalSpacing);
        }
        protected override void Draw(Graphics g)
        {
            Point p1 = Interface.StartPoint;
            p1.Y = VerticalSpacing / 4;
            Point p2 = Interface.EndPoint;

            Rectangle rect = new Rectangle(p1.X - 15, p1.Y, 30, 30);

            g.DrawLine(new Pen(UI_Constants.TelfortColor), p1, p2);
            g.FillEllipse(new SolidBrush(UI_Constants.TelfortColor), rect);

        }
    }

    public class VerticalLine : GraphBaseImpl
    {
        public VerticalLine(int xLevel, int yLevel) : base(xLevel, yLevel) { }
        protected override Point StartPoint()
        {
            return new Point(
                HorizontalOffset * HorizontalSpacing + HorizontalSpacing, 
                VerticalOffset * VerticalSpacing);
        }
        protected override Point EndPoint()
        {
            return new Point(
                HorizontalOffset * HorizontalSpacing + HorizontalSpacing,
                VerticalOffset * VerticalSpacing + VerticalSpacing);
        }
        protected override void Draw(Graphics g)
        {
            Point p1 = Interface.StartPoint;
            Point p2 = Interface.EndPoint;

            g.DrawLine(new Pen(UI_Constants.TelfortColor), p1, p2);

        }
    }
    public class HorizontalLine : GraphBaseImpl
    {
        public HorizontalLine(int xLevel, int yLevel) : base(xLevel, yLevel) { }
        protected override Point StartPoint()
        {
            return new Point(
                HorizontalOffset * HorizontalSpacing,
                VerticalOffset * VerticalSpacing);
        }
        protected override Point EndPoint()
        {
            return new Point(
                HorizontalOffset * HorizontalSpacing + HorizontalSpacing,
                VerticalOffset * VerticalSpacing);
        }
        protected override void Draw(Graphics g)
        {
            Point p1 = Interface.StartPoint;
            Point p2 = Interface.EndPoint;

            g.DrawLine(new Pen(UI_Constants.TelfortColor), p1, p2);

        }
    }

    public class Assignment : GraphBaseImpl
    {
        public Assignment(int xLevel, int yLevel, string label, Bitmap img) : base(xLevel, yLevel)
        {
            Label = label;
            image = img;
        }
        protected override Point StartPoint()
        {
            return new Point(
                HorizontalOffset * HorizontalSpacing + HorizontalSpacing,
                VerticalOffset * VerticalSpacing);
        }
        protected override Point EndPoint()
        {
            return new Point(
                HorizontalOffset * HorizontalSpacing + HorizontalSpacing,
                VerticalOffset * VerticalSpacing + VerticalSpacing);
        }
        protected override void Draw(Graphics g)
        {
            Point p1 = Interface.StartPoint;
            Point p2 = Interface.EndPoint;

            g.DrawLine(new Pen(UI_Constants.TelfortColor), p1, p2);

            p1 = StartPoint();
            int left = p1.X - HorizontalSpacing / 2;
            int middle = p1.Y + VerticalSpacing / 2;

            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            int deflate = -30;
            rect.Inflate(deflate, deflate);

            rect.X = p1.X - rect.Width / 2;
            rect.Y = middle - rect.Height / 2;

            int top = middle + rect.Height / 2 + 5;

            g.DrawImage(image, rect);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            rect = new Rectangle(left, top, HorizontalSpacing, VerticalSpacing / 3);
            g.DrawString(Label, UI_Constants.ModelFont, new SolidBrush(UI_Constants.TelfortColor), rect, format);

        }

        protected override bool ContainsPoint(Point location)
        {
            Rectangle rect = new Rectangle(StartPoint(), new Size(HorizontalSpacing, VerticalSpacing));
            rect.X -= HorizontalSpacing / 2;
            rect.Width += HorizontalSpacing / 2;

            return rect.Contains(location);

        }

        protected Bitmap image = null;
    }
    public class Branch : GraphBaseImpl
    {
        public Branch(int xLevel, int yLevel, string label) : base(xLevel, yLevel)
        {
            Label = label;
        }
        protected override Point StartPoint()
        {
            return new Point(
                HorizontalOffset * HorizontalSpacing + HorizontalSpacing,
                VerticalOffset * VerticalSpacing);
        }
        protected override Point EndPoint()
        {
            return new Point(
                HorizontalOffset * HorizontalSpacing + HorizontalSpacing,
                VerticalOffset * VerticalSpacing + VerticalSpacing);
        }
        protected override bool ContainsPoint(Point location)
        {
            Rectangle rect = new Rectangle(Interface.StartPoint, new Size(HorizontalSpacing, VerticalSpacing));
            rect.X -= HorizontalSpacing / 2;
            rect.Width += HorizontalSpacing / 2;

            return rect.Contains(location);

        }
        protected override void Draw(Graphics g)
        {
            Point p2 = Interface.StartPoint;
            Point p3 = Interface.EndPoint;
            //Point p3 = new Point(x + HorizontalSpacing, y + VerticalSpacing);

            //g.DrawLine(new Pen(Constants.TelfortColor), p1, p2);
            g.DrawLine(new Pen(UI_Constants.TelfortColor), p2, p3);


            Size stringSize = g.MeasureString(Label, UI_Constants.ModelFont).ToSize();
            //g.DrawString(Label, UI_Constants.ModelFont, new SolidBrush(UI_Constants.TelfortColor), p2.X - stringSize.Width, p2.Y + stringSize.Height);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Far;

            Rectangle rect = new Rectangle(p2.X - (HorizontalSpacing + 10), p2.Y, HorizontalSpacing, VerticalSpacing / 3);
            g.DrawString(Label, UI_Constants.ModelFont, new SolidBrush(UI_Constants.TelfortColor), rect, format);

        }
    }


    public class ModelGraphicsImpl : IModelGraphics
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ModelGraphicsImpl));

        public ModelGraphicsImpl(IApplication app)
        {
            AssignmentImage = app.GetResourceBitmap("assignment-big.png");
        }

        List<IGraphBase> IModelGraphics.GraphicCommands { get { return GraphicCommands; } }
        bool IModelGraphics.ParseDecisionModel(IDecisionTree tree)
        {
            if (tree != null)
            {
                //IGraphBase connectedTo = AddGraphicCommand(new VerticalLine(0, 0));
                IGraphBase connectedTo = AddGraphicCommand(new ModelStart(0, 0));
                ParseNodes(tree.RootNode.Nodes, 0, 1, connectedTo);

            }

            //foreach (IGraphBase gb in GraphicCommands)
            //    log.Debug(string.Format("{4} - {0}, ({1},{2}) {3}", gb.GetType().ToString(), gb.HorizontalOffset, gb.VerticalOffset, gb.Label, gb.Index));

            return GraphicCommands.Count > 0;

        }
        IBaseNode IModelGraphics.BaseNodeAt(Point location)
        {
            foreach (IGraphBase gb in GraphicCommands)
                if (gb.ContainsPoint(location))
                    return gb.BaseNode;

            return null;
        }
        int IModelGraphics.HorizontalSpacing
        {
            get { return HorizontalSpacing; }
            set
            {
                if (HorizontalSpacing != value)
                {
                    HorizontalSpacing = value;
                    foreach (IGraphBase gb in GraphicCommands)
                        gb.HorizontalSpacing = value;
                }
            }
        }
        int IModelGraphics.VerticalSpacing
        {
            get { return VerticalSpacing; }
            set
            {
                if (VerticalSpacing != value)
                {
                    VerticalSpacing = value;
                    foreach (IGraphBase gb in GraphicCommands)
                        gb.VerticalSpacing = value;
                }
            }
        }
        protected IGraphBase AddGraphicCommand(IGraphBase cmd, IGraphBase connectedTo = null, IBaseNode baseNode = null)
        {
            cmd.Index = Index++;
            cmd.ConnectedTo = connectedTo;

            GraphicCommands.Add(cmd);

            MaxWidth = Math.Max(MaxWidth, cmd.HorizontalOffset);
            MaxHeight = Math.Max(MaxHeight, cmd.VerticalOffset);

            cmd.BaseNode = baseNode;

            return cmd;

        }
        protected int Index = 1;

        protected void ParseNodes(List<IBaseNode> nodes, int xLevel, int yLevel, IGraphBase connectedTo = null)
        {
            List<IBaseNode> relevantNodes = GetRelevantNodes(nodes);
            List<IGraphBase> graphCmds = new List<IGraphBase>();
            IGraphBase ConnectedTo = connectedTo;

            int branchLevel = 0;
            foreach (IBaseNode node in relevantNodes)
            {
                switch (node.NodeType)
                {
                    case eNodeType.Expression:
                        ConnectedTo = AddGraphicCommand(new Assignment(xLevel, yLevel++, node.Name, AssignmentImage), connectedTo, node);
                        graphCmds.Add(ConnectedTo);
                        branchLevel = 0;
                        break;

                    case eNodeType.Branch:
                        int level = xLevel + 1 + branchLevel++;

                        AdjustHorizontal(level);

                        ConnectedTo = AddGraphicCommand(new HorizontalLine(level, yLevel), ConnectedTo);
                        graphCmds.Add(AddGraphicCommand(new Branch(level, yLevel, node.Name), ConnectedTo, node));

                        break;

                }
            }

            branchLevel = 0;
            for (int i = 0; i < relevantNodes.Count; i++)
                if (relevantNodes[i].NodeType == eNodeType.Branch && relevantNodes[i].Nodes.Count > 0)
                    ParseNodes(relevantNodes[i].Nodes, graphCmds[i].HorizontalOffset, graphCmds[i].VerticalOffset + 1, graphCmds[i]);

        }
        private List<IBaseNode> GetRelevantNodes(List<IBaseNode> nodes)
        {
            List<IBaseNode> result = new List<IBaseNode>();
            foreach (IBaseNode node in nodes)
                if (node.NodeType == eNodeType.Expression || node.NodeType == eNodeType.Branch)
                    result.Add(node);

            return result;

        }
        private void AdjustHorizontal(int level)
        {
            foreach (IGraphBase gb in GraphicCommands)
            {
                gb.AdjustHorizontalOffset(level);

                MaxWidth = Math.Max(MaxWidth, gb.HorizontalOffset);
                MaxHeight = Math.Max(MaxHeight, gb.VerticalOffset);

            }
        }

        void IModelGraphics.Draw(Graphics g)
        {
            foreach (IGraphBase gb in GraphicCommands)
                gb.Draw(g);
        }
        Size IModelGraphics.Size
        {
            get
            {
                return new Size(MaxWidth * HorizontalSpacing, MaxHeight * VerticalSpacing);
            }
        }

        protected List<IGraphBase> GraphicCommands = new List<IGraphBase>();
        protected int MaxWidth = 0;
        protected int MaxHeight = 0;

        protected int HorizontalSpacing = UI_Constants.HorizontalSpacing;
        protected int VerticalSpacing = UI_Constants.VerticalSpacing;

        protected Bitmap AssignmentImage = null;

    }
}
