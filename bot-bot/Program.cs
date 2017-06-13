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

        public async Task StartAsync()
        {
            _client = new DiscordSocketClient();

            var _token = "API_KEY";
            
            await _client.LoginAsync(TokenType.Bot, _token);

            _handler = new CommandHandler(_client);

            await _client.StartAsync();
            await Task.Delay(-1);
        }
    }
}
