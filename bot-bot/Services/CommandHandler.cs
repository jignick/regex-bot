using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace bot_bot
{
    public class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _service;

        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();
            _service.AddModulesAsync(Assembly.GetEntryAssembly());
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage sockMsg)
        {
            var message = sockMsg as SocketUserMessage;

            if (message == null)
                return;

            var context = new SocketCommandContext(_client, message);
            var argPos = 0;

            if (message.HasCharPrefix('>', ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}
