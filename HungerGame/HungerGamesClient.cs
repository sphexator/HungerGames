using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using HungerGame.Entities;
using HungerGame.Entities.Internal;
using HungerGame.Entities.Items;
using HungerGame.Entities.User;
using HungerGame.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace HungerGame
{
    public class HungerGamesClienti : IDisposable
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

        public async Task<HungerGameResult> PlayAsync(List<HungerGameProfile> profiles, ItemDrop itemDrops) =>
            await _service.GetRequiredService<GameHandler>().RoundAsync(profiles, itemDrops);

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

        public void Dispose()
        {

        }
    }
}