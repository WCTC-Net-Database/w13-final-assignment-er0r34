using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;

namespace ConsoleRpgEntities.Models.Characters;

public interface IPlayer
{
    int Id { get; set; }
    string Name { get; set; }
    int Experience { get; set; }
    int Health { get; set; }
    int Attack { get; set; }
    int Defense { get; set; }
    Inventory Inventory { get; set; }
    Equipment Equipment { get; set; }
    Room Room { get; set; }
    int? RoomId { get; set; }
    ICollection<Ability> Abilities { get; set; }

    void AssignAbility(Ability ability); // Added
    void RemoveAbility(Ability ability); // Added
}


