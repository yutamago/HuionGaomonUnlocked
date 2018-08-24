// Decompiled with JetBrains decompiler
// Type: HuionTablet.View.HuionPictureBox
// Assembly: HuionView, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12DF49B7-B0D7-4CEC-B4EC-DDBD1DF4FB9A
// Assembly location: D:\Program Files (x86)\Huion Tablet\HuionView.dll

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