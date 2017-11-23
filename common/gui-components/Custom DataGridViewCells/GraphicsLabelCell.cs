using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace sakwa
{
    public class GraphicLabel : IComparable
    {
        public GraphicLabel() { }
        public GraphicLabel(Bitmap image, string text)
        {
            _Image = image;
            _Text = text;
        }

        public Bitmap Image { get { return _Image; } set { _Image = value; } }
        private Bitmap _Image = null;

        public string Text { get { return _Text; } set { _Text = value; } }
        private string _Text = "Text";

        new public string ToString() { return _Text; }


        public int CompareTo(object obj)
        {
            if(obj != null && obj is GraphicLabel)
            {
                GraphicLabel gl = obj as GraphicLabel;
                return _Text.CompareTo(gl.Text);

            } //if(obj != null && obj is GraphicLabel)

            return 1;

        } //public int CompareTo(object obj)
    } //public class GraphicLabel

    public class GraphicsLabelColumn : DataGridViewColumn
    {
        public GraphicsLabelColumn()
        {
            this.CellTemplate = new GraphicsLabelCell();
            this.ReadOnly = true;
        }

    } //public class GraphicsLabelColumn

    public class GraphicsLabelCell : DataGridViewTextBoxCell
    {
        protected override void Paint(Graphics graphics,
                                        Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
                                        DataGridViewElementStates elementState, object value,
                                        object formattedValue, string errorText,
                                        DataGridViewCellStyle cellStyle,
                                        DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                        DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, "", errorText, cellStyle, advancedBorderStyle, paintParts);
            ToolTipText = "";

            if (value != null)
            {
                GraphicLabel gl = (GraphicLabel)value;
                if (gl != null)
                {
                    int imgWidth = cellBounds.Height - cellStyle.Padding.Vertical;

                    Rectangle imgrect = new Rectangle(
                        cellBounds.X + cellStyle.Padding.Left,
                        cellBounds.Y + cellStyle.Padding.Top,
                        imgWidth,
                        imgWidth);

                    Rectangle rect = new Rectangle(
                        imgrect.Right + cellStyle.Padding.Horizontal,
                        cellBounds.Y + cellStyle.Padding.Top,
                        cellBounds.Width - (imgWidth + cellStyle.Padding.Horizontal),
                        cellBounds.Height - cellStyle.Padding.Vertical);

                    if (gl.Image != null)
                    {
                        double factor = Convert.ToDouble(gl.Image.Height) / Convert.ToDouble(gl.Image.Width);
                        if (factor < 1.0)
                        {
                            int orgHeight = imgrect.Height;
                            imgrect.Height = Convert.ToInt32(factor * Convert.ToDouble(imgrect.Height));
                            orgHeight -= imgrect.Height;
                            orgHeight /= 2;

                            imgrect.Y += orgHeight;

                        } //if (factor < 1.0)

                        if (factor > 1.0)
                        {
                            int orgWidth = imgrect.Width;
                            imgrect.Width = Convert.ToInt32(Convert.ToDouble(imgrect.Width) / factor);
                            orgWidth -= imgrect.Width;
                            orgWidth /= 2;

                            imgrect.X += orgWidth;

                        } //if (factor > 1.0)

                        graphics.DrawImage(gl.Image, imgrect);

                    } //if (gl.Image != null)

                    if (gl.Text != "")
                    {
                        Brush b = null;
                        if ((elementState & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
                            b = new SolidBrush(cellStyle.SelectionForeColor);
                        else
                            b = new SolidBrush(cellStyle.ForeColor); ;

                        StringFormat fmt = new StringFormat();
                        fmt.Alignment = StringAlignment.Near;
                        fmt.LineAlignment = StringAlignment.Center;
                        graphics.DrawString(gl.Text, cellStyle.Font, b, rect, fmt);

                        ToolTipText = gl.Text;
                    } //if (gl.Text != "")
                } //if (gl != null)
            } //if (value != null)
        } //protected override void Paint( ...

        public override string ToString() { return ""; }

      //
      // Summary:
      //     Creates a new accessible object for the System.Windows.Forms.DataGridViewImageCell.
      //
      // Returns:
      //     A new System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject
      //     for the System.Windows.Forms.DataGridViewImageCell.
        protected override AccessibleObject CreateAccessibilityInstance()
        {
           return new GraphicLabelCellAccessibleObject(this);
        }

// Summary:
      //     Provides information about a System.Windows.Forms.DataGridViewImageCell to
      //     accessibility client applications.
      //protected class DataGridViewImageCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
      protected class GraphicLabelCellAccessibleObject : DataGridViewCell.DataGridViewCellAccessibleObject
      {
         // Summary:
         //     Initializes a new instance of the System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject
         //     class.
         //
         // Parameters:
         //   owner:
         //     The System.Windows.Forms.DataGridViewCell that owns the System.Windows.Forms.DataGridViewImageCell.DataGridViewImageCellAccessibleObject.
         //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
         public GraphicLabelCellAccessibleObject(DataGridViewCell owner)  : base(owner) 
         {
         }

         // Summary:
         //     Gets a string representing the formatted value of the owning cell.
         //
         // Returns:
         //     A System.String representation of the cell value.
         //
         // Exceptions:
         //   System.InvalidOperationException:
         //     The value of the System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner
         //     property is null.
         public override string Value
         {
            get
            {
               GraphicsLabelCell cell = (GraphicsLabelCell)Owner;
               
                GraphicLabel gl = (GraphicLabel)cell.Value;
               string s = cell.Value.ToString();
               return gl.Text;
            }
         }
      }
    } //public class GraphicsLabelCell

}
