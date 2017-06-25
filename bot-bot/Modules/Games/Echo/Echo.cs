using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace bot_bot.Modules
{
    public class Test : ModuleBase<SocketCommandContext>
    {
        [Command("Echo")]
        [Alias("echo")]
        [Summary("Echoes the string after the command")]
        public async Task test(string message)
        {
            await Context.Channel.SendMessageAsync(message);
        }
    }
}