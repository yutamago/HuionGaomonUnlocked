// Decompiled with JetBrains decompiler
// Type: HuionTablet.view.ViewUtils
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Drawing;
using System.Windows.Forms;
using HuionTablet.Entity;

namespace HuionTablet.view
{
    internal class ViewUtils
    {
        public static MouseLocationType locationType(Rectangle rect, Point mouseLocation)
        {
            int num = 3;
            MouseLocationType mouseLocationType = MouseLocationType.EXTERNAL;
            if (Math.Abs(mouseLocation.X - rect.Left) < num)
                mouseLocationType |= MouseLocationType.LEFT;
            if (Math.Abs(mouseLocation.X - rect.Right) < num)
                mouseLocationType |= MouseLocationType.RIGHT;
            if (Math.Abs(mouseLocation.Y - rect.Top) < num)
                mouseLocationType |= MouseLocationType.TOP;
            if (Math.Abs(mouseLocation.Y - rect.Bottom) < num)
                mouseLocationType |= MouseLocationType.BOTTOM;
            if (mouseLocation.X > rect.Left + num && mouseLocation.X < rect.Right - num &&
                (mouseLocation.Y > rect.Top + num && mouseLocation.Y < rect.Bottom - num))
                mouseLocationType = MouseLocationType.INNER;
            return mouseLocationType;
        }

        public static Cursor getCursor8LocationType(MouseLocationType type, Cursor cursor)
        {
            switch (type)
            {
                case MouseLocationType.LEFT:
                case MouseLocationType.RIGHT:
                    return Cursors.SizeWE;
                case MouseLocationType.TOP:
                case MouseLocationType.BOTTOM:
                    return Cursors.SizeNS;
                case MouseLocationType.INNER:
                    return Cursors.SizeAll;
                default:
                    if ((type & MouseLocationType.LEFT) == MouseLocationType.LEFT)
                    {
                        if ((type & MouseLocationType.TOP) == MouseLocationType.TOP)
                            return Cursors.SizeNWSE;
                        if ((type & MouseLocationType.BOTTOM) == MouseLocationType.BOTTOM)
                            return Cursors.SizeNESW;
                    }
                    else if ((type & MouseLocationType.RIGHT) == MouseLocationType.RIGHT)
                    {
                        if ((type & MouseLocationType.TOP) == MouseLocationType.TOP)
                            return Cursors.SizeNESW;
                        if ((type & MouseLocationType.BOTTOM) == MouseLocationType.BOTTOM)
                            return Cursors.SizeNWSE;
                    }

                    return cursor;
            }
        }
    }
}