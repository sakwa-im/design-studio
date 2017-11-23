using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.ComponentModel;

namespace sakwa
{
    class test : Control
    {
    }
    public class GridLayoutPanel : Panel
    {
        public enum LayoutMode { Automatic, Horizontal, Vertical }

        public GridLayoutPanel()
        {
            ControlAdded += new ControlEventHandler(OnControlAdded);
            ControlRemoved += new ControlEventHandler(OnControlRemoved);

        }

        [Category("GridLayoutPanel"), RefreshProperties(RefreshProperties.All), Description("The layout strategy of the control")]
        new public LayoutMode Layout
        {
            get { return _LayoutMode; }
            set
            {
                _LayoutMode = value;
                performLayout();

            }
        } //public LayoutMode Layout
        private LayoutMode _LayoutMode = LayoutMode.Automatic;

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            performLayout();
        }

        private void OnControlAdded(object sender, ControlEventArgs e)
        {
            foreach (Control c in Controls)
                c.Dock = DockStyle.None;

            performLayout();

        }

        private void  OnControlRemoved(object sender, ControlEventArgs e)
        {
            performLayout();
        }

        private void performLayout()
        {
            if (Controls.Count == 0)
                return;

            int cellWidth = 0;
            int cellHeight = 0;

            switch (_LayoutMode)
            {
                case LayoutMode.Automatic:
                    int colCount = 0;
                    int rowCount = 0;

                    do
                    {
                        colCount++;

                        int addExtraRow = Controls.Count == 1 || Controls.Count % 2 == 0 ? 0 : 1;
                        rowCount = addExtraRow + Controls.Count / colCount;

                    }
                    while (rowCount > colCount || rowCount * colCount < Controls.Count);

                    cellWidth = Width / colCount;
                    cellHeight = Height / rowCount;

                    int row = 0;
                    int col = 0;

                    for (int i = 0; i < Controls.Count; i++)
                    {
                        Control c = Controls[i];
                        c.Left = col * cellWidth + Padding.Left;
                        c.Top = row * cellHeight + Padding.Top;
                        c.Width = cellWidth - Padding.Horizontal;
                        c.Height = cellHeight - Padding.Vertical;

                        col++;
                        if(col % colCount == 0)
                        {
                            col = 0;
                            row++;

                        } //if(col % colCount == 0)
                    } //for (int i = 0; i < Controls.Count; i++)
                    break;

                case LayoutMode.Horizontal:
                    cellWidth = Width / Controls.Count;
                    cellHeight = Height;
                    int offset = Padding.Left;

                    for (int i = 0; i < Controls.Count; i++)
                    {
                        Control c = Controls[i];
                        c.Left = offset;
                        c.Top = Padding.Top;

                        c.Size = new System.Drawing.Size(cellWidth - Padding.Horizontal, cellHeight - Padding.Vertical);

                        offset += c.Width + Padding.Horizontal;
                        //c.Width = cellWidth - Padding.Horizontal;
                        //c.Height = cellHeight - Padding.Vertical;

                    }
                    
                    break;

                case LayoutMode.Vertical:
                    cellWidth = Width;
                    cellHeight = Height / Controls.Count;

                    for (int i = 0; i < Controls.Count; i++)
                    {
                        Control c = Controls[i];
                        c.Left = Padding.Left;
                        c.Top = i * cellHeight + Padding.Top;

                        c.Width = cellWidth - Padding.Horizontal;
                        c.Height = cellHeight - Padding.Vertical;

                    }
                    
                    break;

            }

        } //private void PerformLayout()

    } //public class GridLayoutPanel
}
