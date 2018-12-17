using System;
using System.Linq;
using HungerGame.Entities;

namespace HungerGame.Calculate
{
    internal class ChanceGenerator
    {
        private readonly Random _rand;
        public ChanceGenerator(Random rand)
        {
            _rand = rand;
        }

        private const int Loot = 400;
        private const int Kill = 100;
        private const int Idle = 200;
        private const int Meet = 100;
        private const int Hack = 25;
        private const int Die = 1;
        private const int Sleep = 1;
        private const int Eat = 1;

        internal static string LootName = "Loot";
        internal static string KillName = "Kill";
        internal static string IdleName = "Idle";
        internal static string MeetName = "Meet";
        internal static string HackName = "Hack";
        internal static string DieName = "Die";
        internal static string SleepName = "Sleep";
        internal static string EatName = "Eat";

        internal string EventDetermination(HungerGameProfile profile)
        {
            var loot = LootChance(profile);
            var kill = KillChance(profile);
            var idle = IdleChance(profile);
            var meet = MeetChance(profile);
            var hack = HackChance(profile);
            var die = DieChance(profile);
            var sleep = SleepChance(profile);
            var eat = EatChance(profile);

            var result = loot + kill + idle + meet + hack + die + sleep + eat;
            var rand = new Random();
            var generator = rand.Next(result);
            if (generator <= loot) return LootName;
            if (generator <= loot + kill) return KillName;
            if (generator <= loot + kill + idle) return IdleName;
            if (generator <= loot + kill + idle + meet) return MeetName;
            if (generator <= loot + kill + idle + meet + hack) return HackName;
            if (generator <= loot + kill + idle + meet + hack + die) return DieName;
            if (generator <= loot + kill + idle + meet + hack + die + sleep) return SleepName;
            return generator <= loot + kill + idle + meet + hack + die + sleep + eat ? EatName : null;
        }

        private int LootChance(HungerGameProfile profile)
        {
            var drinks = profile.Inventory.Drinks.Any();
            var food = profile.Inventory.Food.Any();
            if (!drinks || !food) return Loot + 400;
            return Loot - 200;
        }

        private int KillChance(HungerGameProfile profile)
        {
            if (profile.Water == 0 || profile.Food == 0)
                return 0;
            if (profile.Water == 1 || profile.Food == 1) return Kill;
            if (profile.TotalWeapons >= 1 && (profile.Water > 2 ||
                profile.Food > 2))
                return Kill + 10000;
            if (profile.Water > 1 || profile.Food > 1)
                return Kill + 1500;
            return Kill;
        }

        private int SleepChance(HungerGameProfile profile)
        {
            if (profile.Tiredness >= 90) return Sleep + 1000;
            if (profile.Tiredness >= 75) return Sleep + 750;
            if (profile.Tiredness >= 50) return Sleep + 500;
            return Sleep;
        }

        private int EatChance(HungerGameProfile profile)
        {
            if (profile.Hunger >= 20 && profile.Inventory.Food.Any()) return Eat + 1000;
            else if (profile.Hunger >= 50 && profile.Inventory.Food.Any()) return Eat + 700;
            else if (profile.Hunger >= 75 && profile.Inventory.Food.Any()) return Eat + 400;
            else if (profile.Hunger >= 90 && profile.Inventory.Food.Any()) return Eat + 200;
            else return Eat;
        }

        private int IdleChance(HungerGameProfile profile) => Idle;

        private int MeetChance(HungerGameProfile profile) => Meet;

        private int HackChance(HungerGameProfile profile) => Hack;

        private int DieChance(HungerGameProfile profile) => Die;
    }
}