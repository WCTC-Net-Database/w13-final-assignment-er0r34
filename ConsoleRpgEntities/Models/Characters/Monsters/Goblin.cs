namespace ConsoleRpgEntities.Models.Characters.Monsters
{
    public class Goblin : Monster
    {
        public int Sneakiness { get; set; }
        public int RoomId { get; set; }

        public Goblin()
        {
            Name = "Goblin";
            Health = 20; // Example health value
            Attack = 10; // Base attack value
            Defense = 5; // Example defense value
        }

    }
}
