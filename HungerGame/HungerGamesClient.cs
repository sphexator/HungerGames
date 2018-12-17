using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HungerGame.Entities;
using HungerGame.Generator;
using Microsoft.Extensions.DependencyInjection;
using EventHandler = HungerGame.Events.EventHandler;

namespace HungerGame
{
    public class HungerGamesClient
    {
        private readonly HungerGameConfig _config;
        private ImageGenerator imgGen;
        private IServiceProvider service;

        public HungerGamesClient()
        {
            this.service = service;
            _config = new HungerGameConfig();
        }

        public HungerGamesClient(HungerGameConfig cfg)
        {
            _config = cfg;
            this.service = service;
        }

        public async Task<HungerGameResult> HungerGameRoundAsync(IEnumerable<HungerGameProfile> profiles)
        {
            var imgGenerator = new ImageGenerator();
            var hungerGameProfiles = profiles.ToList();
            string output = null;
            foreach (var x in hungerGameProfiles)
            {
                    if (!x.Status) continue;
                    var eventString = EventHandler.EventManager(x);
                    if (eventString == null) continue;
                try
                {
                    var name = x.Name;
                    output += $"{name.PadRight(20)} {eventString}\n";
                }
                catch
                {
                    output += $"{x.Name.PadRight(20)} {eventString}\n";
                }
            }

            SelfHarm(hungerGameProfiles);
            Fatigue(hungerGameProfiles);
            var image = await imgGenerator.GenerateEventImageAsync(hungerGameProfiles);
            var data = new HungerGameResult
            {
                Content = output,
                Image = image,
                Participants = hungerGameProfiles
            };
            return data;
        }

        private static void Fatigue(IEnumerable<HungerGameProfile> profiles)
        {
            var rand = new Random();
            foreach (var x in profiles)
            {
                if (!x.Status) continue;
                x.Hunger = x.Hunger + rand.Next(5, 10);
                x.Sleep = x.Sleep + rand.Next(20, 30);
            }
        }

        private static void SelfHarm(IEnumerable<HungerGameProfile> profiles)
        {
            var rand = new Random();
            foreach (var x in profiles)
            {
                if (!x.Status) continue;
                int dmg;
                if (x.Hunger >= 90 || x.Sleep >= 100) dmg = rand.Next(20, 30);
                else if (x.Hunger >= 80 || x.Sleep >= 90) dmg = rand.Next(5, 10);
                else continue;
                if (x.Health - dmg <= 0)
                {
                    x.Status = false;
                    x.Health = 0;
                }
                else
                {
                    x.Health = x.Health - dmg;
                }
            }
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<Random>();
            services.AddSingleton<HttpClient>();

        }
    }
}