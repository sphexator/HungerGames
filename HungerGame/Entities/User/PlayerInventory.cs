using System.Collections.Generic;

namespace HungerGame.Entities
{
    public class PlayerInventory
    {
        public IEnumerable<Weapon> Weapons { get; set; }
        public IEnumerable<Drink> Drinks { get; set; }
        public IEnumerable<Food> Food { get; set; }
        public IEnumerable<FirstAid> FirstAid { get; set; }
    }
}
