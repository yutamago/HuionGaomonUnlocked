// Decompiled with JetBrains decompiler
// Type: HuionTablet.View.HuionPictureBox
// Assembly: ViewLibrary, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 54D44D28-9DE2-41E1-9310-1856357D6EEC
// Assembly location: D:\Program Files (x86)\Huion Tablet\ViewLibrary.dll

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HuionTablet.View
{
    public class HuionPictureBox : PictureBox
    {
        private IContainer components;
        private Rectangle mPictrueRect;
        private Bitmap mPicture;

        public HuionPictureBox()
        {
            this.InitializeComponent();
        }

        public Bitmap Pictrue
        {
            set
            {
                this.mPicture = value;
                if (this.mPicture != null)
                    this.mPicture.MakeTransparent();
                this.Invalidate();
            }
            get { return this.mPicture; }
        }

        public Rectangle PictrueRect
        {
            set
            {
                this.mPictrueRect = value;
                this.Invalidate();
            }
            get { return this.mPictrueRect; }
        }

        public void setPictrueInfo(Bitmap picture, Rectangle rect)
        {
            this.Pictrue = picture;
            this.PictrueRect = rect;
        }

        public void setPictureInfo(PictureBox v)
        {
            this.Pictrue = new Bitmap(v.Image);
            this.PictrueRect = v.Bounds;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (this.Pictrue != null)
                pe.Graphics.DrawImage((Image) this.Pictrue, this.PictrueRect);
            base.OnPaint(pe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer) new Container();
        }
    }
}