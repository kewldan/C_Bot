using System.IO;
using System;
using DSharpPlus.EventArgs;


namespace Bot
{
    class Config
    {
        public static string pr = "!";
        public static string TOKEN;
        public static bool ENABLE_LOG_FILE = true;
        public static string VERSION = "1.0";
        public static int BUILD = 0;
        public static string RU_PATH = "./RU.json";
        public static ReadyEventArgs readyEventArgs;
        private static string[] lines;

        public static void Init()
        {
            try{
                lines = File.ReadAllLines("./config.txt");
            }catch(IOException){
                Logger.FatalError("[C] config.txt not found");
            }
            foreach (string line in lines)
            {
                string[] spl = line.Split("=");
                if (spl.Length == 2)
                {
                    if (spl[0].Equals("BUILD"))
                    {
                        try
                        {
                            BUILD = int.Parse(spl[1]);
                        }
                        catch (Exception) { }
                    }
                    else if (spl[0].Equals("TOKEN"))
                    {
                        try
                        {
                            TOKEN = spl[1];
                        }
                        catch (Exception) { }
                    }
                    else if (spl[0].Equals("PREFIX"))
                    {
                        try
                        {
                            pr = spl[1];
                        }
                        catch (Exception) { }
                    }
                    else if (spl[0].Equals("VERSION"))
                    {
                        try
                        {
                            VERSION = spl[1];
                        }
                        catch (Exception) { }
                    }
                    else if (spl[0].Equals("ENABLE_LOG_FILE"))
                    {
                        try
                        {
                            ENABLE_LOG_FILE = spl[1].Equals("1");
                        }
                        catch (Exception) { }
                    }
                }
            }
            try
            {
                lines[0] = "BUILD=" + (int.Parse(lines[0].Split("=")[1]) + 1).ToString();
                File.WriteAllLines("./config.txt", lines);
            }
            catch (Exception err)
            {
                Logger.Error("[C] Failed to write new build int, message: " + err.Message);
            }
        }
    }
}