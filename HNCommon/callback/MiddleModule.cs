// Decompiled with JetBrains decompiler
// Type: HuionTablet.MiddleModule
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Windows.Forms;

namespace HuionTablet
{
  public class MiddleModule
  {
    public static Form mMainForm;

    public static event Send eventSend;

    public static event Post eventPost;

    public static event SendSreenNum eventSendSreenNum;

    public static void initMainFormHandle(Form f)
    {
      MiddleModule.mMainForm = f;
    }

    public static void postConnectionMessage()
    {
      MiddleModule.mMainForm.Invoke((Delegate) new HuionTablet.utils.Void(MiddleModule.invokePostConnectionMessage));
    }

    private static void invokePostConnectionMessage()
    {
      HuionDriverDLL.PostMessage(MiddleModule.mMainForm.Handle, 1044, IntPtr.Zero, IntPtr.Zero);
    }

    public static void SendMessage(object sender, object msg)
    {
      // ISSUE: reference to a compiler-generated field
      if (MiddleModule.eventSend == null)
        return;
      // ISSUE: reference to a compiler-generated field
      MiddleModule.eventSend(sender, msg);
    }

    public static void PostMessage(object post, object msg)
    {
      // ISSUE: reference to a compiler-generated field
      if (MiddleModule.eventPost == null)
        return;
      // ISSUE: reference to a compiler-generated field
      MiddleModule.eventPost(post, msg);
    }

    public static void SendrMessageScreen(object senderScreen, object msg)
    {
      // ISSUE: reference to a compiler-generated field
      MiddleModule.eventSendSreenNum(senderScreen, msg);
    }
  }
}
