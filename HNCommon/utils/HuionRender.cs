// Decompiled with JetBrains decompiler
// Type: HuionTablet.utils.HuionRender
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace HuionTablet.utils
{
    public class HuionRender
    {
        public static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180f, 90f);
            graphicsPath.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            graphicsPath.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2,
                270f, 90f);
            graphicsPath.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right,
                rect.Y + rect.Height - cornerRadius * 2);
            graphicsPath.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2,
                cornerRadius * 2, cornerRadius * 2, 0.0f, 90f);
            graphicsPath.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            graphicsPath.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90f, 90f);
            graphicsPath.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        public static Bitmap resizeImage(Bitmap bmp, int newW, int newH)
        {
            try
            {
                Bitmap bitmap = new Bitmap(newW, newH);
                Graphics graphics = Graphics.FromImage((Image) bitmap);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.DrawImage((Image) bmp, new Rectangle(0, 0, newW, newH),
                    new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                graphics.Dispose();
                return bitmap;
            }
            catch
            {
                return (Bitmap) null;
            }
        }

        public static Bitmap compressImageWithRate(Bitmap bmp, int maxW, int maxH)
        {
            if (bmp == null || bmp.Width <= maxW && bmp.Height <= maxH)
                return bmp;
            float num = Math.Max((float) bmp.Width / (float) maxW, (float) bmp.Height / (float) maxH);
            int newW = (int) ((double) bmp.Width / (double) num);
            int newH = (int) ((double) bmp.Height / (double) num);
            return resizeImage(bmp, newW, newH);
        }

        public static Bitmap blowupImage(Bitmap bmp, float rate)
        {
            if (bmp == null || (double) rate == 1.0)
                return bmp;
            int newW = (int) ((double) bmp.Width * (double) rate);
            int newH = (int) ((double) bmp.Height * (double) rate);
            return resizeImage(bmp, newW, newH);
        }

        public static Bitmap rotateImageCenter(Bitmap bmp, float angle, Color bkColor)
        {
            if (bmp == null)
                return (Bitmap) null;
            int width = bmp.Width;
            int height = bmp.Height;
            PixelFormat format = !(bkColor == Color.Transparent) ? bmp.PixelFormat : PixelFormat.Format32bppArgb;
            Bitmap bitmap1 = new Bitmap(width, height, format);
            Graphics graphics1 = Graphics.FromImage((Image) bitmap1);
            graphics1.Clear(bkColor);
            graphics1.DrawImageUnscaled((Image) bmp, 0, 0);
            graphics1.Dispose();
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddRectangle(new RectangleF(0.0f, 0.0f, (float) width, (float) height));
            Matrix matrix = new Matrix();
            matrix.Rotate(angle);
            RectangleF bounds = graphicsPath.GetBounds(matrix);
            Bitmap bitmap2 = new Bitmap((int) bounds.Width, (int) bounds.Height, format);
            Graphics graphics2 = Graphics.FromImage((Image) bitmap2);
            graphics2.Clear(bkColor);
            graphics2.TranslateTransform(-bounds.X, -bounds.Y);
            graphics2.RotateTransform(angle);
            graphics2.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics2.DrawImageUnscaled((Image) bitmap1, 0, 0);
            graphics2.Dispose();
            bitmap1.Dispose();
            return bitmap2;
        }
    }
}