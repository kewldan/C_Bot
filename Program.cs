using System.IO;
using System.Threading.Tasks;
using System.Timers;
using DSharpPlus;
using System;

namespace Bot
{
    class Program
    {
        static DiscordClient discord;

        static void Main(string[] args)
        {
            if (!File.Exists("./Bases/")) Directory.CreateDirectory("./Bases/");
            if (!File.Exists("./Bases/rp/")) Directory.CreateDirectory("./Bases/rp/");

            //Inits
            Logger.Init();
            Language.Init();
            Config.Init();
            RP.Init();
            ///////

            RP.LoadAllUsers();
            Timer timer = new System.Timers.Timer();
            timer.Elapsed += SaveRP;
            timer.Interval = 5000;
            timer.Enabled = true;
            Logger.Log("RPSaver enabled");

            Timer rester = new System.Timers.Timer();
            rester.Elapsed += RestAllCheck;
            rester.Interval = 5000;
            rester.Enabled = true;
            Logger.Log("Rest manager enabled");

            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static void SaveRP(Object source, ElapsedEventArgs e)
        {
            try
            {
                RP.SaveAllUsers();
            }
            catch (Exception err)
            {
                Logger.Error("RP save error, message: " + err.Message);
                return;
            }
        }

        static void RestAllCheck(Object source, ElapsedEventArgs e)
        {
            foreach (RPUser user in RP.users)
            {
                if (Helper.millis() > user.lastRest && user.rest < 5)
                {
                    user.rest++;
                    user.lastRest = Helper.millis() + 300;
                }
            }
        }

        static async Task MainAsync(string[] args)
        {
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = Config.TOKEN,
                TokenType = TokenType.Bot
            });

            discord.MessageCreated += async e =>
            {
                CommandHandler.MessageCreated(e);
            };

            discord.Ready += async e =>
            {
                Config.readyEventArgs = e;
                Logger.Log("Bot is ready");
            };

            await discord.ConnectAsync();
            Console.ReadKey(true);
        }
    }
}