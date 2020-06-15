namespace Server.Infrastructure.Classes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    //���������� ���� �������  �������� ������������ ����� ���������������� ������������ �������
    using MathWorks.MATLAB.NET.Arrays;
    using MathWorks.MATLAB.NET.Utility;
    using qrs;

    public static class PeaksDetection
    {
        //������� ������ ������� �������
        private static RPeaksDetection detector = new RPeaksDetection();


        public static void GetRPeaks(string path)
        {
            //�������� �������� ����������� �����, ���� ��������� � ���� RPeaks.txt
            detector.qrs(path);
        }

        //������ �������� ����� �� ����� RPeaks.txt � ������� � ������
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
