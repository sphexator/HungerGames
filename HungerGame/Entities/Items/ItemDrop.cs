using System.Collections.Generic;

namespace HungerGame.Entities
{
    public class ItemDrop
    {
        public IEnumerable<Drink> Drinks { get; set; }
        public IEnumerable<Food> Foods { get; set; }
        public IEnumerable<FirstAid> FirstAids { get; set; }
        public IEnumerable<Weapon> Weapons { get; set; }
    }
}