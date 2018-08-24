// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlEntity
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlAgilityPack
{
  public class HtmlEntity
  {
    private static Dictionary<int, string> _entityName = new Dictionary<int, string>();
    private static Dictionary<string, int> _entityValue = new Dictionary<string, int>();
    private static readonly int _maxEntitySize;

    public static Dictionary<int, string> EntityName
    {
      get
      {
        return HtmlEntity._entityName;
      }
    }

    public static Dictionary<string, int> EntityValue
    {
      get
      {
        return HtmlEntity._entityValue;
      }
    }

    static HtmlEntity()
    {
      HtmlEntity._entityValue.Add("nbsp", 160);
      HtmlEntity._entityName.Add(160, "nbsp");
      HtmlEntity._entityValue.Add("iexcl", 161);
      HtmlEntity._entityName.Add(161, "iexcl");
      HtmlEntity._entityValue.Add("cent", 162);
      HtmlEntity._entityName.Add(162, "cent");
      HtmlEntity._entityValue.Add("pound", 163);
      HtmlEntity._entityName.Add(163, "pound");
      HtmlEntity._entityValue.Add("curren", 164);
      HtmlEntity._entityName.Add(164, "curren");
      HtmlEntity._entityValue.Add("yen", 165);
      HtmlEntity._entityName.Add(165, "yen");
      HtmlEntity._entityValue.Add("brvbar", 166);
      HtmlEntity._entityName.Add(166, "brvbar");
      HtmlEntity._entityValue.Add("sect", 167);
      HtmlEntity._entityName.Add(167, "sect");
      HtmlEntity._entityValue.Add("uml", 168);
      HtmlEntity._entityName.Add(168, "uml");
      HtmlEntity._entityValue.Add("copy", 169);
      HtmlEntity._entityName.Add(169, "copy");
      HtmlEntity._entityValue.Add("ordf", 170);
      HtmlEntity._entityName.Add(170, "ordf");
      HtmlEntity._entityValue.Add("laquo", 171);
      HtmlEntity._entityName.Add(171, "laquo");
      HtmlEntity._entityValue.Add("not", 172);
      HtmlEntity._entityName.Add(172, "not");
      HtmlEntity._entityValue.Add("shy", 173);
      HtmlEntity._entityName.Add(173, "shy");
      HtmlEntity._entityValue.Add("reg", 174);
      HtmlEntity._entityName.Add(174, "reg");
      HtmlEntity._entityValue.Add("macr", 175);
      HtmlEntity._entityName.Add(175, "macr");
      HtmlEntity._entityValue.Add("deg", 176);
      HtmlEntity._entityName.Add(176, "deg");
      HtmlEntity._entityValue.Add("plusmn", 177);
      HtmlEntity._entityName.Add(177, "plusmn");
      HtmlEntity._entityValue.Add("sup2", 178);
      HtmlEntity._entityName.Add(178, "sup2");
      HtmlEntity._entityValue.Add("sup3", 179);
      HtmlEntity._entityName.Add(179, "sup3");
      HtmlEntity._entityValue.Add("acute", 180);
      HtmlEntity._entityName.Add(180, "acute");
      HtmlEntity._entityValue.Add("micro", 181);
      HtmlEntity._entityName.Add(181, "micro");
      HtmlEntity._entityValue.Add("para", 182);
      HtmlEntity._entityName.Add(182, "para");
      HtmlEntity._entityValue.Add("middot", 183);
      HtmlEntity._entityName.Add(183, "middot");
      HtmlEntity._entityValue.Add("cedil", 184);
      HtmlEntity._entityName.Add(184, "cedil");
      HtmlEntity._entityValue.Add("sup1", 185);
      HtmlEntity._entityName.Add(185, "sup1");
      HtmlEntity._entityValue.Add("ordm", 186);
      HtmlEntity._entityName.Add(186, "ordm");
      HtmlEntity._entityValue.Add("raquo", 187);
      HtmlEntity._entityName.Add(187, "raquo");
      HtmlEntity._entityValue.Add("frac14", 188);
      HtmlEntity._entityName.Add(188, "frac14");
      HtmlEntity._entityValue.Add("frac12", 189);
      HtmlEntity._entityName.Add(189, "frac12");
      HtmlEntity._entityValue.Add("frac34", 190);
      HtmlEntity._entityName.Add(190, "frac34");
      HtmlEntity._entityValue.Add("iquest", 191);
      HtmlEntity._entityName.Add(191, "iquest");
      HtmlEntity._entityValue.Add("Agrave", 192);
      HtmlEntity._entityName.Add(192, "Agrave");
      HtmlEntity._entityValue.Add("Aacute", 193);
      HtmlEntity._entityName.Add(193, "Aacute");
      HtmlEntity._entityValue.Add("Acirc", 194);
      HtmlEntity._entityName.Add(194, "Acirc");
      HtmlEntity._entityValue.Add("Atilde", 195);
      HtmlEntity._entityName.Add(195, "Atilde");
      HtmlEntity._entityValue.Add("Auml", 196);
      HtmlEntity._entityName.Add(196, "Auml");
      HtmlEntity._entityValue.Add("Aring", 197);
      HtmlEntity._entityName.Add(197, "Aring");
      HtmlEntity._entityValue.Add("AElig", 198);
      HtmlEntity._entityName.Add(198, "AElig");
      HtmlEntity._entityValue.Add("Ccedil", 199);
      HtmlEntity._entityName.Add(199, "Ccedil");
      HtmlEntity._entityValue.Add("Egrave", 200);
      HtmlEntity._entityName.Add(200, "Egrave");
      HtmlEntity._entityValue.Add("Eacute", 201);
      HtmlEntity._entityName.Add(201, "Eacute");
      HtmlEntity._entityValue.Add("Ecirc", 202);
      HtmlEntity._entityName.Add(202, "Ecirc");
      HtmlEntity._entityValue.Add("Euml", 203);
      HtmlEntity._entityName.Add(203, "Euml");
      HtmlEntity._entityValue.Add("Igrave", 204);
      HtmlEntity._entityName.Add(204, "Igrave");
      HtmlEntity._entityValue.Add("Iacute", 205);
      HtmlEntity._entityName.Add(205, "Iacute");
      HtmlEntity._entityValue.Add("Icirc", 206);
      HtmlEntity._entityName.Add(206, "Icirc");
      HtmlEntity._entityValue.Add("Iuml", 207);
      HtmlEntity._entityName.Add(207, "Iuml");
      HtmlEntity._entityValue.Add("ETH", 208);
      HtmlEntity._entityName.Add(208, "ETH");
      HtmlEntity._entityValue.Add("Ntilde", 209);
      HtmlEntity._entityName.Add(209, "Ntilde");
      HtmlEntity._entityValue.Add("Ograve", 210);
      HtmlEntity._entityName.Add(210, "Ograve");
      HtmlEntity._entityValue.Add("Oacute", 211);
      HtmlEntity._entityName.Add(211, "Oacute");
      HtmlEntity._entityValue.Add("Ocirc", 212);
      HtmlEntity._entityName.Add(212, "Ocirc");
      HtmlEntity._entityValue.Add("Otilde", 213);
      HtmlEntity._entityName.Add(213, "Otilde");
      HtmlEntity._entityValue.Add("Ouml", 214);
      HtmlEntity._entityName.Add(214, "Ouml");
      HtmlEntity._entityValue.Add("times", 215);
      HtmlEntity._entityName.Add(215, "times");
      HtmlEntity._entityValue.Add("Oslash", 216);
      HtmlEntity._entityName.Add(216, "Oslash");
      HtmlEntity._entityValue.Add("Ugrave", 217);
      HtmlEntity._entityName.Add(217, "Ugrave");
      HtmlEntity._entityValue.Add("Uacute", 218);
      HtmlEntity._entityName.Add(218, "Uacute");
      HtmlEntity._entityValue.Add("Ucirc", 219);
      HtmlEntity._entityName.Add(219, "Ucirc");
      HtmlEntity._entityValue.Add("Uuml", 220);
      HtmlEntity._entityName.Add(220, "Uuml");
      HtmlEntity._entityValue.Add("Yacute", 221);
      HtmlEntity._entityName.Add(221, "Yacute");
      HtmlEntity._entityValue.Add("THORN", 222);
      HtmlEntity._entityName.Add(222, "THORN");
      HtmlEntity._entityValue.Add("szlig", 223);
      HtmlEntity._entityName.Add(223, "szlig");
      HtmlEntity._entityValue.Add("agrave", 224);
      HtmlEntity._entityName.Add(224, "agrave");
      HtmlEntity._entityValue.Add("aacute", 225);
      HtmlEntity._entityName.Add(225, "aacute");
      HtmlEntity._entityValue.Add("acirc", 226);
      HtmlEntity._entityName.Add(226, "acirc");
      HtmlEntity._entityValue.Add("atilde", 227);
      HtmlEntity._entityName.Add(227, "atilde");
      HtmlEntity._entityValue.Add("auml", 228);
      HtmlEntity._entityName.Add(228, "auml");
      HtmlEntity._entityValue.Add("aring", 229);
      HtmlEntity._entityName.Add(229, "aring");
      HtmlEntity._entityValue.Add("aelig", 230);
      HtmlEntity._entityName.Add(230, "aelig");
      HtmlEntity._entityValue.Add("ccedil", 231);
      HtmlEntity._entityName.Add(231, "ccedil");
      HtmlEntity._entityValue.Add("egrave", 232);
      HtmlEntity._entityName.Add(232, "egrave");
      HtmlEntity._entityValue.Add("eacute", 233);
      HtmlEntity._entityName.Add(233, "eacute");
      HtmlEntity._entityValue.Add("ecirc", 234);
      HtmlEntity._entityName.Add(234, "ecirc");
      HtmlEntity._entityValue.Add("euml", 235);
      HtmlEntity._entityName.Add(235, "euml");
      HtmlEntity._entityValue.Add("igrave", 236);
      HtmlEntity._entityName.Add(236, "igrave");
      HtmlEntity._entityValue.Add("iacute", 237);
      HtmlEntity._entityName.Add(237, "iacute");
      HtmlEntity._entityValue.Add("icirc", 238);
      HtmlEntity._entityName.Add(238, "icirc");
      HtmlEntity._entityValue.Add("iuml", 239);
      HtmlEntity._entityName.Add(239, "iuml");
      HtmlEntity._entityValue.Add("eth", 240);
      HtmlEntity._entityName.Add(240, "eth");
      HtmlEntity._entityValue.Add("ntilde", 241);
      HtmlEntity._entityName.Add(241, "ntilde");
      HtmlEntity._entityValue.Add("ograve", 242);
      HtmlEntity._entityName.Add(242, "ograve");
      HtmlEntity._entityValue.Add("oacute", 243);
      HtmlEntity._entityName.Add(243, "oacute");
      HtmlEntity._entityValue.Add("ocirc", 244);
      HtmlEntity._entityName.Add(244, "ocirc");
      HtmlEntity._entityValue.Add("otilde", 245);
      HtmlEntity._entityName.Add(245, "otilde");
      HtmlEntity._entityValue.Add("ouml", 246);
      HtmlEntity._entityName.Add(246, "ouml");
      HtmlEntity._entityValue.Add("divide", 247);
      HtmlEntity._entityName.Add(247, "divide");
      HtmlEntity._entityValue.Add("oslash", 248);
      HtmlEntity._entityName.Add(248, "oslash");
      HtmlEntity._entityValue.Add("ugrave", 249);
      HtmlEntity._entityName.Add(249, "ugrave");
      HtmlEntity._entityValue.Add("uacute", 250);
      HtmlEntity._entityName.Add(250, "uacute");
      HtmlEntity._entityValue.Add("ucirc", 251);
      HtmlEntity._entityName.Add(251, "ucirc");
      HtmlEntity._entityValue.Add("uuml", 252);
      HtmlEntity._entityName.Add(252, "uuml");
      HtmlEntity._entityValue.Add("yacute", 253);
      HtmlEntity._entityName.Add(253, "yacute");
      HtmlEntity._entityValue.Add("thorn", 254);
      HtmlEntity._entityName.Add(254, "thorn");
      HtmlEntity._entityValue.Add("yuml", (int) byte.MaxValue);
      HtmlEntity._entityName.Add((int) byte.MaxValue, "yuml");
      HtmlEntity._entityValue.Add("fnof", 402);
      HtmlEntity._entityName.Add(402, "fnof");
      HtmlEntity._entityValue.Add("Alpha", 913);
      HtmlEntity._entityName.Add(913, "Alpha");
      HtmlEntity._entityValue.Add("Beta", 914);
      HtmlEntity._entityName.Add(914, "Beta");
      HtmlEntity._entityValue.Add("Gamma", 915);
      HtmlEntity._entityName.Add(915, "Gamma");
      HtmlEntity._entityValue.Add("Delta", 916);
      HtmlEntity._entityName.Add(916, "Delta");
      HtmlEntity._entityValue.Add("Epsilon", 917);
      HtmlEntity._entityName.Add(917, "Epsilon");
      HtmlEntity._entityValue.Add("Zeta", 918);
      HtmlEntity._entityName.Add(918, "Zeta");
      HtmlEntity._entityValue.Add("Eta", 919);
      HtmlEntity._entityName.Add(919, "Eta");
      HtmlEntity._entityValue.Add("Theta", 920);
      HtmlEntity._entityName.Add(920, "Theta");
      HtmlEntity._entityValue.Add("Iota", 921);
      HtmlEntity._entityName.Add(921, "Iota");
      HtmlEntity._entityValue.Add("Kappa", 922);
      HtmlEntity._entityName.Add(922, "Kappa");
      HtmlEntity._entityValue.Add("Lambda", 923);
      HtmlEntity._entityName.Add(923, "Lambda");
      HtmlEntity._entityValue.Add("Mu", 924);
      HtmlEntity._entityName.Add(924, "Mu");
      HtmlEntity._entityValue.Add("Nu", 925);
      HtmlEntity._entityName.Add(925, "Nu");
      HtmlEntity._entityValue.Add("Xi", 926);
      HtmlEntity._entityName.Add(926, "Xi");
      HtmlEntity._entityValue.Add("Omicron", 927);
      HtmlEntity._entityName.Add(927, "Omicron");
      HtmlEntity._entityValue.Add("Pi", 928);
      HtmlEntity._entityName.Add(928, "Pi");
      HtmlEntity._entityValue.Add("Rho", 929);
      HtmlEntity._entityName.Add(929, "Rho");
      HtmlEntity._entityValue.Add("Sigma", 931);
      HtmlEntity._entityName.Add(931, "Sigma");
      HtmlEntity._entityValue.Add("Tau", 932);
      HtmlEntity._entityName.Add(932, "Tau");
      HtmlEntity._entityValue.Add("Upsilon", 933);
      HtmlEntity._entityName.Add(933, "Upsilon");
      HtmlEntity._entityValue.Add("Phi", 934);
      HtmlEntity._entityName.Add(934, "Phi");
      HtmlEntity._entityValue.Add("Chi", 935);
      HtmlEntity._entityName.Add(935, "Chi");
      HtmlEntity._entityValue.Add("Psi", 936);
      HtmlEntity._entityName.Add(936, "Psi");
      HtmlEntity._entityValue.Add("Omega", 937);
      HtmlEntity._entityName.Add(937, "Omega");
      HtmlEntity._entityValue.Add("alpha", 945);
      HtmlEntity._entityName.Add(945, "alpha");
      HtmlEntity._entityValue.Add("beta", 946);
      HtmlEntity._entityName.Add(946, "beta");
      HtmlEntity._entityValue.Add("gamma", 947);
      HtmlEntity._entityName.Add(947, "gamma");
      HtmlEntity._entityValue.Add("delta", 948);
      HtmlEntity._entityName.Add(948, "delta");
      HtmlEntity._entityValue.Add("epsilon", 949);
      HtmlEntity._entityName.Add(949, "epsilon");
      HtmlEntity._entityValue.Add("zeta", 950);
      HtmlEntity._entityName.Add(950, "zeta");
      HtmlEntity._entityValue.Add("eta", 951);
      HtmlEntity._entityName.Add(951, "eta");
      HtmlEntity._entityValue.Add("theta", 952);
      HtmlEntity._entityName.Add(952, "theta");
      HtmlEntity._entityValue.Add("iota", 953);
      HtmlEntity._entityName.Add(953, "iota");
      HtmlEntity._entityValue.Add("kappa", 954);
      HtmlEntity._entityName.Add(954, "kappa");
      HtmlEntity._entityValue.Add("lambda", 955);
      HtmlEntity._entityName.Add(955, "lambda");
      HtmlEntity._entityValue.Add("mu", 956);
      HtmlEntity._entityName.Add(956, "mu");
      HtmlEntity._entityValue.Add("nu", 957);
      HtmlEntity._entityName.Add(957, "nu");
      HtmlEntity._entityValue.Add("xi", 958);
      HtmlEntity._entityName.Add(958, "xi");
      HtmlEntity._entityValue.Add("omicron", 959);
      HtmlEntity._entityName.Add(959, "omicron");
      HtmlEntity._entityValue.Add("pi", 960);
      HtmlEntity._entityName.Add(960, "pi");
      HtmlEntity._entityValue.Add("rho", 961);
      HtmlEntity._entityName.Add(961, "rho");
      HtmlEntity._entityValue.Add("sigmaf", 962);
      HtmlEntity._entityName.Add(962, "sigmaf");
      HtmlEntity._entityValue.Add("sigma", 963);
      HtmlEntity._entityName.Add(963, "sigma");
      HtmlEntity._entityValue.Add("tau", 964);
      HtmlEntity._entityName.Add(964, "tau");
      HtmlEntity._entityValue.Add("upsilon", 965);
      HtmlEntity._entityName.Add(965, "upsilon");
      HtmlEntity._entityValue.Add("phi", 966);
      HtmlEntity._entityName.Add(966, "phi");
      HtmlEntity._entityValue.Add("chi", 967);
      HtmlEntity._entityName.Add(967, "chi");
      HtmlEntity._entityValue.Add("psi", 968);
      HtmlEntity._entityName.Add(968, "psi");
      HtmlEntity._entityValue.Add("omega", 969);
      HtmlEntity._entityName.Add(969, "omega");
      HtmlEntity._entityValue.Add("thetasym", 977);
      HtmlEntity._entityName.Add(977, "thetasym");
      HtmlEntity._entityValue.Add("upsih", 978);
      HtmlEntity._entityName.Add(978, "upsih");
      HtmlEntity._entityValue.Add("piv", 982);
      HtmlEntity._entityName.Add(982, "piv");
      HtmlEntity._entityValue.Add("bull", 8226);
      HtmlEntity._entityName.Add(8226, "bull");
      HtmlEntity._entityValue.Add("hellip", 8230);
      HtmlEntity._entityName.Add(8230, "hellip");
      HtmlEntity._entityValue.Add("prime", 8242);
      HtmlEntity._entityName.Add(8242, "prime");
      HtmlEntity._entityValue.Add("Prime", 8243);
      HtmlEntity._entityName.Add(8243, "Prime");
      HtmlEntity._entityValue.Add("oline", 8254);
      HtmlEntity._entityName.Add(8254, "oline");
      HtmlEntity._entityValue.Add("frasl", 8260);
      HtmlEntity._entityName.Add(8260, "frasl");
      HtmlEntity._entityValue.Add("weierp", 8472);
      HtmlEntity._entityName.Add(8472, "weierp");
      HtmlEntity._entityValue.Add("image", 8465);
      HtmlEntity._entityName.Add(8465, "image");
      HtmlEntity._entityValue.Add("real", 8476);
      HtmlEntity._entityName.Add(8476, "real");
      HtmlEntity._entityValue.Add("trade", 8482);
      HtmlEntity._entityName.Add(8482, "trade");
      HtmlEntity._entityValue.Add("alefsym", 8501);
      HtmlEntity._entityName.Add(8501, "alefsym");
      HtmlEntity._entityValue.Add("larr", 8592);
      HtmlEntity._entityName.Add(8592, "larr");
      HtmlEntity._entityValue.Add("uarr", 8593);
      HtmlEntity._entityName.Add(8593, "uarr");
      HtmlEntity._entityValue.Add("rarr", 8594);
      HtmlEntity._entityName.Add(8594, "rarr");
      HtmlEntity._entityValue.Add("darr", 8595);
      HtmlEntity._entityName.Add(8595, "darr");
      HtmlEntity._entityValue.Add("harr", 8596);
      HtmlEntity._entityName.Add(8596, "harr");
      HtmlEntity._entityValue.Add("crarr", 8629);
      HtmlEntity._entityName.Add(8629, "crarr");
      HtmlEntity._entityValue.Add("lArr", 8656);
      HtmlEntity._entityName.Add(8656, "lArr");
      HtmlEntity._entityValue.Add("uArr", 8657);
      HtmlEntity._entityName.Add(8657, "uArr");
      HtmlEntity._entityValue.Add("rArr", 8658);
      HtmlEntity._entityName.Add(8658, "rArr");
      HtmlEntity._entityValue.Add("dArr", 8659);
      HtmlEntity._entityName.Add(8659, "dArr");
      HtmlEntity._entityValue.Add("hArr", 8660);
      HtmlEntity._entityName.Add(8660, "hArr");
      HtmlEntity._entityValue.Add("forall", 8704);
      HtmlEntity._entityName.Add(8704, "forall");
      HtmlEntity._entityValue.Add("part", 8706);
      HtmlEntity._entityName.Add(8706, "part");
      HtmlEntity._entityValue.Add("exist", 8707);
      HtmlEntity._entityName.Add(8707, "exist");
      HtmlEntity._entityValue.Add("empty", 8709);
      HtmlEntity._entityName.Add(8709, "empty");
      HtmlEntity._entityValue.Add("nabla", 8711);
      HtmlEntity._entityName.Add(8711, "nabla");
      HtmlEntity._entityValue.Add("isin", 8712);
      HtmlEntity._entityName.Add(8712, "isin");
      HtmlEntity._entityValue.Add("notin", 8713);
      HtmlEntity._entityName.Add(8713, "notin");
      HtmlEntity._entityValue.Add("ni", 8715);
      HtmlEntity._entityName.Add(8715, "ni");
      HtmlEntity._entityValue.Add("prod", 8719);
      HtmlEntity._entityName.Add(8719, "prod");
      HtmlEntity._entityValue.Add("sum", 8721);
      HtmlEntity._entityName.Add(8721, "sum");
      HtmlEntity._entityValue.Add("minus", 8722);
      HtmlEntity._entityName.Add(8722, "minus");
      HtmlEntity._entityValue.Add("lowast", 8727);
      HtmlEntity._entityName.Add(8727, "lowast");
      HtmlEntity._entityValue.Add("radic", 8730);
      HtmlEntity._entityName.Add(8730, "radic");
      HtmlEntity._entityValue.Add("prop", 8733);
      HtmlEntity._entityName.Add(8733, "prop");
      HtmlEntity._entityValue.Add("infin", 8734);
      HtmlEntity._entityName.Add(8734, "infin");
      HtmlEntity._entityValue.Add("ang", 8736);
      HtmlEntity._entityName.Add(8736, "ang");
      HtmlEntity._entityValue.Add("and", 8743);
      HtmlEntity._entityName.Add(8743, "and");
      HtmlEntity._entityValue.Add("or", 8744);
      HtmlEntity._entityName.Add(8744, "or");
      HtmlEntity._entityValue.Add("cap", 8745);
      HtmlEntity._entityName.Add(8745, "cap");
      HtmlEntity._entityValue.Add("cup", 8746);
      HtmlEntity._entityName.Add(8746, "cup");
      HtmlEntity._entityValue.Add("int", 8747);
      HtmlEntity._entityName.Add(8747, "int");
      HtmlEntity._entityValue.Add("there4", 8756);
      HtmlEntity._entityName.Add(8756, "there4");
      HtmlEntity._entityValue.Add("sim", 8764);
      HtmlEntity._entityName.Add(8764, "sim");
      HtmlEntity._entityValue.Add("cong", 8773);
      HtmlEntity._entityName.Add(8773, "cong");
      HtmlEntity._entityValue.Add("asymp", 8776);
      HtmlEntity._entityName.Add(8776, "asymp");
      HtmlEntity._entityValue.Add("ne", 8800);
      HtmlEntity._entityName.Add(8800, "ne");
      HtmlEntity._entityValue.Add("equiv", 8801);
      HtmlEntity._entityName.Add(8801, "equiv");
      HtmlEntity._entityValue.Add("le", 8804);
      HtmlEntity._entityName.Add(8804, "le");
      HtmlEntity._entityValue.Add("ge", 8805);
      HtmlEntity._entityName.Add(8805, "ge");
      HtmlEntity._entityValue.Add("sub", 8834);
      HtmlEntity._entityName.Add(8834, "sub");
      HtmlEntity._entityValue.Add("sup", 8835);
      HtmlEntity._entityName.Add(8835, "sup");
      HtmlEntity._entityValue.Add("nsub", 8836);
      HtmlEntity._entityName.Add(8836, "nsub");
      HtmlEntity._entityValue.Add("sube", 8838);
      HtmlEntity._entityName.Add(8838, "sube");
      HtmlEntity._entityValue.Add("supe", 8839);
      HtmlEntity._entityName.Add(8839, "supe");
      HtmlEntity._entityValue.Add("oplus", 8853);
      HtmlEntity._entityName.Add(8853, "oplus");
      HtmlEntity._entityValue.Add("otimes", 8855);
      HtmlEntity._entityName.Add(8855, "otimes");
      HtmlEntity._entityValue.Add("perp", 8869);
      HtmlEntity._entityName.Add(8869, "perp");
      HtmlEntity._entityValue.Add("sdot", 8901);
      HtmlEntity._entityName.Add(8901, "sdot");
      HtmlEntity._entityValue.Add("lceil", 8968);
      HtmlEntity._entityName.Add(8968, "lceil");
      HtmlEntity._entityValue.Add("rceil", 8969);
      HtmlEntity._entityName.Add(8969, "rceil");
      HtmlEntity._entityValue.Add("lfloor", 8970);
      HtmlEntity._entityName.Add(8970, "lfloor");
      HtmlEntity._entityValue.Add("rfloor", 8971);
      HtmlEntity._entityName.Add(8971, "rfloor");
      HtmlEntity._entityValue.Add("lang", 9001);
      HtmlEntity._entityName.Add(9001, "lang");
      HtmlEntity._entityValue.Add("rang", 9002);
      HtmlEntity._entityName.Add(9002, "rang");
      HtmlEntity._entityValue.Add("loz", 9674);
      HtmlEntity._entityName.Add(9674, "loz");
      HtmlEntity._entityValue.Add("spades", 9824);
      HtmlEntity._entityName.Add(9824, "spades");
      HtmlEntity._entityValue.Add("clubs", 9827);
      HtmlEntity._entityName.Add(9827, "clubs");
      HtmlEntity._entityValue.Add("hearts", 9829);
      HtmlEntity._entityName.Add(9829, "hearts");
      HtmlEntity._entityValue.Add("diams", 9830);
      HtmlEntity._entityName.Add(9830, "diams");
      HtmlEntity._entityValue.Add("quot", 34);
      HtmlEntity._entityName.Add(34, "quot");
      HtmlEntity._entityValue.Add("amp", 38);
      HtmlEntity._entityName.Add(38, "amp");
      HtmlEntity._entityValue.Add("lt", 60);
      HtmlEntity._entityName.Add(60, "lt");
      HtmlEntity._entityValue.Add("gt", 62);
      HtmlEntity._entityName.Add(62, "gt");
      HtmlEntity._entityValue.Add("OElig", 338);
      HtmlEntity._entityName.Add(338, "OElig");
      HtmlEntity._entityValue.Add("oelig", 339);
      HtmlEntity._entityName.Add(339, "oelig");
      HtmlEntity._entityValue.Add("Scaron", 352);
      HtmlEntity._entityName.Add(352, "Scaron");
      HtmlEntity._entityValue.Add("scaron", 353);
      HtmlEntity._entityName.Add(353, "scaron");
      HtmlEntity._entityValue.Add("Yuml", 376);
      HtmlEntity._entityName.Add(376, "Yuml");
      HtmlEntity._entityValue.Add("circ", 710);
      HtmlEntity._entityName.Add(710, "circ");
      HtmlEntity._entityValue.Add("tilde", 732);
      HtmlEntity._entityName.Add(732, "tilde");
      HtmlEntity._entityValue.Add("ensp", 8194);
      HtmlEntity._entityName.Add(8194, "ensp");
      HtmlEntity._entityValue.Add("emsp", 8195);
      HtmlEntity._entityName.Add(8195, "emsp");
      HtmlEntity._entityValue.Add("thinsp", 8201);
      HtmlEntity._entityName.Add(8201, "thinsp");
      HtmlEntity._entityValue.Add("zwnj", 8204);
      HtmlEntity._entityName.Add(8204, "zwnj");
      HtmlEntity._entityValue.Add("zwj", 8205);
      HtmlEntity._entityName.Add(8205, "zwj");
      HtmlEntity._entityValue.Add("lrm", 8206);
      HtmlEntity._entityName.Add(8206, "lrm");
      HtmlEntity._entityValue.Add("rlm", 8207);
      HtmlEntity._entityName.Add(8207, "rlm");
      HtmlEntity._entityValue.Add("ndash", 8211);
      HtmlEntity._entityName.Add(8211, "ndash");
      HtmlEntity._entityValue.Add("mdash", 8212);
      HtmlEntity._entityName.Add(8212, "mdash");
      HtmlEntity._entityValue.Add("lsquo", 8216);
      HtmlEntity._entityName.Add(8216, "lsquo");
      HtmlEntity._entityValue.Add("rsquo", 8217);
      HtmlEntity._entityName.Add(8217, "rsquo");
      HtmlEntity._entityValue.Add("sbquo", 8218);
      HtmlEntity._entityName.Add(8218, "sbquo");
      HtmlEntity._entityValue.Add("ldquo", 8220);
      HtmlEntity._entityName.Add(8220, "ldquo");
      HtmlEntity._entityValue.Add("rdquo", 8221);
      HtmlEntity._entityName.Add(8221, "rdquo");
      HtmlEntity._entityValue.Add("bdquo", 8222);
      HtmlEntity._entityName.Add(8222, "bdquo");
      HtmlEntity._entityValue.Add("dagger", 8224);
      HtmlEntity._entityName.Add(8224, "dagger");
      HtmlEntity._entityValue.Add("Dagger", 8225);
      HtmlEntity._entityName.Add(8225, "Dagger");
      HtmlEntity._entityValue.Add("permil", 8240);
      HtmlEntity._entityName.Add(8240, "permil");
      HtmlEntity._entityValue.Add("lsaquo", 8249);
      HtmlEntity._entityName.Add(8249, "lsaquo");
      HtmlEntity._entityValue.Add("rsaquo", 8250);
      HtmlEntity._entityName.Add(8250, "rsaquo");
      HtmlEntity._entityValue.Add("euro", 8364);
      HtmlEntity._entityName.Add(8364, "euro");
      HtmlEntity._maxEntitySize = 9;
    }

    private HtmlEntity()
    {
    }

    public static string DeEntitize(string text)
    {
      if (text == null)
        return (string) null;
      if (text.Length == 0)
        return text;
      StringBuilder stringBuilder1 = new StringBuilder(text.Length);
      HtmlEntity.ParseState parseState = HtmlEntity.ParseState.Text;
      StringBuilder stringBuilder2 = new StringBuilder(10);
      for (int index = 0; index < text.Length; ++index)
      {
        switch (parseState)
        {
          case HtmlEntity.ParseState.Text:
            if (text[index] == '&')
            {
              parseState = HtmlEntity.ParseState.EntityStart;
              break;
            }
            stringBuilder1.Append(text[index]);
            break;
          case HtmlEntity.ParseState.EntityStart:
            switch (text[index])
            {
              case '&':
                stringBuilder1.Append("&" + (object) stringBuilder2);
                stringBuilder2.Remove(0, stringBuilder2.Length);
                continue;
              case ';':
                if (stringBuilder2.Length == 0)
                {
                  stringBuilder1.Append("&;");
                }
                else
                {
                  if (stringBuilder2[0] == '#')
                  {
                    string str1 = stringBuilder2.ToString();
                    try
                    {
                      string str2 = str1.Substring(1).Trim().ToLower();
                      int fromBase;
                      if (str2.StartsWith("x"))
                      {
                        fromBase = 16;
                        str2 = str2.Substring(1);
                      }
                      else
                        fromBase = 10;
                      int int32 = Convert.ToInt32(str2, fromBase);
                      stringBuilder1.Append(Convert.ToChar(int32));
                    }
                    catch
                    {
                      stringBuilder1.Append("&#" + str1 + ";");
                    }
                  }
                  else
                  {
                    object obj = (object) HtmlEntity._entityValue[stringBuilder2.ToString()];
                    if (obj == null)
                    {
                      stringBuilder1.Append("&" + (object) stringBuilder2 + ";");
                    }
                    else
                    {
                      int num = (int) obj;
                      stringBuilder1.Append(Convert.ToChar(num));
                    }
                  }
                  stringBuilder2.Remove(0, stringBuilder2.Length);
                }
                parseState = HtmlEntity.ParseState.Text;
                continue;
              default:
                stringBuilder2.Append(text[index]);
                if (stringBuilder2.Length > HtmlEntity._maxEntitySize)
                {
                  parseState = HtmlEntity.ParseState.Text;
                  stringBuilder1.Append("&" + (object) stringBuilder2);
                  stringBuilder2.Remove(0, stringBuilder2.Length);
                  continue;
                }
                continue;
            }
        }
      }
      if (parseState == HtmlEntity.ParseState.EntityStart)
        stringBuilder1.Append("&" + (object) stringBuilder2);
      return stringBuilder1.ToString();
    }

    public static HtmlNode Entitize(HtmlNode node)
    {
      if (node == null)
        throw new ArgumentNullException(nameof (node));
      HtmlNode htmlNode = node.CloneNode(true);
      if (htmlNode.HasAttributes)
        HtmlEntity.Entitize(htmlNode.Attributes);
      if (htmlNode.HasChildNodes)
        HtmlEntity.Entitize(htmlNode.ChildNodes);
      else if (htmlNode.NodeType == HtmlNodeType.Text)
        ((HtmlTextNode) htmlNode).Text = HtmlEntity.Entitize(((HtmlTextNode) htmlNode).Text, true, true);
      return htmlNode;
    }

    public static string Entitize(string text)
    {
      return HtmlEntity.Entitize(text, true);
    }

    public static string Entitize(string text, bool useNames)
    {
      return HtmlEntity.Entitize(text, useNames, false);
    }

    public static string Entitize(string text, bool useNames, bool entitizeQuotAmpAndLtGt)
    {
      if (text == null)
        return (string) null;
      if (text.Length == 0)
        return text;
      StringBuilder stringBuilder = new StringBuilder(text.Length);
      for (int index1 = 0; index1 < text.Length; ++index1)
      {
        int index2 = (int) text[index1];
        if (index2 > (int) sbyte.MaxValue || entitizeQuotAmpAndLtGt && (index2 == 34 || index2 == 38 || (index2 == 60 || index2 == 62)))
        {
          string str = HtmlEntity._entityName[index2];
          if (str == null || !useNames)
            stringBuilder.Append("&#" + (object) index2 + ";");
          else
            stringBuilder.Append("&" + str + ";");
        }
        else
          stringBuilder.Append(text[index1]);
      }
      return stringBuilder.ToString();
    }

    private static void Entitize(HtmlAttributeCollection collection)
    {
      foreach (HtmlAttribute htmlAttribute in (IEnumerable<HtmlAttribute>) collection)
        htmlAttribute.Value = HtmlEntity.Entitize(htmlAttribute.Value);
    }

    private static void Entitize(HtmlNodeCollection collection)
    {
      foreach (HtmlNode htmlNode in (IEnumerable<HtmlNode>) collection)
      {
        if (htmlNode.HasAttributes)
          HtmlEntity.Entitize(htmlNode.Attributes);
        if (htmlNode.HasChildNodes)
          HtmlEntity.Entitize(htmlNode.ChildNodes);
        else if (htmlNode.NodeType == HtmlNodeType.Text)
          ((HtmlTextNode) htmlNode).Text = HtmlEntity.Entitize(((HtmlTextNode) htmlNode).Text, true, true);
      }
    }

    private enum ParseState
    {
      Text,
      EntityStart,
    }
  }
}
