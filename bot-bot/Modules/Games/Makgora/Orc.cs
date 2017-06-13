using System;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace bot_bot.Modules.Games.Makgora
{
    public class Orc
    {
        /*
        private string _name;
        private BattlePhase _phase;
        private bool _turn;
        */

        public string Name { get; set; }
        public BattlePhase Phase { get; set; }
        public bool Turn { get; set; }
        public int Health { get; set;} 

        public Orc(string name, BattlePhase phase, bool turn)
        {
            Name = name;
            Phase = phase;
            Turn = turn;
            Health = 100;
        }

        public void Attack(Orc orc)
        {
            
        }
    }
}