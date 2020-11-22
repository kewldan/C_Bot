using System;
using System.IO;

namespace Bot
{
    class Logger
    {
        public static string LogName = null;

        public static bool FileAccessGranted = true;


        public static void Init()
        {
            if (Logger.LogName == null) Logger.LogName = "./Logs/log" + Helper.millis().ToString() + ".txt";
            if (!File.Exists("./Logs/")) Directory.CreateDirectory("./Logs/");
        }
        public static void Log(string msg = "")
        {
            Console.WriteLine("[LOG] " + msg);
            if (Config.ENABLE_LOG_FILE)
            {
                FileLogWriteText("[LOG] " + msg);
            }
        }

        public static void Warning(string msg = "")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[WARNING] " + msg);
            Console.ResetColor();
            if (Config.ENABLE_LOG_FILE)
            {
                FileLogWriteText("[WARNING] " + msg);
            }
        }

        public static void Error(string msg = "")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR] " + msg);
            Console.ResetColor();
            if (Config.ENABLE_LOG_FILE)
            {
                FileLogWriteText("[ERROR] " + msg);
            }
        }
        public static void FatalError(string msg = "")
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[FATAL ERROR] " + msg);
            Console.ResetColor();
            if (Config.ENABLE_LOG_FILE)
            {
                FileLogWriteText("[FATAL ERROR] " + msg);
            }
            Environment.Exit(45);
        }

        static void FileLogWriteText(string text = "NULL")
        {
            if (!FileAccessGranted) return;
            using (FileStream wf = new FileStream(Logger.LogName, FileMode.Append))
            {
                using (StreamWriter sw = new StreamWriter(wf))
                {
                    sw.WriteLine("[" + DateTime.Now.ToString() + "] " + text);
                }
            }
        }
    }
}