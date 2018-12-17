using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using HungerGame.Entities;
using HungerGame.Entities.Internal;
using HungerGame.Entities.User;
using HungerGame.Generator;
using Microsoft.Extensions.DependencyInjection;
using EventHandler = HungerGame.Handler.EventHandler;

namespace HungerGame
{
    public class HungerGamesClient
    {
        private readonly HungerGameConfig _config;
        private readonly IServiceProvider _service;

        public HungerGamesClient()
        {
            _service = ConfigureServices();
            _config = new HungerGameConfig();
        }

        public HungerGamesClient(HungerGameConfig cfg)
        {
            _config = cfg;
             _service = ConfigureServices();
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
                if (!x.Alive) continue;
                x.Hunger = x.Hunger + rand.Next(5, 10);
                x.Tiredness = x.Tiredness + rand.Next(20, 30);
            }
        }

        private static void SelfHarm(IEnumerable<HungerGameProfile> profiles)
        {
            var rand = new Random();
            foreach (var x in profiles)
            {
                if (!x.Alive) continue;
                int dmg;
                if (x.Hunger >= 90 || x.Tiredness >= 100) dmg = rand.Next(20, 30);
                else if (x.Hunger >= 80 || x.Tiredness >= 90) dmg = rand.Next(5, 10);
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

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<Random>();
            services.AddSingleton<HttpClient>();

            var assembly = Assembly.GetAssembly(typeof(HungerGamesClient));
            var requiredServices = assembly.GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IRequired))
                            && !x.GetTypeInfo().IsInterface && !x.GetTypeInfo().IsAbstract).ToList();
            foreach (var x in requiredServices) services.AddSingleton(x);
            return services.BuildServiceProvider();
        }
    }
}