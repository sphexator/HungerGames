using HungerGame.Entities.User.Items;
using System.Collections.Generic;

namespace HungerGame.Entities.User
{
    public class PlayerInventory
    {
        public List<WeaponInventory> Weapons { get; set; }
        public List<DrinkInventory> Drinks { get; set; }
        public List<FoodInventory> Food { get; set; }
        public List<FirstAidInventory> FirstAid { get; set; }
    }
}
