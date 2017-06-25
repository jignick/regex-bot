using System;
using System.Threading.Tasks;
using Discord.Net;
using Discord.WebSocket;

namespace bot_bot
{
    public class InsultBots
    {
        private DiscordSocketClient _client;
        private static int _messageCount;

        public InsultBots(DiscordSocketClient client)
        {
            _client = client;
            _client.MessageReceived += InsultBotsAsync;
            _messageCount = 0;            
        }

        private async Task InsultBotsAsync(SocketMessage sockMessage)
        {
            _messageCount++;
            
            var message = sockMessage as SocketUserMessage;

            if (message == null)
                return;

            if (_messageCount % 10 == 0)
            {
                if (message.Author.IsBot)
                {
                    await message.Channel.SendMessageAsync($"stfu normie bot {message.Author}");
                }
            }    

            await Task.CompletedTask;
        }
    }
}