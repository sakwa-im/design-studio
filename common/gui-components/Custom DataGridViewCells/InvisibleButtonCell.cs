using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace sakwa
{
    public class InvisibleButtonColumn : DataGridViewColumn
    {
        public InvisibleButtonColumn()
            : base(new InvisibleButtonCell())
        {
        }

    } //public class InvisibleButtonColumn

    public class InvisibleButtonCell : DataGridViewButtonCell
    {
        //public override object Clone()
        //{
        //    InvisibleButtonCell cell = (InvisibleButtonCell)base.Clone();
        //    return cell;
        //}

        protected override void Paint(Graphics graphics,
                                        Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
                                        DataGridViewElementStates elementState, object value,
                                        object formattedValue, string errorText,
                                        DataGridViewCellStyle cellStyle,
                                        DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                        DataGridViewPaintParts paintParts)
        {

            bool doShow = value != null && value.ToString() != "";
            if (!doShow)
            {
                // Draw the cell background, if specified.
		        if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                {
                    SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
                    graphics.FillRectangle(cellBackground, cellBounds);
                    cellBackground.Dispose();
                }

                // Draw the cell borders, if specified.
                if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);

            }
            else
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    elementState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts);
            }
        } //protected override void Paint( ...

    } //public class InvisibleButtonCell
}
