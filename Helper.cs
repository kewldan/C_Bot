using System;

namespace Bot
{
    class Helper
    {
        public static long millis()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}