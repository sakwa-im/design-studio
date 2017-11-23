using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace sakwa
{
public class GraphicSupport
{

    public static Bitmap Recolor(Bitmap image, Color replaceColor, Color newColor)
    {
        if (image != null)
        {
            ColorMap colorMap = new ColorMap();
            colorMap.OldColor = replaceColor;
            colorMap.NewColor = newColor;

            ColorMap[] remapTable = { colorMap };
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetRemapTable(new ColorMap[] { colorMap }, ColorAdjustType.Bitmap);
            Graphics g = Graphics.FromImage(image);
            g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                0, 0,
                image.Width, image.Height,
                GraphicsUnit.Pixel,
                imageAttributes);

        } //if (image != null)

        return image;

    } //public static Bitmap Recolor( ...

    public static Image RotateImage(Image image, float degrees)
    {
        if (image != null)
        {
            #region Rotate in chunks of 90 degrees
            if (degrees >= 90.0F && degrees < 180.0F)
            {
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                degrees -= 90.0F;

            }
            else
                if (degrees >= 180.0F && degrees < 270.0F)
                {
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    degrees -= 180.0F;

                }
                else
                    if (degrees >= 270.0F && degrees < 360.0)
                    {
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        degrees -= 270.0F;

                    }
            #endregion
            if (degrees > 0.0F)
            {
                #region Calulate required size
                double angle = (Math.PI * degrees) / 180.0;
                int ah = Convert.ToInt16(Math.Cos(angle) * Convert.ToSingle(image.Height));
                int oh = Convert.ToInt16(Math.Sin(angle) * Convert.ToSingle(image.Height));

                int aw = Convert.ToInt16(Math.Cos(angle) * Convert.ToSingle(image.Width));
                int ow = Convert.ToInt16(Math.Sin(angle) * Convert.ToSingle(image.Width));
                #endregion
                #region Create resulting bitmap
                Bitmap result = new Bitmap(oh + aw, ah + ow);
                Graphics g = Graphics.FromImage(result);
                #endregion

                g.DrawImage(image, new Point[] { new Point(oh, 0), new Point(oh + aw, ow), new Point(0, ah) });

                return result;

            } //if (degrees > 0.0F)
        } //if (image != null)

        return image;

    } //public static Image RotateImage(Image image, float degrees)

    public static Image CropImage(Image image, int width, int height)
    {
        if (image != null)
        {
            Bitmap result = new Bitmap(width, height);

            Graphics g = Graphics.FromImage(result);
            g.DrawImage(image, 0, 0);

            return result;

        } //if (image != null)

        return image;

    } //public static Image CropImage(Image image, int width, int height)

    public static Bitmap ToneDown(float opacity, Bitmap image, GraphicsPath path = null)
    {
        ImageAttributes imageAttributes = new ImageAttributes();
        int width = image.Width;
        int height = image.Height;

        float[][] colorMatrixElements = { 
            new float[] {opacity,  0,  0,  0, 0},
            new float[] {0,  opacity,  0,  0, 0},
            new float[] {0,  0,  opacity,  0, 0},
            new float[] {0,  0,  0,  1F, 0},
            new float[] {0, 0, 0, 0, 1F}};

        ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

        imageAttributes.SetColorMatrix(
            colorMatrix,
            ColorMatrixFlag.Default,
            ColorAdjustType.Bitmap);

        Graphics g = Graphics.FromImage(image);

        if (path != null)
            g.Clip = new Region(path);

        g.DrawImage(
            image,
            new Rectangle(0, 0, width, height),  // destination rectangle 
            0, 0,        // upper-left corner of source rectangle 
            width,       // width of source rectangle
            height,      // height of source rectangle
            GraphicsUnit.Pixel,
            imageAttributes);

        return image;

    } //public static Bitmap ToneDown( ...

    public static Bitmap AdjustBrightness(float brightness, Bitmap image, GraphicsPath path = null)
    {
        ImageAttributes imageAttributes = new ImageAttributes();
        int width = image.Width;
        int height = image.Height;

        brightness /= 255.0F;

        float[][] colorMatrixElements = { 
            new float[] {1,  0,  0,  0, 0},
            new float[] {0,  1,  0,  0, 0},
            new float[] {0,  0,  1,  0, 0},
            new float[] {0,  0,  0,  1, 0},
            new float[] {brightness, brightness, brightness, 0, 1}};

        ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

        imageAttributes.SetColorMatrix(
            colorMatrix,
            ColorMatrixFlag.Default,
            ColorAdjustType.Bitmap);

        Graphics g = Graphics.FromImage(image);
        g.SmoothingMode = SmoothingMode.HighQuality;

        if (path != null)
            g.Clip = new Region(path);

        g.DrawImage(
            image,
            new Rectangle(0, 0, width, height),  // destination rectangle 
            0, 0,        // upper-left corner of source rectangle 
            width,       // width of source rectangle
            height,      // height of source rectangle
            GraphicsUnit.Pixel,
            imageAttributes);

        return image;

    } //public static Bitmap AdjustBrightness( ...

    public enum RoundedTypes { Transparent, None, Left, Top, Right, Bottom, Full, TopLeft, TopRight, BottomRight, BottomLeft }
    public static Bitmap DrawRoundedBox(Bitmap image, Rectangle bounds, 
        RoundedTypes type, int radius, float lineWidth, 
        Color fillColor, Color borderColor)
    {
        Graphics g = Graphics.FromImage(image);
        GraphicsPath path = new GraphicsPath();
        Pen pen = new Pen(borderColor, lineWidth);

        if (type != RoundedTypes.Transparent)
        {
            pen.EndCap = pen.StartCap = LineCap.Round;
            path.StartFigure();
            switch (type)
            {
                case RoundedTypes.Left:
                    /*Left top*/
                    path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                    /*Right side*/
                    path.AddLine(bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
                    /*Left bottom*/
                    path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
                    break;

                case RoundedTypes.Top:
                    /*Left top*/
                    path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                    /*Right top*/
                    path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
                    /*Bottom side*/
                    path.AddLine(bounds.X + bounds.Width, bounds.Y + bounds.Height, bounds.X, bounds.Y + bounds.Height);
                    break;

                case RoundedTypes.Right:
                    /*Right top*/
                    path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
                    /*Right bottom*/
                    path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius, radius, radius, 0, 90);
                    /*Left side*/
                    path.AddLine(bounds.X, bounds.Y + bounds.Height, bounds.X, bounds.Y);
                    break;

                case RoundedTypes.Bottom:
                    /*Top side*/
                    path.AddLine(bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y);
                    /*Right bottom*/
                    path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius, radius, radius, 0, 90);
                    /*Left bottom*/
                    path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
                    break;

                case RoundedTypes.Full:
                    /*Left top*/
                    path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                    /*Right top*/
                    path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
                    /*Right bottom*/
                    path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius, radius, radius, 0, 90);
                    /*Left bottom*/
                    path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
                    break;

                case RoundedTypes.None:
                    /*Top side*/
                    path.AddLine(bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y);
                    /*Right side*/
                    path.AddLine(bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
                    /*Bottom side*/
                    path.AddLine(bounds.X + bounds.Width, bounds.Y + bounds.Height, bounds.X, bounds.Y + bounds.Height);
                    /*Left side*/
                    path.AddLine(bounds.X, bounds.Y + bounds.Height, bounds.X, bounds.Y);
                    break;

                case RoundedTypes.TopLeft:
                    /*Left top*/
                    path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                    /*Right side*/
                    path.AddLine(bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
                    /*Bottom side*/
                    path.AddLine(bounds.X + bounds.Width, bounds.Y + bounds.Height, bounds.X, bounds.Y + bounds.Height);
                    break;

                case RoundedTypes.TopRight:
                    /*Right top*/
                    path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
                    /*Right side*/
                    path.AddLine(bounds.X + bounds.Width, bounds.Y + radius, bounds.X + bounds.Width, bounds.Y + bounds.Height);
                    /*Bottom side*/
                    path.AddLine(bounds.X + bounds.Width, bounds.Y + bounds.Height, bounds.X, bounds.Y + bounds.Height);
                    break;

                case RoundedTypes.BottomLeft:
                    /*Top side*/
                    path.AddLine(bounds.X, bounds.Y, bounds.X + bounds.Width, bounds.Y);
                    /*Right side*/
                    path.AddLine(bounds.X + bounds.Width, bounds.Y, bounds.X + bounds.Width, bounds.Y + bounds.Height);
                    /*Left bottom*/
                    path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
                    break;

                case RoundedTypes.BottomRight:
                    /*Left side*/
                    path.AddLine(bounds.X, bounds.Y, bounds.X, bounds.Y + bounds.Height);
                    /*Bottom side*/
                    path.AddLine(bounds.X, bounds.Y + bounds.Height, bounds.X + bounds.Width - radius, bounds.Y + bounds.Height);
                    /*Right bottom*/
                    path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius, radius, radius, 0, 90);
                    break;

            }
            path.CloseAllFigures();

            g.SmoothingMode = SmoothingMode.HighQuality;

            g.FillPath(new SolidBrush(fillColor), path);
            g.DrawPath(pen, path);

        }
        else
            g.FillRectangle(new SolidBrush(fillColor), bounds);

        return image;

    } //public void DrawRoundedBox( ...

} //public class GraphicSupport
}
