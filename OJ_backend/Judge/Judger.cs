using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Judge
{
    public sealed class CommandResult
    {
        public CommandResult(int exitCode, string standardOut, string standardError, int usedMemory = 0, int usedTime = 0)
        {
            ExitCode = exitCode;
            StandardOut = standardOut;
            StandardError = standardError;
            UsedMemory = usedMemory;
            UsedTime = usedTime;
        }

        public string StandardOut { get; }
        public string StandardError { get; }
        public int ExitCode { get; }

        public int UsedMemory { get;}

        public int UsedTime { get; }
    }

    public class JudgeResult
    {
        public JudgeResult(string msg,int usedTime,int usedMemory,int exitCode)
        {
            Msg = msg;
            UsedTime = usedTime;
            UsedMemory = usedMemory;
            ExitCode = exitCode;
        }

        public string Msg { get; }
        public int UsedTime { get; }
        public int UsedMemory { get; }

        //0表示正常
        public int ExitCode { get; }
    }


    public class Judger
    {
        public static string WorkSpace = @"D:\Judgefile";
        public enum Language { CPLUSPLUS};


        [DllImport("../x64/Debug/COMPARATOR.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool Compare([MarshalAs(UnmanagedType.LPStr)]string filePath1, [MarshalAs(UnmanagedType.LPStr)]string filePath2);

        

        static public int Add(int a,int b)
        {
            return a + b;
        }
        public static int Execute(string commandPath, string arguments = null, string workingDirectory = null)
        {
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo(commandPath, arguments ?? string.Empty)
                {
                    //RedirectStandardInput=true,
                   // RedirectStandardOutput=true,
                    UseShellExecute = false,
                    CreateNoWindow = true,

                    WorkingDirectory = workingDirectory ?? Environment.CurrentDirectory
                }
            };
            //process.StandardInput.BaseStream.

            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }

        public static CommandResult ExecuteAndCapture
            (string commandPath, string arguments = null,string workingDirectory = null, string inputString=null,int timeLimit=2000)
        {
            //Console.WriteLine(Environment.CurrentDirectory);
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo(commandPath, arguments ?? string.Empty)
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,

                    
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,

                    WorkingDirectory = workingDirectory ?? Environment.CurrentDirectory
                }
            };
            if (inputString != null) process.StartInfo.RedirectStandardInput = true;
            //Console.WriteLine(fileName);
            // Thread thread = new Thread(new ParameterizedThreadStart(ExeThread));
            // thread.Start(process);

            Dictionary<string, Object> dict = new Dictionary<string, object>();
            dict["time"] = timeLimit.ToString();
            dict["process"] = process;
            Thread thread = new Thread(new ParameterizedThreadStart(TimerThread));
            thread.Start(dict);

            process.Start();
            
            if(process.HasExited)
            {
                return new CommandResult(-1, "", "", 0, 0);
            }

            if (inputString != null) process.StandardInput.WriteLine(inputString);

            int time = (int)process.TotalProcessorTime.TotalMilliseconds;
            int memory = (int)process.PrivateMemorySize64 / 1024;

            var standardOut = process.StandardOutput.ReadToEnd();
            var standardError = process.StandardError.ReadToEnd();
           // Console.WriteLine(process.Id);
            
            //Console.WriteLine(process.Id);
            int ExitCode = process.ExitCode;
            
            
            return new CommandResult(process.ExitCode, standardOut, standardError,memory,time);
        }


        //计时进程
        public static void TimerThread(Object s)
        {
            Dictionary<string, Object> dict = s as Dictionary<string, Object>;
            int time = int.Parse(dict["time"] as string);
            Process process = dict["process"] as Process;
            Thread.Sleep(time);
            if (!process.HasExited)
            {
                try
                {
                    process.Kill();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static CommandResult Compile(int fileId,Language language)
        {
            if(language==Language.CPLUSPLUS)
            {
                string args = " -O2 -s -Wall -std=c++11 -o " + fileId.ToString() + ".exe " + fileId.ToString() + ".cpp -lm";
                CommandResult result = ExecuteAndCapture("g++", args, WorkSpace);
                return result;
            }
            return null;
        }


        public static JudgeResult Excute(int codeFileId,int problemId,Language language,int testCnt,int limitTime,int limitMemory)
        {
            if(language==Language.CPLUSPLUS)
            {
                CommandResult compileResult = Compile(codeFileId, language);
                if(compileResult.ExitCode!=0)
                {
                    return new JudgeResult("Compile Error", 0, 0, -1);
                }
                
                int mxtime = 0, mxmemory = 0;
                for(int i=1;i<=testCnt;i++)
                {
                    string input = FileOperation.File.Read(WorkSpace + @"\" + problemId.ToString() + @"\" + i.ToString() + @".in");
                    CommandResult result=ExecuteAndCapture(WorkSpace+@"\"+codeFileId.ToString() + ".exe", null, null, input,2*limitTime);
                    if(result.ExitCode!=0)
                    {
                        return new JudgeResult("Runtime Error", 0, 0, -1);
                    }
                    mxtime = Math.Max(mxtime, result.UsedTime);
                    mxmemory = Math.Max(mxmemory, result.UsedMemory);
                    FileOperation.File.Write(WorkSpace + @"\" + problemId.ToString() + @"\" + i.ToString() + @"program.out", result.StandardOut);
                    Console.WriteLine(result.StandardOut);
                    //return new JudgeResult("Accept", mxtime, mxmemory, 0);
                    if (!Compare(WorkSpace + @"\" + problemId.ToString() + @"\" + i.ToString() + @".out"
                        , WorkSpace + @"\" + problemId.ToString() + @"\" + i.ToString() + @"program.out"))
                    {
                        return new JudgeResult("Weong Answer", 0, 0, -1);
                    }
                }
                if(mxtime>limitTime) return new JudgeResult("Time Limit Exceed", 0, 0, -1);
                if (mxmemory > limitMemory) return new JudgeResult("Memory Limit Exceed", 0, 0, -1);
                return new JudgeResult("Accept", mxtime, mxmemory, 0);
            }
            return null;
        }



        //static void Main()
        //{
        //    //string output;
        //    //RunCmd(".\\../Judgefile/1.exe", out output);
        //    //Console.WriteLine(output);

        //    // CommandResult result = ExecuteAndCapture(@"..\..\..\Judgefile\1.exe");
        //    //CommandResult result = ExecuteAndCapture("g++", " -O2 -s -Wall -std=c++11 -o 1.exe 1.cpp -lm");
        //    //CommandResult result = ExecuteAndCapture("g++", @" -O2 -s -Wall -std=c++11 -o ..\..\..\Judgefile\add.exe ..\..\..\Judgefile\add.cpp -lm");
        //    //Console.WriteLine(result.StandardOut);
        //    //Console.WriteLine(result.StandardError);
        //    //CommandResult result = ExecuteAndCapture(@"add.exe");
        //    //Console.WriteLine(result.StandardOut);
        //    //System.Console.WriteLine("Hello World!");
        //    // CommandResult result= Compile(1, Language.CPLUSPLUS);
        //    //  Console.WriteLine(result.ExitCode);

        //    JudgeResult result = Excute(1, 1000, Language.CPLUSPLUS, 1, 1000, 65535);
        //    if (result != null)
        //    {
        //        Console.WriteLine(result.Msg);
        //        Console.WriteLine(result.UsedMemory);
        //        Console.WriteLine(result.UsedTime);
        //    }

        //    //Console.WriteLine(TestInt(3));

        //    //CommandResult result = ExecuteAndCapture( "1.exe", null, WorkSpace, "1 2", 2 * 1000);
        //    // CommandResult result = ExecuteAndCapture(@"D:\Judgefile\1.exe", null, null, "1 2", 2 * 1000);
        //    // Console.WriteLine(result.StandardOut);

        //    System.Console.WriteLine("Hello World!");
        //}
    }
}
