using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Server.Infrastructure.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Server.Infrastructure.Logic
{
    public class GetData
    {
        ////папка хранения файлов
        //string folderName = "Upload";
        //string webRootPath = hostingEnvironment.WebRootPath;
        ////путь к этой папке
        //string path = Path.Combine(webRootPath, folderName);
        ////путь файла
        //path = Path.Combine(path, id + ".dat");
        
        public static List<string> ReadFile(int id, IHostingEnvironment env)
        {
            string folderName = "Upload";
            string webRootPath = env.WebRootPath;
            string path = Path.Combine(webRootPath, folderName);
            //путь файла
            path = Path.Combine(path, id + ".dat");

            List<string> list = new List<string>();

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                try
                {
                    //читаем все точки из файла и заносим в список
                    list = Reader.GetData(stream);
                }
                catch (Exception ex)
                {
                    throw new Exception("Cannot read data from file");
                }
            }
            return list;
        }

        public static List<string> GetRPeaks(int id, IHostingEnvironment env)
        {
            string folderName = "Upload";
            string webRootPath = env.WebRootPath;
            string path = Path.Combine(webRootPath, folderName);
            path = Path.Combine(path, id + ".dat");
            string path2 = Path.Combine(env.ContentRootPath, "Rpeaks.txt");

            List<string> peaks = new List<string>();
            using (StreamReader stream = new StreamReader(path))
            {
                try
                {
                    //Получаем пики и заносим их в список
                    PeaksDetection.GetRPeaks(path);
                    peaks = PeaksDetection.RPeaksToList(path2);
                }
                catch (Exception ex)
                {
                    throw new Exception("Cannot get RPeaks");
                }
            }

            return peaks;
        }
    }
}
