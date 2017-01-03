using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Xml;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Reflection;
using System.Threading;

namespace MapleLauncher_Console
{
    class Program
    {
        
        static Hashtable hash;
        static string filepath;
        static Hashtable currentHash = new Hashtable();
       static string text = "[Main]\r\nEnabled=1\r\nWindowname=BFMS v117.2\r\n\r\n[Server]\r\nIP=bfms117.ddns.net\r\n\r\n[Hacks]\r\nSwapUsernamePassword=0";

        static string text2 = "[Main]\r\nEnabled=1\r\nWindowname=BFMS v117.2\r\n\r\n[Server]\r\nIP=+t6Q/A+pgL+7uF8xrdJq0nNQCTBEiDoBFoi8K2ukdGU=\r\n\r\n[Hacks]\r\nSwapUsernamePassword=0";
        
       

        static void Main(string[] args)
        {
            Console.Title = "百花撩亂  v117.2 遊戲開啟器";

            //The validate.xml path

            filepath = "http://bfms117.ddns.net/Validate.xml";

            //filepath = "Validate.xml";
           
            //Write text2 to Len.ini
            StreamWriter sw2 = new StreamWriter("LEN.ini");
            sw2.Write(text2);

            sw2.Flush();
            sw2.Close();

            //Get current directory, if Launcher.exe is exists, delete it

            string currentDirectory = Directory.GetCurrentDirectory();
            if(File.Exists(currentDirectory + "/Launcher.exe"))
            {
                File.Delete(currentDirectory + "/Launcher.exe");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("歡迎來到【百花撩亂】v117.2 私服\r\n\r\n\r\n");

            //Start the function
            StartValidate();

            //Console.ReadKey();

        }

        static void StartValidate()
        {
            bool validate = false;
            bool fileComplete = false;
            Console.WriteLine("/* \t\t\t請勿把程式關掉，否則將無法進入遊戲。\t\t */\r\n\r\n");
            Console.WriteLine("正在連接遊戲 . . .\r\n\r\n請耐心稍等 . . .");

            
            MyXmlReader myXml = new MyXmlReader();

            //Set myXml path
            myXml.Path = filepath;

            //Get hashtable from myXml class
            hash = myXml.getHashFromXml();
            DirectoryInfo dir = new DirectoryInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

            //ArrayList list = new ArrayList();

            var md5 = MD5.Create();
            string getMD5;

            //Using foreach run through the directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string path = file.Name;

                if (path.Substring(path.Length - 3) == ".wz" && (path == "Character.wz" || (path == "Skill.wz" || path == "skill.wz")))
                {
                    var stream = File.OpenRead(path);
                    getMD5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                    currentHash.Add(path, getMD5);
                    //list.Add(path);
                }
            }
            ArrayList currHash = new ArrayList();
            ArrayList serHash = new ArrayList();
            //ArrayList currHashName = new ArrayList();

            foreach (string wzName in currentHash.Keys)
            {

                currHash.Add(currentHash[wzName].ToString());
                //currHashName.Add(wzName);
            }

            foreach (string wzServerName in hash.Keys)
            {

                serHash.Add(hash[wzServerName].ToString());

            }
            //Console.WriteLine("主程式驗證的WZ數量為: " + currHash.Count);
            //Console.WriteLine("服務器驗證的WZ數量為: " + serHash.Count);
            //Console.ReadKey();

            if (currHash.Count == serHash.Count)
            {
                int Count = 0;
                for (int i = 0; i < currHash.Count; i++)
                {


                    for (int j = 0; j < serHash.Count; j++)
                    {

                        if (currHash[i].ToString() != serHash[j].ToString())
                            continue;

                        //Console.WriteLine("遊戲檔案 " + currHashName[i].ToString() + " 驗證完畢. . .");
                        //Console.ReadKey();
                        Count++;
                        validate = true;
                        break;
                    }

                }
                if (Count == 2)
                    fileComplete = true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("驗證失敗！請馬上聯繫管理員。");
                Thread.Sleep(2000);
            }


            if (validate == true && fileComplete == true)
            {

                //Console.WriteLine("驗證完畢，準備開啟遊戲 . . .");
                Console.Clear();
                string currentDirectory = Directory.GetCurrentDirectory();
                if (File.Exists(currentDirectory + "/Maplestory.exe"))
                {
                    StreamWriter sw1 = new StreamWriter("LEN.ini");





                    Console.WriteLine("開啟遊戲中 . . .\r\n");
                    Process process = new Process();
                    string textpath = System.IO.Path.Combine(currentDirectory, "Patchers.exe");

                    File.WriteAllBytes(textpath, MapleLauncher_Console.Properties.Resources.Patchers);

                    sw1.Write(text);

                    sw1.Flush();
                    sw1.Close();
                    for (int i = 3; i > 0; i--)
                    {
                        Console.WriteLine(i + "\r\n");
                        Thread.Sleep(500);
                    }
                    Console.WriteLine("遊戲將自動開啟，請稍等 . . . ");
                    process.StartInfo.FileName = textpath;
                    process.Start();
                    Thread.Sleep(5000);
                    StreamWriter sw3 = new StreamWriter("LEN.ini");

                    sw3.Write(text2);

                    sw3.Flush();
                    sw3.Close();

                    File.Delete(currentDirectory + "/Patchers.exe");

                    
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("主程式貌似有問題，請聯繫遊戲管理 . . . ");
                    Thread.Sleep(2000);
                }

            }
            else if (validate == true && fileComplete == false)
            {
                Console.Clear();
                Console.WriteLine("偵測到檔案有被篡改的現象！程式將自動關閉 . . .");
                //Console.ReadKey();
                Thread.Sleep(2000);
            }
        }
    
        
    }
}
