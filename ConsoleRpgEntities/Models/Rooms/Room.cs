using System.ComponentModel.DataAnnotations.Schema;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;

namespace ConsoleRpgEntities.Models.Rooms
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("NorthId")]
        public virtual Room? North { get; set; }
        public int? NorthId { get; set; }

        [ForeignKey("SouthId")]
        public virtual Room? South { get; set; }
        public int? SouthId { get; set; }

        [ForeignKey("EastId")]
        public virtual Room? East { get; set; }
        public int? EastId { get; set; }

        [ForeignKey("WestId")]
        public virtual Room? West { get; set; }
        public int? WestId { get; set; }

        public virtual int? PlayerId { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Monster> Monsters { get; set; } // Add this property

        // Parameterless constructor for Entity Framework
        public Room()
        {
            Players = new List<Player>();
            Monsters = new List<Monster>(); // Initialize the collection
        }

        public Room(string name, string description)
        {
            Name = name;
            Description = description;
            Players = new List<Player>();
            Monsters = new List<Monster>(); // Initialize the collection
        }

        public void Enter()
        {
            Console.WriteLine($"You have entered {Name}. {Description}");
            foreach (var player in Players)
            {
                Console.WriteLine($"{player.Name} is here.");
            }
        }

        // The following methods exist to add and remove players from the room (e.g. if they are defeated)
        public void AddPlayer(Player player)
        {
            Players.Add(player);
            Console.WriteLine($"INFO: {player.Name} has been added to room {Name}");
        }

        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
            Console.WriteLine($"INFO: {player.Name} has been removed from room {Name}.");
        }
    }
}
