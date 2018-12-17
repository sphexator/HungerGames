using System;
using System.Collections.Generic;
using System.Net.Http;
using HungerGame.Entities.Internal;
using System.Threading.Tasks;
using HungerGame.Entities;
using HungerGame.Entities.User;
using HungerGame.Generator;

namespace HungerGame.Handler
{
    internal class GameHandler : IRequired
    {
        private readonly HttpClient _httpClient;
        private readonly Random _random;
        private readonly ImageGenerator _generator;
        private readonly EventHandler _eventHandler;
        internal GameHandler(Random random, HttpClient httpClient, ImageGenerator generator, EventHandler eventHandler)
        {
            _random = random;
            _httpClient = httpClient;
            _generator = generator;
            _eventHandler = eventHandler;
        }

        internal async Task<HungerGameResult> RoundAsync(List<HungerGameProfile> profiles)
        {
            string output = null;
            foreach (var x in profiles)
            {
                if (!x.Alive) continue;
                var eventString = _eventHandler.EventManager(profiles, x);
                if (eventString == null) continue;
                try
                {
                    output += $"{x.Name.PadRight(20)} {eventString}\n";
                }
                catch
                {
                    output += $"{x.Name.PadRight(20)} {eventString}\n";
                }
            }

            Fatigue(profiles);
            SelfHarm(profiles);

            var image = await _generator.GenerateEventImageAsync(profiles);
            var data = new HungerGameResult
            {
                Content = output,
                Image = image,
                Participants = profiles
            };
            return data;
        }

        private void Fatigue(IEnumerable<HungerGameProfile> profiles)
        {
            foreach (var x in profiles)
            {
                if (!x.Alive) continue;
                x.Hunger = x.Hunger + _random.Next(5, 10);
                x.Tiredness = x.Tiredness + _random.Next(20, 30);
            }
        }

        private void SelfHarm(IEnumerable<HungerGameProfile> profiles)
        {
            foreach (var x in profiles)
            {
                if (!x.Alive) continue;
                int dmg;
                if (x.Hunger >= 90 || x.Tiredness >= 100) dmg = _random.Next(20, 30);
                else if (x.Hunger >= 80 || x.Tiredness >= 90) dmg = _random.Next(5, 10);
                else continue;
                if (x.Health - dmg <= 0)
                {
                    x.Alive = false;
                    x.Health = 0;
                }
                else
                {
                    x.Health = x.Health - dmg;
                }
            }
        }
    }
}
