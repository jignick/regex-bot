using System;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace bot_bot.Modules.Games.Makgora
{
    public class Orc
    {
        private Random _rng;

        public string Name { get; private set; }
        public int Health { get; private set; } 

        public Orc(string name)
        {
            Name = name;
            Health = 100;
        }

        public void Attack(Orc orc)
        {
            _rng = new Random();
            orc.Health -= _rng.Next(10, 26);
        }
    }
}