using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace bot_bot
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        private CommandHandler _handler;
        private LogHandler _logger;
        private MessageCleaner _cleaner;

        public async Task StartAsync()
        {
            Console.WriteLine("[Regex Bot - github.com/jignick/regex-bot]");
            _client = new DiscordSocketClient();

            var _token = "";

            Console.WriteLine("Connecting to server");
            await _client.LoginAsync(TokenType.Bot, _token);
            Console.WriteLine("Connected");

            _handler = new CommandHandler(_client);
            _logger = new LogHandler(_client);
            _cleaner = new MessageCleaner(_client);

            await _client.StartAsync();
            await Task.Delay(-1);
        }
    }
}
