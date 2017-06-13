using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace bot_bot.Modules
{
    public class Test : ModuleBase<SocketCommandContext>
    {
        [Command("Test")]
        public async Task test(string message)
        {
            await Context.Channel.SendMessageAsync(message);
            
            Console.WriteLine(Context.User.Username + ": " + message);
        }
    }
}