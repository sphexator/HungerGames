using System;
using System.Linq;
using HungerGame.Entities.Items;
using HungerGame.Entities.User;
using HungerGame.Entities.User.Items;

namespace HungerGame.Entities.Internal.Events
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

        internal string LootEvent(HungerGameProfile profile, ItemDrop items)
        {
            const int pool = FoodAndWater + Weapons + Bandages;
            var result = _random.Next(pool);
            if (result <= FoodAndWater)
            {
                var toGive = IsFood();
                Drink drink;
                Food food;
                switch (toGive)
                {
                    case 1:
                        drink = items.Drinks[_random.Next(items.Drinks.Count)];
                        var drinkCheck = profile.Inventory.Drinks.FirstOrDefault(x => x.Drink == drink);
                        
                        if (drinkCheck == null) profile.Inventory.Drinks.Add(new DrinkInventory { Amount = 1, Drink = drink });
                        else drinkCheck.Amount += 1;
                        
                        return $"Looted {drink.Name}";
                    case 2:
                        food = items.Foods[_random.Next(items.Foods.Count)];
                        var foodCheck = profile.Inventory.Food.FirstOrDefault(x => x.Food == food);
                        
                        if (foodCheck == null) profile.Inventory.Food.Add(new FoodInventory { Amount = 1, Food = food });
                        else foodCheck.Amount += 1;

                        return $"Looted {food.Name}";
                    case 3:
                        drink = items.Drinks[_random.Next(items.Drinks.Count)];
                        food = items.Foods[_random.Next(items.Foods.Count)];
                        var foodCheckTwo = profile.Inventory.Food.FirstOrDefault(x => x.Food == food);
                        var drinkCheckTwo = profile.Inventory.Drinks.FirstOrDefault(x => x.Drink == drink);
                        
                        if (foodCheckTwo == null) profile.Inventory.Food.Add(new FoodInventory { Amount = 1, Food = food });
                        else foodCheckTwo.Amount += 1;
                        if (drinkCheckTwo == null) profile.Inventory.Drinks.Add(new DrinkInventory { Amount = 1, Drink = drink });
                        else drinkCheckTwo.Amount += 1;

                        return $"Looted {food.Name} and {drink.Name}";
                    default:
                        return null;
                }
            }
            if (result <= FoodAndWater + Bandages)
            {
                var firstAid = items.FirstAids[_random.Next(items.FirstAids.Count)];
                var firstAidCheck = profile.Inventory.FirstAid.FirstOrDefault(x => x.FirstAid == firstAid);
                if (firstAidCheck == null)
                    profile.Inventory.FirstAid.Add(new FirstAidInventory {Amount = 1, FirstAid = firstAid});
                else firstAidCheck.Amount += 1;
                return $"Looted {firstAid.Name}";
            }
            string response;
            var weapon = items.Weapons[_random.Next(items.Weapons.Count)];
            var weaponCheck = profile.Inventory.Weapons.FirstOrDefault(x => x.Weapon == weapon);
            if (weaponCheck != null)
            {
                if (weaponCheck.Weapon.Ammo != null)
                {
                    weaponCheck.Weapon.Ammo += 10;
                    response = $"Found 10 ammo to {weaponCheck.Weapon.Name}";
                }
                else
                {
                    weaponCheck.Amount += 1;
                    response = $"Found and picked up {weaponCheck.Weapon.Name}";
                }
            }
            else
            {
                profile.Inventory.Weapons.Add(new WeaponInventory {Amount = 1, Weapon = weapon});
                response = $"Found and picked up {weapon.Name}";
            }

            return response;
        }

        private int IsFood() => _random.Next(1, 3);
    }
}