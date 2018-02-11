using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace bot_bot
{
    public class LogHandler
    {
        private string _path;
        private DiscordSocketClient _client;
        private FileStream _fStream;
        private StreamWriter _sWriter; 

        public LogHandler(DiscordSocketClient client)
        {
            _path = "server.log";
            _client = client;
            _client.MessageReceived += LogMessageAsync;
        }

        private async Task LogMessageAsync(SocketMessage sockMessage)
        {
            var message = sockMessage as SocketUserMessage;

            if (message == null)
            {
                return;
            }

            var messageToLog = $"[{message.Author}]-[{DateTime.Now}]: {message}";

            using (_fStream = new FileStream(_path, FileMode.Append))
            {
                using (_sWriter = new StreamWriter(_fStream))
                {
                    await _sWriter.WriteLineAsync(messageToLog);
                    Console.WriteLine(messageToLog);
                }
            }

            await Task.CompletedTask;
        }
    }
}