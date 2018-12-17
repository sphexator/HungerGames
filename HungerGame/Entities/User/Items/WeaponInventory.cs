using System;
using System.Collections.Generic;
using System.Text;
using HungerGame.Entities.Items;

namespace HungerGame.Entities.User.Items
{
    public class WeaponInventory
    {
        public Weapon Weapon { get; set; }
        public int Amount { get; set; }
    }
}
