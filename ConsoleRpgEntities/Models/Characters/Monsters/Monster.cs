using System.ComponentModel.DataAnnotations.Schema;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Rooms;

namespace ConsoleRpgEntities.Models.Characters.Monsters
{
    public abstract class Monster : IMonster, ITargetable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int AggressionLevel { get; set; }
        public string MonsterType { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int RoomId { get; set; } // Add this property
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
    }
}
