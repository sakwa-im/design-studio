using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace sakwa
{
    public interface IGraphBase
    {
        int Index { get; set; }
        string Label { get; set; }
        void Draw(Graphics g);
        int HorizontalSpacing { get; set; }
        int VerticalSpacing { get; set; }
        int VerticalOffset { get; set; }
        int HorizontalOffset { get; set; }
        int Weight { get; set; }
        void AdjustHorizontalOffset(int offset);

        Point StartPoint { get; }
        Point EndPoint { get; }
        IGraphBase ConnectedTo { get; set; }

        IBaseNode BaseNode { get; set; }

        bool ContainsPoint(Point location);

    }

}
