// Decompiled with JetBrains decompiler
// Type: HuionTablet.Fixer4TabletPen
// Assembly: Fixer, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0244B443-444F-4961-B0E5-29DA8D9959BB
// Assembly location: D:\Program Files (x86)\Huion Tablet\Fixer.dll

using HuionTablet.utils;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HuionTablet
{
  public class Fixer4TabletPen
  {
    public static void savePenButtonValue(int index, HNStruct.HNEkey value)
    {
      HNStruct.globalInfo.pbtns[index] = value;
      IntPtr ptr = HNStruct.globalInfo.userConfig.pbtns;
      switch (IntPtr.Size)
      {
        case 4:
          int index1 = 0;
          while (index1 < (int) HNStruct.globalInfo.tabletInfo.pbtnNum)
          {
            if (index1 == index)
            {
              HNStruct.globalInfo.pbtns[index1] = value;
              Marshal.StructureToPtr((object) HNStruct.globalInfo.pbtns[index1], ptr, false);
            }
            else
              Marshal.StructureToPtr((object) HNStruct.globalInfo.pbtns[index1], ptr, false);
            ++index1;
            ptr = new IntPtr(ptr.ToInt32() + Marshal.SizeOf(typeof (HNStruct.HNEkey)));
          }
          break;
        case 8:
          int index2 = 0;
          while (index2 < (int) HNStruct.globalInfo.tabletInfo.pbtnNum)
          {
            if (index2 == index)
            {
              HNStruct.globalInfo.pbtns[index2] = value;
              Marshal.StructureToPtr((object) HNStruct.globalInfo.pbtns[index2], ptr, false);
            }
            else
              Marshal.StructureToPtr((object) HNStruct.globalInfo.pbtns[index2], ptr, false);
            ++index2;
            ptr = new IntPtr(ptr.ToInt64() + (long) Marshal.SizeOf(typeof (HNStruct.HNEkey)));
          }
          break;
      }
    }

    public static void bGameCheckedChanged(object sender, EventArgs e)
    {
      byte num = Convert.ToByte(((CheckBox) sender).Checked);
      HNStruct.globalInfo.userConfig.bGame = num;
      Console.WriteLine(HNStruct.globalInfo.userConfig.bGame.ToString());
    }

    public static void inkCheckedChanged(object sender, EventArgs e)
    {
      byte num = Convert.ToByte(((CheckBox) sender).Checked);
      HNStruct.globalInfo.userConfig.bTabletpc = num;
    }

    public static void wintabCheckedChanged(object sender, EventArgs e)
    {
      byte num = Convert.ToByte(((CheckBox) sender).Checked);
      HNStruct.globalInfo.userConfig.bImproveLinearity = num;
    }

    public static void onMouseModeChanged(object sender, EventArgs e)
    {
      CheckBox checkBox = (CheckBox) sender;
      HNStruct.globalInfo.userConfig.bAbsoluteMode = checkBox.Checked ? (byte) 0 : (byte) 1;
    }

    public static uint calibratePressVal(HNStruct.HNConfig cfg, uint psVal, uint maxP)
    {
      try
      {
        switch (cfg.pressFactor)
        {
          case -4:
            int num1 = (int) HuionDriverDLL.hnc_equation_power((double) psVal, 0.125, (double) maxP);
            return HuionDriverDLL.hnc_equation_power((double) psVal, 0.125, (double) maxP);
          case -3:
            int num2 = (int) HuionDriverDLL.hnc_equation_power((double) psVal, 0.25, (double) maxP);
            return HuionDriverDLL.hnc_equation_power((double) psVal, 0.25, (double) maxP);
          case -2:
            int num3 = (int) HuionDriverDLL.hnc_equation_circle((double) psVal, (double) maxP, (double) maxP, 1);
            return HuionDriverDLL.hnc_equation_circle((double) psVal, (double) maxP, (double) maxP, 1);
          case -1:
            int num4 = (int) HuionDriverDLL.hnc_equation_circle((double) psVal, (double) (2U * maxP), (double) maxP, 1);
            return HuionDriverDLL.hnc_equation_circle((double) psVal, (double) (2U * maxP), (double) maxP, 1);
          case 1:
            int num5 = (int) HuionDriverDLL.hnc_equation_circle((double) psVal, (double) (2U * maxP), (double) maxP, 0);
            return HuionDriverDLL.hnc_equation_circle((double) psVal, (double) (2U * maxP), (double) maxP, 0);
          case 2:
            int num6 = (int) HuionDriverDLL.hnc_equation_circle((double) psVal, (double) maxP, (double) maxP, 0);
            return HuionDriverDLL.hnc_equation_circle((double) psVal, (double) maxP, (double) maxP, 0);
          case 3:
            int num7 = (int) HuionDriverDLL.hnc_equation_power((double) psVal, 4.0, (double) maxP);
            return HuionDriverDLL.hnc_equation_power((double) psVal, 4.0, (double) maxP);
          case 4:
            int num8 = (int) HuionDriverDLL.hnc_equation_power((double) psVal, 8.0, (double) maxP);
            return HuionDriverDLL.hnc_equation_power((double) psVal, 8.0, (double) maxP);
        }
      }
      catch (Exception ex)
      {
        HuionLog.saveLog("calibratePressValue", ex.Message);
        HuionLog.saveLog("calibratePressValue", ex.StackTrace);
      }
      return psVal;
    }

    public static Image getPenImage()
    {
      return (Image) HuionRender.blowupImage(ImageHelper.getDllImage(Marshal.PtrToStringUni(HuionDriverDLL.hnc_get_pen_image((HnConst.HNTabletType) HNStruct.globalInfo.tabletInfo.devType))), DpiHelper.getInstance().XDpi);
    }
  }
}
