using System;
using System.Collections.Generic;
using System.IO;

namespace Server.Infrastructure.Classes
{
    public static class ReaderUpdate
    {
        static List<string> Points { get; set; }
        public static List<string> GetData2(FileStream stream)
        {
            int hexIn;
            String hex;
            int row = 0;

            for (int i = 0; (hexIn = stream.ReadByte()) != -1; i++)
            {
                hex = string.Format("{0:X2}", hexIn);
                Points.Add(hex);
                row++;
            }

            return Points;
        }
    }
}
