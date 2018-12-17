using System;

namespace HungerGame.Entities.Internal.Events
{
    internal class Idle : IRequired
    {
        private readonly Random _random;
        internal Idle(Random random)
        {
            _random = random;
        }

        private readonly string[] _idleStrings =
        {
            "Looks at the sky, pondering about life",
            "frozen in time",
            "Standing still, looking at a tree",
            "Wonders if its possible to do ninjutsu"
        };

        internal string IdleEvent() => _idleStrings[_random.Next(0, _idleStrings.Length)];
    }
}