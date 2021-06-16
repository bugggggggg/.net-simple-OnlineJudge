using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOperation
{
    public class File
    {
        public static string Read(string filePath)
        {
            string ret = "";
            try
            {
                // 创建一个 StreamReader 的实例来读取文件 
                // using 语句也能关闭 StreamReader
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    bool fi = true;
                    // 从文件读取并显示行，直到文件的末尾 
                    while ((line = sr.ReadLine()) != null)
                    {
                        if(!fi)
                        {
                            ret += "\n";
                            fi = false;
                        }
                        ret += line;
                        
                    }
                    return ret;
                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return "";
        }

        public static void Write(string filePath,string content)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine(content);

                
            }

        }
        //static void Main()
        //{
        //    string s = Read(@"D:\Judgefile\a.txt");
        //    Console.WriteLine(s);
        //    Write(@"D:\Judgefile\b.txt",s);

        //    System.Console.WriteLine("Hello World!");
        //}
    }
}
