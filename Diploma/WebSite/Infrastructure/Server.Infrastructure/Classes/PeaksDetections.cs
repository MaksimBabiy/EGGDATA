using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

using qrs;



namespace Server.Infrastructure.Classes
{
    public static class PeaksDetection
    {
        private static RPeaksDetection detector = new RPeaksDetection();


        public static void GetRPeaks(string path)
        {
            detector.qrs(path);
        }

        public static List<string> RPeaksToList(string path)
        {
            string result = "";

            using (StreamReader stream = new StreamReader(path))
            {
                result = stream.ReadToEnd();
                result.Trim();
            }
            return result.Split(' ').ToList<string>();

        }

       
    }
}
