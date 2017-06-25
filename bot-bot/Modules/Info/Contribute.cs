using System;
using Discord.Net;
using Discord.Commands;
using Discord;
using System.Threading.Tasks;

namespace bot_bot.Modules.Services.Contribution
{
   public class Contribute : ModuleBase<SocketCommandContext>
    {
        [Command("Contribute")]
        [Alias("contribute")]
        [Summary("How to help us and contribute to our project")]
        public async Task giveInfo() 
        {
            var builder = new EmbedBuilder();
            builder.Title = "How to contribute to regex-bot";
            builder.Description = "https://github.com/jignick/regex-bot";
            await Context.Channel.SendMessageAsync("", false, builder);
        }
    
    }

}
