using Discord;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Simple_Discord_Bot
{
    class Program
    {
        public static string BOT_TOKEN { get; set; }
        public static string BOT_PREFIX { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Please Enter Your Bots Token...");
            BOT_TOKEN = Console.ReadLine();
            Console.WriteLine("Please Enter A Prefix For Your Bot...");
            BOT_PREFIX = Console.ReadLine();
            new Program().MainAsync().GetAwaiter().GetResult();
        }
        public static DiscordSocketClient _client;

        public Program()
        {
            _client = new DiscordSocketClient();
            _client.MessageReceived += MessageReceivedAsync;
        }

        public async Task MainAsync()
        {
            await _client.LoginAsync(TokenType.Bot, BOT_TOKEN);
            await _client.StartAsync();
            Console.WriteLine(string.Format("Bot Successfully Loaded\nToken : {0}\nPrefix : {1}", BOT_TOKEN, BOT_PREFIX));
            Console.Title = "Simple Discord Bot";
            await Task.Delay(-1);
        }
        private async Task MessageReceivedAsync(SocketMessage message)
        {
            if (message.Content == BOT_PREFIX + "ping")
            {
                Console.WriteLine(message.Author.Username +"#"+ message.Author.Discriminator + " Used Command [" + message.Content.Replace(BOT_PREFIX, "") + "]");
                await message.Channel.SendMessageAsync("pong");
            }
            else if (message.Content.Contains(BOT_PREFIX + "prefix")) { 
                BOT_PREFIX = message.Content.Replace(BOT_PREFIX + "prefix ", "");
                await message.Channel.SendMessageAsync("Bots Ptrefix Has Been Canged To [" + BOT_PREFIX + "]");
                Console.WriteLine(message.Author.Username + "#" + message.Author.Discriminator + " Used Command [" + message.Content.Replace(BOT_PREFIX, "").Replace(" ", "") + "]");
            }
            else if (message.Content == BOT_PREFIX + "help") { 
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(255, 0, 0);
                embed.WithTitle("Simple Discord Bot | HELP");
                embed.WithFooter("Requested By " + message.Author.Username);
                embed.WithDescription("Commands");
                embed.WithCurrentTimestamp();
                embed.WithThumbnailUrl(message.Author.GetAvatarUrl());
                embed.AddField("Help Command", BOT_PREFIX + "help", true);
                embed.AddField("Change Prefix", BOT_PREFIX + "prefix", true);
                embed.AddField("Ping Check", BOT_PREFIX + "ping", true);
                await message.Channel.SendMessageAsync("", false, embed.Build());
                Console.WriteLine(message.Author.Username + "#" + message.Author.Discriminator + " Used Command [" + message.Content.Replace(BOT_PREFIX, "") + "]");
            }
        }
    }
}
