using HungerGame.Entities;

namespace HungerGame.Events.Types
{
    internal class Eat : IRequired
    {
        internal string EatEvent(HungerGameProfile profile)
        {
            if (profile.Food == 0) return null;
            profile.Hunger = 0;
            profile.Thirst = 0;
            profile.Fatigue = 0;
            profile.Inventory.Food = user.Food - 1;
            return "Ate fish";
        }
    }
}