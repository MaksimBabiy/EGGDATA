namespace Server.Infrastructure.Classes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    //подгружаем либы которые  позволят использовать ранее скомпилированную матлабовскую функцию
    using MathWorks.MATLAB.NET.Arrays;
    using MathWorks.MATLAB.NET.Utility;
    using qrs;

    public static class PeaksDetection
    {
        //создаем обьект функции матлаба
        private static RPeaksDetection detector = new RPeaksDetection();


        public static void GetRPeaks(string path)
        {
            //вызываем алгоритм обнаружения пиков, пики заносятся в файл RPeaks.txt
            detector.qrs(path);
        }

        //читает значения пиков из файла RPeaks.txt и заносит в список
        public static List<string> RPeaksToList(string path)
        {
            string result = "";

            using (StreamReader stream = new StreamReader(path))
            {
                result = stream.ReadToEnd();
            }

            return result.Split(' ').ToList<string>();
        }
    }
}
