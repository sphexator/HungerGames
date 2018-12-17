namespace HungerGame.Entities
{
    public class HungerGameProfile
    {
        public string Name { get; set; } = "Test Unit";
        public bool Alive { get; set; } = true;
        public double Health { get; set; } = 100;
        public double Stamina { get; set; } = 100;
        public double Hunger { get; set; } = 100;
        public double Thirst { get; set; } = 100;
        public double Tiredness { get; set; } = 100;
        public bool Bleeding { get; set; } = false;
        public PlayerInventory Inventory { get; set; } = new PlayerInventory();
    }
}