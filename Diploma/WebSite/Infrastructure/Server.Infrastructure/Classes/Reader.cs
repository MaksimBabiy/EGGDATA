namespace Server.Infrastructure.Classes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public static class Reader
    {
        static List<string> Points { get; set; } = new List<string>();

        public static List<string> GetData(FileStream stream)
        {
            using (stream)
            {
                int hexIn;
                string hex = "";

                for (int i = 0; (hexIn = stream.ReadByte()) != -1; i++)
                {
                    hex += string.Format("{0:X2}", hexIn);
                }

                foreach (var item in SplitString(hex))
                {
                    string point = "";
                    point = item.Substring(2, 2) + item.Substring(0, 2);
                    Points.Add(Convert.ToInt32(point,16).ToString());
                }

                return Points;
            }
        }

        public static List<string> SplitString(string str)
        {
            List<string> list = new List<string>();
            int i = 0;
            while (i < str.Length - 1)
            {
                try
                {
                    list.Add(str.Substring(i, 4));
                    i += 4;
                }
                catch { break; }
            }
            return list;
        }



    }
}
