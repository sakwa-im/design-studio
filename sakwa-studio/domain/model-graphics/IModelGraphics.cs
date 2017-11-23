using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace sakwa
{
    public interface IModelGraphics
    {
        List<IGraphBase> GraphicCommands { get; }
        bool ParseDecisionModel(IDecisionTree tree);
        int HorizontalSpacing { get; set; }
        int VerticalSpacing { get; set; }
        void Draw(Graphics g);
        Size Size { get; }

        IBaseNode BaseNodeAt(Point location);

    }
}
