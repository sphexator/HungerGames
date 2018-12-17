﻿using System;
using HungerGame.Entities;

namespace HungerGame.Events.Types
{
    internal class Meet : IRequired
    {
        private readonly Random _random;
        internal Meet(Random random)
        {
            _random = random;
        }

        private readonly string[] _meetEventStrings =
        {
            "Climbed up in a tree, seeing someone in the distance",
            "Lurks behind a tree, spying on someone"
        };

        internal string MeetEvent() => _meetEventStrings[_random.Next(0, _meetEventStrings.Length)];
    }
}