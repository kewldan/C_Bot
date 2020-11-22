using System;
using DSharpPlus.Entities;

namespace Bot
{
    class CommandHandler
    {
        public static string pr = Config.pr;
        public static void MessageCreated(DSharpPlus.EventArgs.MessageCreateEventArgs e)
        {
            DiscordMessage msg = e.Message;
            string[] spl = msg.Content.Split(" ");
            string cmd = msg.Content;
            if (!msg.Author.IsBot)
            {
                Logger.Log("[CH] Created message |" + cmd + "|");
                if (spl[0].Equals(Config.pr + "rp"))
                {
                    RP.MessageCreated(cmd, spl, msg);
                }
                else if (spl[0].Equals(Config.pr + "rpa"))
                {
                    RP.MessageCreatedAdmin(cmd, spl, msg);
                }
                else if (cmd.StartsWith(pr))
                {
                    if (cmd.Equals(pr + "bot"))
                    {
                        DiscordEmbedBuilder Embed = new DiscordEmbedBuilder();
                        Embed.Title = Config.readyEventArgs.Client.CurrentUser.Username + " информация";
                        Embed.AddField("Версия", Config.VERSION + "@" + Config.BUILD);
                        Embed.AddField("Автор", "Avenger#1818 (Ведущий программист)");
                        Embed.AddField("Информация", "Этот бот создан в основном для игр");
                        Embed.AddField("Движок", "C# (.NET 5), DShartPlus, **Lua (Moonsharp)**");
                        msg.Channel.SendMessageAsync(embed: Embed);
                    }
                }
            }
        }
    }
}