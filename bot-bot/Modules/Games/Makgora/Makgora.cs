using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace bot_bot.Modules.Games.Makgora
{
    public class Makgora : ModuleBase
    {
        private static bool _isDuelActive = false;
        private Orc _orc;
        private Orc _felOrc;

        [Command("Makgora")]
        [Alias("makgora")]
        [Summary("Starts a new Mak'gora duel.")]
        public async Task makgora(IUser user)
        {
            // (Panagiotis):TODO battlephase and turn should be chosen randomly
            _orc = new Orc(Context.User.Mention, BattlePhase.Attack, true);
            _felOrc = new Orc(user.Mention, BattlePhase.Defend, false);

            if (_orc.Name != _felOrc.Name && _isDuelActive == false)
            {
                _isDuelActive = true;

                if (_orc.Turn)
                {
                    _orc.Phase = BattlePhase.Attack;
                    _orc.Turn = true;
                    _felOrc.Phase = BattlePhase.Defend;
                }
                else
                {
                    _felOrc.Phase = BattlePhase.Attack;
                    _felOrc.Turn = true;
                    _orc.Phase = BattlePhase.Defend;
                }

                // o Embed builder dinei dunatothta na peirakseis to format tou message pou tha paei sto discord
                // Remove hardcoded turns
                var embedMessage = new EmbedBuilder();

                embedMessage.WithColor(new Color(255, 255, 255));
                embedMessage.Description = ($"A Mak'gora duel just started between, {_orc.Name} and {_felOrc.Name}!\n" +
                    $"{_orc.Name} goes first!\n" +
                    $"{_orc.Name} has {_orc.Health} health.\n" +
                    $"{_felOrc.Name} has {_felOrc.Health} health.\n");

                await ReplyAsync("", false, embedMessage);

            }
            else if (_orc.Name == _felOrc.Name)
            {
                await ReplyAsync($"{_orc.Name}, you can't declare a Mak'gora on yourself. :thonk:");
            }
            else
            {
                await ReplyAsync($"{_orc.Name}, there is currently another Mak'gora in progress.");
            }
        }

        [Command("Surrender")]
        [Alias("surrender")]
        [Summary("Forfeits the duel.")]
        public async Task surrender()
        {
            if (_isDuelActive == true && (Context.User.Mention == _orc.Name || Context.User.Mention == _felOrc.Name)) // one () less?
            {
                await ReplyAsync("The fight has stopped");
                _isDuelActive = false;

                
            }
            else 
            {

            }
        }

        [Command("Katsavidi")]
        [Alias("katsavidi")]
        [Summary("Performs a katsavidi attack while in a Mak'gora duel.")]
        public async Task katsavidi()
        {

        }
    }
}