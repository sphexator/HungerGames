using System;
using HungerGame.Entities;

namespace HungerGame.Events.Types
{
    internal class Loot : IRequired
    {
        private readonly Random _random;
        internal Loot(Random random)
        {
            _random = random;
        }
        private const int FoodAndWater = 100;
        private const int Weapons = 15;
        private const int Bandages = 50;

        internal string LootEvent(HungerGameProfile profile)
        {
            const int pool = FoodAndWater + Weapons + Bandages;
            var result = _random.Next(1, pool);
            if (result <= FoodAndWater)
            {
                var toGive = IsFood();
                switch (toGive)
                {
                    case 1:

                        profile.Inventory.Drinks = profile.Inventory.Drinks + 1;
                        return "Obtained Water";
                    case 2:
                        user.Food = user.Food + 1;
                        return "Obtained Food";
                    case 3:
                        profile.Inventory.Drinks = profile.Inventory.Drinks + 1;
                        user.Food = user.Food + 1;
                        return "Obtained Water and Food";
                    default:
                        return null;
                }
            }

            if (result <= FoodAndWater + Bandages) return $"Obtained {ConsumableNames.Bandages}";

            if (result > FoodAndWater + Bandages + Weapons) return WeaponNames.WeaponStrings[1];
            var weapon = _random.Next(0, 100);
            if (weapon <= 50)
            {
                user.Bow = user.Bow + 1;
                return "Obtained bow";
                //Add Bow
            }

            if (weapon <= 50 + 30)
            {
                user.Axe = user.Axe + 1;
                return "Obtained axe";
                //Add Axe
            }

            if (weapon <= 50 + 30 + 15)
            {
                user.Pistol = user.Pistol + 1;
                return "Obtained pistol";
                //Add Pistol
            }

            if (weapon <= 50 + 30 + 15 + 15) return $"Obtained {WeaponNames.WeaponStrings[4]}";

            user.Bow = user.Bow + 1;
            return "Obtained bow";
        }

        private int IsFood() => _random.Next(1, 3);
    }
}