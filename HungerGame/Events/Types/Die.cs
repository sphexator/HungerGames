using System;
using HungerGame.Entities;

namespace HungerGame.Events.Types
{
    internal class Die : IRequired
    {
        private readonly string[] _dieResponseStrings =
        {
            "Climbed up a tree and fell to his death",
            "Got bit by a snake and decided to chop his leg off, bleed to death",
            "I used to be interested in this game, but then I took an arrow to the knee"
        };

        internal string DieEvent(HungerGameProfile profile)
        {
            var rand = new Random();
            var response = _dieResponseStrings[rand.Next(0, _dieResponseStrings.Length)];
            profile.Alive = false;
            profile.Health = 0;
            return response;
        }
    }
}