// Decompiled with JetBrains decompiler
// Type: HuionTablet.MiddleModule
// Assembly: HNCommon, Version=14.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: F61A447E-F5B9-4160-AD25-173BA5066379
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

using System;
using System.Windows.Forms;
using Void = HuionTablet.utils.Void;

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
            mMainForm = f;
        }

        public static void postConnectionMessage()
        {
            mMainForm.Invoke((Delegate) new Void(invokePostConnectionMessage));
        }

        private static void invokePostConnectionMessage()
        {
            HuionDriverDLL.PostMessage(mMainForm.Handle, 1044, IntPtr.Zero, IntPtr.Zero);
        }

        public static void SendMessage(object sender, object msg)
        {
            // ISSUE: reference to a compiler-generated field
            if (eventSend == null)
                return;
            // ISSUE: reference to a compiler-generated field
            eventSend(sender, msg);
        }

        public static void PostMessage(object post, object msg)
        {
            // ISSUE: reference to a compiler-generated field
            if (eventPost == null)
                return;
            // ISSUE: reference to a compiler-generated field
            eventPost(post, msg);
        }

        public static void SendrMessageScreen(object senderScreen, object msg)
        {
            // ISSUE: reference to a compiler-generated field
            eventSendSreenNum(senderScreen, msg);
        }
    }
}