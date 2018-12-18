using System;
using HungerGame.Entities.User;

namespace HungerGame.Entities.Internal.Events
{
    internal class Consume : IRequired
    {
        private readonly Random _random;

        internal Consume(Random random) => _random = random;

        internal string EatEvent(HungerGameProfile profile) =>
            profile.Thirst > profile.Hunger ? Drink(profile) : Eat(profile);

        private string Drink(HungerGameProfile profile)
        {
            if (profile.Inventory.Drinks.Count == 0) return null;

            var drink = profile.Inventory.Drinks[_random.Next(profile.Inventory.Drinks.Count)];
            profile.Hunger += drink.Drink.HydrateAmount;
            profile.Stamina += drink.Drink.HydrateAmount;
            if (drink.Amount == 1)
                profile.Inventory.Drinks.Remove(drink);
            else drink.Amount -= 1;

            return $"Drank {drink.Drink.Name}";
        }

        private string Eat(HungerGameProfile profile)
        {
            if (profile.Inventory.Food.Count == 0) return null;

            var food = profile.Inventory.Food[_random.Next(profile.Inventory.Food.Count)];
            profile.Hunger += food.Food.HungerGain;
            profile.Stamina += food.Food.HungerGain;
            if (food.Amount == 1)
                profile.Inventory.Food.Remove(food);
            else food.Amount -= 1;

            return $"Ate {food.Food.Name}";
        }
    }
}