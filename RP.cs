using System;
using System.IO;
using System.Collections.Generic;
using DSharpPlus.Entities;


namespace Bot
{
    [Serializable]
    class JobElem
    {
        public int needXP;
        public bool needPJ;
        public int needAge;
        public int Salary;
        public string name;

        public JobElem(int xp, int age, int salary, bool pj, string name)
        {
            needXP = xp;
            needAge = age;
            needPJ = pj;
            Salary = salary;
            this.name = name;
        }

        public JobElem(int xp, int age, int salary, string name)
        {
            needXP = xp;
            needAge = age;
            needPJ = false;
            Salary = salary;
            this.name = name;
        }
    }
    class Job
    {
        public static JobElem Jobless = new JobElem(0, 0, 1500, "Jobless");
        public static JobElem Janitor = new JobElem(0, 12, 15000, "Janitor");
        public static JobElem Loader = new JobElem(0, 14, 45000, true, "Loader");
        public static JobElem Promoter = new JobElem(0, 14, 45000, "Promoter");
        public static JobElem Cleaner = new JobElem(0, 16, 20000, true, "Cleaner");
        public static JobElem Cashir = new JobElem(0, 18, 30000, "Cashir");
        public static JobElem Security = new JobElem(0, 18, 32000, "Security");
        public static JobElem Lawyer = new JobElem(0, 18, 48000, "Lawyer");
        public static JobElem Teacher = new JobElem(0, 20, 41000, "Teacher");
        public static JobElem Director = new JobElem(0, 25, 50000, "Director");
        public static JobElem CompanyDirector = new JobElem(0, 30, 70000, "CompanyDirector");
        public static JobElem Mayor = new JobElem(0, 40, 635000, "Mayor");
        public static JobElem StateDuma = new JobElem(0, 45, 1000000, "StateDuma");
        public static JobElem President = new JobElem(0, 60, 8600000, "President");
    }

    [Serializable]
    enum Phone
    {
        Without
    }

    [Serializable]
    enum Home
    {
        Bum
    }

    [Serializable]
    class RPUser
    {
        public byte age, xp;
        public ulong id, gid;
        public int money;
        public byte rest;
        public long lastRest;
        public bool pj;
        public Phone phone;
        public JobElem job;
        public Home home;

        public RPUser(ulong id, ulong gid)
        {
            this.id = id;
            this.gid = gid;
            job = Job.Jobless;
            phone = Phone.Without;
            home = Home.Bum;
        }
    }

    class RP
    {
        public static List<RPUser> users = new List<RPUser>();
        public static string[] cmds = { "info" };

        public static void SaveAllUsers()
        {
            using (FileStream binaryfile = new FileStream("./Bases/rp/users.bin", FileMode.OpenOrCreate))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(binaryfile, users);
            }

        }

        public static void LoadAllUsers()
        {
            if (!File.Exists("./Bases/rp/users.bin")) return;

            using (FileStream binaryfile = new FileStream("./Bases/rp/users.bin", FileMode.OpenOrCreate))
            {
                try
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    users = (List<RPUser>)binaryFormatter.Deserialize(binaryfile);
                }
                catch (Exception) { }

                binaryfile.Close();
            }
        }

        public static void Init()
        {
            for (int i = 0; i < cmds.Length; i++)
            {
                cmds[i] = Config.pr + "rp " + cmds[i];
            }
        }

        public static void MessageCreated(string cmd, string[] spl, DiscordMessage msg)
        {
            ulong aid = msg.Author.Id;
            ulong gid = msg.Channel.GuildId;

            if (!HasRPUser(aid))
            {
                users.Add(new RPUser(aid, gid));
                Logger.Log("[RP] REGISTRY " + msg.Author.Username);
            }

            if (spl.Length == 1)
            {
                DiscordEmbedBuilder Embed = new DiscordEmbedBuilder();
                Embed.Title = "RP режим";
                Embed.Description = string.Format(@"
                РП режим от Avenger - это текстовая
                игра о реальной жизни (Плати налоги, ходи в школу и т.д.)
                
                Чтобы начать играть напиши {0}rp help

                С любовью Avenger :-D
                ", Config.pr);
                msg.Channel.SendMessageAsync(embed: Embed);
            }
            else
            {
                cmd = spl[1];
            }

            if (cmd.Equals("help"))
            {
                DiscordEmbedBuilder Embed = new DiscordEmbedBuilder();
                Embed.Title = "RP режим";
                Embed.Color = DiscordColor.Aquamarine;
                Embed.AddField("Информация", string.Join(", ", cmds));
                msg.Channel.SendMessageAsync(embed: Embed);
            }
            else if (cmd.Equals("info"))
            {
                DiscordEmbedBuilder Embed = new DiscordEmbedBuilder();
                Embed.Title = "Информация о пользователе " + msg.Author.Username;
                RPUser user = GetUserByDId(aid);
                Embed.AddField("Работа", Language.getString(user.job.name));
                Embed.AddField("Телефон", Language.getString(user.phone.ToString()));
                Embed.AddField("Дом", Language.getString(user.home.ToString()));
                Embed.Color = DiscordColor.Aquamarine;
                msg.Channel.SendMessageAsync(embed: Embed);
            }
            else if (cmd.StartsWith("job"))
            {
                if (spl.Length == 2)
                {
                    msg.Channel.SendMessageAsync("Ошибка, введите название работы");
                }
                else
                {

                }
            }
        }

        public static void MessageCreatedAdmin(string cmd, string[] spl, DiscordMessage msg)
        {

            ulong aid = msg.Author.Id;
            ulong gid = msg.Channel.GuildId;

            if (aid != 394205235037339648UL)
            {
                msg.Channel.SendMessageAsync("Ошибка, доступ запрещён");
            }
            else
            {
                if (spl.Length == 1)
                {
                    msg.Channel.SendMessageAsync("Доступ к [РП АДМИН] разрешён");
                }
                else
                {
                    if (spl[1].Equals("getUserId"))
                    {
                        if (spl.Length == 2)
                        {
                            msg.Channel.SendMessageAsync("WIP");
                        }
                        else
                        {
                            msg.Channel.SendMessageAsync("Ошибка, вы должны ввести [АРГУМЕНТ 1] для работы команды");
                        }
                    }
                    else
                    {
                        msg.Channel.SendMessageAsync("Введите команду в [АДМИН РП]");
                    }
                }
            }
        }

        public static bool HasRPUser(ulong id)
        {
            foreach (RPUser user in users)
            {
                if (user.id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CanHaveNewWork(RPUser user, JobElem job)
        {
            return user.age >= job.needAge && user.xp >= job.needXP && Convert.ToUInt16(user.pj) >= Convert.ToUInt16(job.needPJ);
        }

        public static int ComparePercanteWord(string reco, string gypo)
        {
            int count = 0, percant = 0, percant1 = 0, percant2 = 0;
            char[] recos = reco.ToCharArray();
            char[] gypos = gypo.ToCharArray();
            for (int i = 0; i < recos.Length; i++)
            {
                for (int j = 0; j < gypos.Length; j++)
                {
                    if (recos[i] == gypos[j])
                    { count++; }
                }
            }
            if (count > 0)
            {
                percant1 = (100 / reco.Length) * count;
                percant2 = (100 / gypo.Length) * count;
                percant = (percant1 + percant2) / 2;
            }
            else
            { percant = 0; }
            return percant;
        }

        public static RPUser GetUserByDId(ulong id)
        {
            foreach (RPUser u in users)
            {
                if (u.id == id)
                {
                    return u;
                }
            }
            return null;
        }
    }
}