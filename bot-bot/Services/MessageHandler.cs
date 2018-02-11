using System;
using System.Threading.Tasks;
using Discord.Net;
using Discord.WebSocket;

namespace bot_bot
{
    public class MessageHandler
    {
        private DiscordSocketClient _client;

        public MessageHandler(DiscordSocketClient client)
        {
            _client = client;
            _client.MessageReceived += ClearMessageAsync;
        }

        private async Task ClearMessageAsync(SocketMessage socketMessage)
        {
            var message = socketMessage as SocketUserMessage;

            if (message == null)
            {
                return;
            }

            if (message.Content.StartsWith("!")) 
            {
                await message.DeleteAsync();
            }

            if (message.Author.Username == "Rythm")
            {
                await _client.GetGuild(152690567720075264).GetTextChannel(404659774043717632).SendMessageAsync($"{message.Content}");            
                await message.DeleteAsync();     
                await socketMessage.DeleteAsync();
            }

            await Task.CompletedTask;
        }
    }
}