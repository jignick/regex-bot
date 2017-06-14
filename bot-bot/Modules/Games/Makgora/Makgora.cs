using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace bot_bot.Modules.Games.Makgora
{
    public class Makgora : ModuleBase
    {
        private static bool _isDuelActive;
        private static Orc _orc;
        private static Orc _felOrc;
        private static Random _rng;
        private static string _whoPlays;
        
        [Command("Makgora")]
        [Alias("makgora")]
        [Summary("Starts a new Mak'gora duel.")]
        public async Task makgora(IUser user)
        {
            _orc = new Orc(Context.User.Mention);
            _felOrc = new Orc(user.Mention);    

            if (_orc.Name != _felOrc.Name && _isDuelActive == false)
            {
                _isDuelActive = true;
                _rng = new Random();

                var choice = _rng.Next(0, 2);
                if (choice == 0)
                    _whoPlays = _orc.Name;
                else
                    _whoPlays = _felOrc.Name;
                

                var embedMessage = new EmbedBuilder();

                embedMessage.Color = new Color(255, 255, 255);
                embedMessage.Description = ($"A Mak'gora duel just started between, {_orc.Name} and {_felOrc.Name}!\n" +
                    $"{_whoPlays} goes first!\n" +
                    $"{_orc.Name} has {_orc.Health} health.\n" +
                    $"{_felOrc.Name} has {_felOrc.Health} health.\n");

                await ReplyAsync("", false, embedMessage);

            }
            else if (_orc.Name == _felOrc.Name)
                await ReplyAsync($"{_orc.Name}, you can't declare a Mak'gora on yourself. :thonk:");
            else
                await ReplyAsync($"{_orc.Name}, there is currently another Mak'gora in progress.");
        }

        [Command("Surrender")]
        [Alias("surrender")]
        [Summary("Forfeits the duel.")]
        public async Task surrender()
        {
            if (_isDuelActive && (Context.User.Mention == _orc.Name || Context.User.Mention == _felOrc.Name)) // one () less?
            {
                var embedMessage = new EmbedBuilder();

                embedMessage.Description = ($"The duel has stopped.");  
                await ReplyAsync("", false, embedMessage);
                _isDuelActive = false;
            }
            else 
            {
                var embedMessage = new EmbedBuilder();

                embedMessage.Description = ($"You are not fighting.");
                await ReplyAsync("", false, embedMessage);
            }
        }

        [Command("Katsavidi")]
        [Alias("katsavidi")]
        [Summary("Performs a katsavidi attack while in a Mak'gora duel.")]
        public async Task katsavidi()
        {
            if (_isDuelActive && (Context.User.Mention == _orc.Name || Context.User.Mention == _felOrc.Name))
            {
                if (!(Context.User.Mention == _whoPlays))
                {
                    var embedMessage = new EmbedBuilder();
                    embedMessage.Description = ($"It's not your turn {Context.User.Mention}.");                    

                    await ReplyAsync("", false, embedMessage);      
                    return;              
                }

                if (Context.User.Mention != _felOrc.Name)
                {
                    var embedMessage = new EmbedBuilder();

                    _orc.Attack(_felOrc);
                    _whoPlays = _felOrc.Name;

                    if (_felOrc.Health > 0)
                    {    
                        embedMessage.Description = ($"{_orc.Name}'s katsavidi went through {_felOrc.Name}.\n" +
                            $"{_felOrc.Name} has {_felOrc.Health} health.\n" +
                            $"{_felOrc.Name}, your turn!");

                            await ReplyAsync("", false, embedMessage);
                    }
                    else
                    {
                        embedMessage.Description = ($"{_felOrc.Name} has died. [*]\n" +
                            $"{_orc.Name} is victorious!");

                        _isDuelActive = false;

                        await ReplyAsync("", false, embedMessage);
                    }
                }
                else if(Context.User.Mention == _felOrc.Name)
                {
                    var embedMessage = new EmbedBuilder();
                    
                    _felOrc.Attack(_orc);
                    _whoPlays = _orc.Name;

                    if (_orc.Health > 0)
                    {
                        embedMessage.Description = ($"{_felOrc.Name}'s katsavidi went through {_orc.Name}.\n" +
                            $"{_orc.Name} has {_orc.Health} health.\n" +
                            $"{_orc.Name}, your turn!");

                        await ReplyAsync("", false, embedMessage);
                    }
                    else
                    {
                        embedMessage.Description = ($"{_orc.Name} has died. [*]\n" +
                            $"{_felOrc.Name} is victorious!");

                        _isDuelActive = false;
                        await ReplyAsync("", false, embedMessage);
                    }
                }
            }
            else
            {
                var embedMessage = new EmbedBuilder();

                embedMessage.Description = ($"You are not fighting?");
                await ReplyAsync("", false, embedMessage);
            }
        }
    }
}