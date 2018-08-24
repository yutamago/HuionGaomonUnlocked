// Decompiled with JetBrains decompiler
// Type: HuionTablet.HnConst
// Assembly: HNCommon, Version=14.4.7.4, Culture=neutral, PublicKeyToken=null
// MVID: 25752B5D-65A2-4F38-BCC4-D8B7ED057FB9
// Assembly location: D:\Program Files (x86)\Huion Tablet\HNCommon.dll

namespace HuionTablet
{
  public class HnConst
  {
    public const ushort HN_VID = 9580;
    public const ushort HN_PID = 110;
    public const int HN_SCREEN_DIV_NUM = 20;
    public const int MAX_KBTNS = 16;
    public const int HN_MAX_NUM_HEKEY = 16;
    public const int HN_MAX_NUM_SEKEY = 16;
    public const int HN_MAX_NUM_PEKEY = 16;
    public const int HN_MAX_NUM_MEKEY = 2;
    public const int HN_MAX_NUM_MEKEY_EKEY = 16;
    public const int HN_MAX_NUM_MEKEY_MUX = 4;
    public const int HN_MAX_NUM_CONTEXT = 8;
    public const int HN_MAX_LEN_IDEK = 12;
    public const string HN_Description = "Graphics Tablet Device";

    public enum HNEkeyFuncType
    {
      HNEKT_NONE = 0,
      HNEKT_SWITCH_DISPLAY = 1,
      HNEKT_SWITCH_BRUSH = 2,
      HNEKT_KBTN = 4,
      HNEKT_EXEC = 8,
      HNEKT_MOUSE_LEFT = 16, // 0x00000010
      HNEKT_MOUSE_MID = 32, // 0x00000020
      HNEKT_MOUSE_RIGHT = 64, // 0x00000040
      HNEKT_MOUSE_WHEEL_FORWARD = 128, // 0x00000080
      HNEKT_MOUSE_WHEEL_BACKWARD = 256, // 0x00000100
    }

    public enum HNOEMType
    {
      HNOT_UNKNOW,
      HNOT_NOBRAND,
      HNOT_HUION,
      HNOT_GAOMON,
      HNOT_XINDONGFANG,
      HNOT_XINSHENGLAN,
      HNOT_HONGSHULIN,
      HNOT_FANTUO,
      HNOT_YOUSHANG,
      HNOT_YINENG,
      HNOT_SHIJUN,
      HNOT_LEISAI,
      HNOT_KJC,
      HNOT_MENGTIAN,
      HNOT_ZLZH,
    }

    public enum HNTabletType
    {
      HNTT_UNKNOW,
      HNTT_HUION_T141,
      HNTT_HUION_T152,
      HNTT_HUION_T153,
      HNTT_HUION_T154,
      HNTT_HUION_T155,
      HNTT_HUION_T156,
      HNTT_HUION_T161,
      HNTT_HUION_T162,
      HNTT_HUION_T163,
      HNTT_HUION_T164,
      HNTT_HUION_T165,
      HNTT_HUION_T166,
      HNTT_HUION_T167,
      HNTT_HUION_T171,
      HNTT_HUION_T172,
      HNTT_HUION_T173,
      HNTT_HUION_T174,
      HNTT_HUION_T175,
      HNTT_HUION_T176,
      HNTT_HUION_M161,
      HNTT_HUION_M162,
      HNTT_HUION_M163,
      HNTT_HUION_M164,
      HNTT_HUION_M165,
      HNTT_HUION_M167,
      HNTT_HUION_M168,
      HNTT_HUION_M171,
      HNTT_HUION_M174,
      HNTT_HUION_M175,
      HNTT_OEM02_T151,
      HNTT_OEM02_T156,
      HNTT_OEM02_T166,
      HNTT_OEM02_T171,
      HNTT_OEM02_T174,
      HNTT_OEM02_T178,
      HNTT_OEM02_T17b,
      HNTT_OEM02_T17c,
      HNTT_OEM02_M164,
      HNTT_OEM02_M165,
      HNTT_OEM02_M166,
      HNTT_OEM02_M171,
      HNTT_OEM02_M172,
      HNTT_OEM02_M177,
      HNTT_OEM00_M166,
      HNTT_OEM00_T172,
      HNTT_OEM00_T173,
      HNTT_OEM07_T168,
      HNTT_OEM08_T169,
      HNTT_OEM08_T171,
      HNTT_OEM06_T165,
      HNTT_OEM10_M173,
      HNTT_OEM12_M161,
      HNTT_OEM11_T17d,
      HNTT_HUION_M181,
      HNTT_HUION_M182,
      HNTT_HUION_M183,
      HNTT_HUION_M184,
      HNTT_HUION_M187,
      HNTT_HUION_M189,
      HNTT_HUION_M18a,
      HNTT_HUION_M18b,
      HNTT_HUION_M18c,
      HNTT_HUION_M18d,
      HNTT_OEM02_M185,
      HNTT_OEM02_M186,
      HNTT_OEM02_M187,
      HNTT_OEM02_M188,
      HNTT_OEM05_M181,
      HNTT_HUION_T181,
      HNTT_HUION_T182,
      HNTT_HUION_T184,
      HNTT_HUION_T185,
      HNTT_HUION_T186,
      HNTT_HUION_T187,
      HNTT_HUION_T188,
      HNTT_HUION_T189,
      HNTT_HUION_T18a,
      HNTT_OEM02_T183,
      HNTT_OEM00_T165,
    }

    public enum HNPenType
    {
      HNPT_UNKNOW,
      HNPT_HUION_P161,
      HNPT_HUION_P162,
      HNPT_HUION_P151,
      HNPT_HUION_P152,
      HNPT_HUION_P153,
      HNPT_HUION_P171,
      HNPT_HUION_P172,
      HNPT_HUION_P173,
      HNPT_HUION_P174,
      HNPT_HUION_P175,
      HNPT_HUION_P176,
      HNPT_HUION_P177,
      HNPT_HUION_P178,
      HNPT_HUION_P179,
      HNPT_HUION_P17a,
      HNPT_HUION_P17b,
      HNPT_OEM07_P163,
      HNPT_HUION_P180,
      HNPT_HUION_P181,
      HNPT_HUION_P183,
      HNPT_HUION_P184,
      HNPT_OEM10_P182,
      HNPT_OEM00_P160,
    }

    public enum HNReportDataType
    {
      HNRDT_PEN_HOVER = 128, // 0x00000080
      HNRDT_PEN_PRESS = 129, // 0x00000081
      HNRDT_PEN_BTN_BELOW = 130, // 0x00000082
      HNRDT_PEN_PRESS_BTN_BELOW = 131, // 0x00000083
      HNRDT_PEN_BTN_ABOVE = 132, // 0x00000084
      HNRDT_PEN_PRESS_BTN_ABOVE = 133, // 0x00000085
      HNRDT_KEY_TOUCH_MOVE = 144, // 0x00000090
      HNRDT_KEY_HBTN = 224, // 0x000000E0
      HNRDT_KEY_TOUCH = 225, // 0x000000E1
      HNRDT_KEY_SLIDER = 240, // 0x000000F0
    }

    public enum HNEkeyEventType
    {
      HNEKEVT_NONE,
      HNEKEVT_UP,
      HNEKEVT_DOWN,
      HNEKEVT_CLICK,
    }
  }
}
