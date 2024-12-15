using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;

public class Player : IPlayer, ITargetable
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Experience { get; set; }
    public int Health { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    // Foreign key
    public int? EquipmentId { get; set; }
    // Navigation properties
    public virtual Inventory Inventory { get; set; }
    public virtual Equipment Equipment { get; set; }
    public virtual Room Room { get; set; }
    public virtual int? RoomId { get; set; }
    public virtual ICollection<Ability> Abilities { get; set; }
    public virtual ICollection<Item> InventoryItems { get; set; } = new List<Item>();

    // Public or protected constructor
    public Player()
    {
        Abilities = new List<Ability>();
        InventoryItems = new List<Item>();
    }

    // Method to assign an ability to the player
    public void AssignAbility(Ability ability)
    {
        Abilities.Add(ability);
    }

    // Method to remove an ability from the player
    public void RemoveAbility(Ability ability)
    {
        Abilities.Remove(ability);
    }

    // Inventory methods
    public void AddItem(Item item)
    {
        InventoryItems.Add(item);
    }

    public void UseItem(string itemName)
    {
        var item = InventoryItems.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (item != null)
        {
            // Implement item usage logic here
            Console.WriteLine($"Used item: {item.Name}");
            InventoryItems.Remove(item);
        }
        else
        {
            Console.WriteLine($"Item '{itemName}' not found in inventory.");
        }
    }

    public void EquipItem(string itemName)
    {
        var item = InventoryItems.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (item != null)
        {
            if (item.Type == "Weapon")
            {
                Equipment.Weapon = item;
            }
            else if (item.Type == "Armor")
            {
                Equipment.Armor = item;
            }
            Console.WriteLine($"Equipped item: {item.Name}");
        }
        else
        {
            Console.WriteLine($"Item '{itemName}' not found in inventory.");
        }
    }
    public void RemoveItem(string itemName)
    {
        var item = InventoryItems.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (item != null)
        {
            InventoryItems.Remove(item);
            Console.WriteLine($"Removed item: {item.Name}");
        }
        else
        {
            Console.WriteLine($"Item '{itemName}' not found in inventory.");
        }
    }
    public void ListAvailableItemTypes()
    {
        var itemTypes = InventoryItems.Select(i => i.Type).Distinct().ToList();
        Console.WriteLine("Available item types:");
        foreach (var type in itemTypes)
        {
            Console.WriteLine(type);
        }
    }
}

