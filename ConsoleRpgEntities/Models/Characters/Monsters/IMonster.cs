using ConsoleRpgEntities.Models.Attributes;

namespace ConsoleRpgEntities.Models.Characters.Monsters;

public interface IMonster : ITargetable
{
    int Id { get; set; }
    string Name { get; set; }
    int Attack { get; set; }
}

