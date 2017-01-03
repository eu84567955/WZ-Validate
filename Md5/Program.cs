using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Reflection;

namespace Md5
{
    class Program
    {
        static void Main(string[] args)
        {
            const int revision = 0;
            
            Console.Title= "WZ演算程式 by 東南亞人";
            Console.WriteLine("WZ演算程式 by 東南亞人，小馬專屬論壇和技術論壇 www.ponytw.com 。\r\nRevision: {0}", revision);
            // FileInfo fileInfo = new FileInfo(@"MapleStory V83");
            Console.WriteLine("\r\n\r\n\r\n請按任意鍵開始 . . .");
            Console.ReadKey();
            Console.Clear();
            if (File.Exists(@"HashCode.txt"))
            {
                DirectoryInfo dir = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

                StreamWriter sw = new StreamWriter(@"HashCode.txt");
                var md5 = MD5.Create();
                string getMD5;


                foreach (FileInfo file in dir.GetFiles())
                {
                    string path = file.Name;

                    if (path.Substring(path.Length - 3) == ".wz" && path != "List.wz" && path != "Base.wz")
                    {
                        Console.WriteLine("正在演算 " + path + " . . . ");
                        var stream = File.OpenRead(path);
                        getMD5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                        string msg = path + " = ";

                        sw.WriteLine(msg + getMD5 + "\r\n");

                    }

                }
                sw.Flush();
                sw.Close();
                //Console.Clear();
                Console.WriteLine("\r\n演算完畢！請檢閱您的 HashCode.txt , 祝你有個美好的一天!");
                Console.WriteLine("\r\n請按任意鍵繼續 . . .");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\r\n請確保您的文件夾有 HashCode.txt .");
                Console.WriteLine("\r\n請按任意鍵繼續 . . .");
                Console.ReadKey();
            }
        }

    }
}
