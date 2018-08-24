// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.HtmlCmdLine
// Assembly: HtmlAgilityPack, Version=1.4.6.0, Culture=neutral, PublicKeyToken=bd319b19eaf3b43a
// MVID: 83185D3B-3939-439C-A54F-260F9279D9C8
// Assembly location: D:\Program Files (x86)\Huion Tablet\Release\HtmlAgilityPack.dll

using System;

namespace HtmlAgilityPack
{
    internal class HtmlCmdLine
    {
        internal static bool Help = false;

        static HtmlCmdLine()
        {
            ParseArgs();
        }

        internal static string GetOption(string name, string def)
        {
            string ArgValue = def;
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            for (int index = 1; index < commandLineArgs.Length; ++index)
                GetStringArg(commandLineArgs[index], name, ref ArgValue);
            return ArgValue;
        }

        internal static string GetOption(int index, string def)
        {
            string ArgValue = def;
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            int num = 0;
            for (int index1 = 1; index1 < commandLineArgs.Length; ++index1)
            {
                if (GetStringArg(commandLineArgs[index1], ref ArgValue))
                {
                    if (index == num)
                        return ArgValue;
                    ArgValue = def;
                    ++num;
                }
            }

            return ArgValue;
        }

        internal static bool GetOption(string name, bool def)
        {
            bool ArgValue = def;
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            for (int index = 1; index < commandLineArgs.Length; ++index)
                GetBoolArg(commandLineArgs[index], name, ref ArgValue);
            return ArgValue;
        }

        internal static int GetOption(string name, int def)
        {
            int ArgValue = def;
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            for (int index = 1; index < commandLineArgs.Length; ++index)
                GetIntArg(commandLineArgs[index], name, ref ArgValue);
            return ArgValue;
        }

        private static void GetBoolArg(string Arg, string Name, ref bool ArgValue)
        {
            if (Arg.Length < Name.Length + 1 || '/' != Arg[0] && '-' != Arg[0] ||
                !(Arg.Substring(1, Name.Length).ToLower() == Name.ToLower()))
                return;
            ArgValue = true;
        }

        private static void GetIntArg(string Arg, string Name, ref int ArgValue)
        {
            if (Arg.Length < Name.Length + 3 || '/' != Arg[0] && '-' != Arg[0])
                return;
            if (!(Arg.Substring(1, Name.Length).ToLower() == Name.ToLower()))
                return;
            try
            {
                ArgValue = Convert.ToInt32(Arg.Substring(Name.Length + 2, Arg.Length - Name.Length - 2));
            }
            catch
            {
            }
        }

        private static bool GetStringArg(string Arg, ref string ArgValue)
        {
            if ('/' == Arg[0] || '-' == Arg[0])
                return false;
            ArgValue = Arg;
            return true;
        }

        private static void GetStringArg(string Arg, string Name, ref string ArgValue)
        {
            if (Arg.Length < Name.Length + 3 || '/' != Arg[0] && '-' != Arg[0] ||
                !(Arg.Substring(1, Name.Length).ToLower() == Name.ToLower()))
                return;
            ArgValue = Arg.Substring(Name.Length + 2, Arg.Length - Name.Length - 2);
        }

        private static void ParseArgs()
        {
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            for (int index = 1; index < commandLineArgs.Length; ++index)
            {
                GetBoolArg(commandLineArgs[index], "?", ref Help);
                GetBoolArg(commandLineArgs[index], "h", ref Help);
                GetBoolArg(commandLineArgs[index], "help", ref Help);
            }
        }
    }
}