using HungerGame.Entities.Items;
using HungerGame.Entities.User;

namespace HungerGame.Entities.Internal.Events
{
    internal class Hack : IRequired
    {
        internal string HackEvent(HungerGameProfile profile, ItemDrop items)
        {
            foreach (var x in items.Weapons)
            {

            }
            foreach (var x in items.FirstAids)
            {

            }
            foreach (var x in items.Drinks)
            {

            }
            foreach (var x in items.Foods)
            {

            }
            return "Hacked the system, obtaining every single item";
        }
    }
}