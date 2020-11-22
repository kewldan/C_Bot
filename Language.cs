using System.IO;
using System.Text.Json;
using System;
using System.Collections.Generic;

namespace Bot
{
    class Language
    {
        public static string langText;
        public static Dictionary<string, string> lang;
        public static string getString(string id)
        {
            string forOut = "";
            return lang.TryGetValue(id, out forOut) ? forOut : id;
        }

        public static void Init()
        {
            try
            {
                langText = File.ReadAllText(Config.RU_PATH);
            }
            catch (Exception err)
            {
                Logger.Error("[LANG] Failed to load all text from file " + Config.RU_PATH + ", message: " + err.Message);
            }
            try
            {
                lang = JsonSerializer.Deserialize<Dictionary<string, string>>(langText);
            }
            catch (Exception err)
            {
                Logger.Error("[LANG] Failed to deserialize all text from file " + Config.RU_PATH + ", message: " + err.Message);
                lang = new Dictionary<string, string>();
            }
        }
    }
}