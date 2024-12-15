using ConsoleRpg.Helpers;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using Spectre.Console;
using Microsoft.EntityFrameworkCore;
using ConsoleRpgEntities.Models.Abilities.PlayerAbilities;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;

namespace ConsoleRpg.Services;

public class GameEngine
{
    private readonly GameContext _context;
    private readonly MenuManager _menuManager;
    private readonly MapManager _mapManager;
    private readonly PlayerService _playerService;
    private readonly OutputManager _outputManager;
    private Table _logTable;
    private Panel _mapPanel;

    private Player _player;
    private IMonster _goblin;

    public GameEngine(GameContext context, MenuManager menuManager, MapManager mapManager, PlayerService playerService, OutputManager outputManager)
    {
        _menuManager = menuManager;
        _mapManager = mapManager;
        _playerService = playerService;
        _outputManager = outputManager;
        _context = context;
    }

    public void Run()
    {
        if (_menuManager.ShowMainMenu())
        {
            SetupGame();
        }
    }

    private void GameLoop()
    {
        while (true)
        {
            _outputManager.AddLogEntry("1. Navigate Rooms");
            _outputManager.AddLogEntry("2. Character Management");
            _outputManager.AddLogEntry("3. Room Management");
            _outputManager.AddLogEntry("4. Inventory Management");
            _outputManager.AddLogEntry("5. Quit");
            var input = _outputManager.GetUserInput("Choose an action:");

            switch (input)
            {
                case "1":
                    NavigateRooms();
                    break;
                case "2":
                    CharacterManagementMenu();
                    break;
                case "3":
                    RoomManagementMenu();
                    break;
                case "4":
                    ShowInventoryMenu();
                    break;
                case "5":
                    _outputManager.AddLogEntry("Exiting game...");
                    Environment.Exit(0);
                    break;
                default:
                    _outputManager.AddLogEntry("Invalid selection. Please choose a valid option.");
                    break;
            }
        }
    }
    private void CharacterManagementMenu()
    {
        while (true)
        {
            _outputManager.AddLogEntry("1. Add Character");
            _outputManager.AddLogEntry("2. Find Character");
            _outputManager.AddLogEntry("3. Update Character Attributes");
            _outputManager.AddLogEntry("4. Display All Characters");
            _outputManager.AddLogEntry("5. Create Ability");
            _outputManager.AddLogEntry("6. Assign Ability to Character");
            _outputManager.AddLogEntry("7. Display Character Abilities");
            _outputManager.AddLogEntry("8. Add Equipment");
            _outputManager.AddLogEntry("9. Back to Main Menu");
            var input = _outputManager.GetUserInput("Choose an action:");

            switch (input)
            {
                case "1":
                    AddCharacter();
                    break;
                case "2":
                    FindCharacter();
                    break;
                case "3":
                    UpdateCharacterAttributes();
                    break;
                case "4":
                    DisplayAllCharacters();
                    break;
                case "5":
                    CreateAbility();
                    break;
                case "6":
                    AssignAbilityToCharacter();
                    break;
                case "7":
                    DisplayCharacterAbilities();
                    break;
                case "8":
                    AddEquipment();
                    break;
                case "9":
                    return;
                default:
                    _outputManager.AddLogEntry("Invalid selection. Please choose a valid option.");
                    break;
            }
        }
    }

    private void RoomManagementMenu()
    {
        while (true)
        {
            _outputManager.ClearLogEntries(); // Clear previous log entries
            _outputManager.AddLogEntry("1. Add Room");
            _outputManager.AddLogEntry("2. Display Room Details");
            _outputManager.AddLogEntry("3. Display All Rooms");
            _outputManager.AddLogEntry("4. Modify Room Connections");
            _outputManager.AddLogEntry("5. List Characters in Room by Attribute");
            _outputManager.AddLogEntry("6. List All Rooms with Characters");
            _outputManager.AddLogEntry("7. Find Equipment and Location");
            _outputManager.AddLogEntry("8. Back to Main Menu");
            var input = _outputManager.GetUserInput("Choose an action:");

            switch (input)
            {
                case "1":
                    AddRoom();
                    break;
                case "2":
                    DisplayRoomDetails();
                    break;
                case "3":
                    DisplayAllRooms();
                    break;
                case "4":
                    ModifyRoomConnections();
                    break;
                case "5":
                    ListCharactersInRoomByAttribute();
                    break;
                case "6":
                    ListAllRoomsWithCharacters();
                    break;
                case "7":
                    FindEquipmentAndLocation();
                    break;
                case "8":
                    return;
                default:
                    _outputManager.AddLogEntry("Invalid selection. Please choose a valid option.");
                    break;
            }
        }
    }

    public void ShowInventoryMenu()
    {
        while (true)
        {
            _outputManager.AddLogEntry("Inventory Management:");
            _outputManager.AddLogEntry("1. Search for item by name");
            _outputManager.AddLogEntry("2. List items by type");
            _outputManager.AddLogEntry("3. Sort items");
            _outputManager.AddLogEntry("4. Equip item");
            _outputManager.AddLogEntry("5. Use item");
            _outputManager.AddLogEntry("6. Remove item");
            _outputManager.AddLogEntry("7. Back to Main Menu");
            var input = _outputManager.GetUserInput("Choose an action:");

            switch (input)
            {
                case "1":
                    SearchItemByName();
                    break;
                case "2":
                    ListItemsByType();
                    break;
                case "3":
                    SortItems();
                    break;
                case "4":
                    EquipItem();
                    break;
                case "5":
                    UseItem();
                    break;
                case "6":
                    RemoveItem();
                    break;
                case "7":
                    return;
                default:
                    _outputManager.AddLogEntry("Invalid selection. Please choose a valid option.");
                    break;
            }
        }
    }

    private void AttackCharacter(Monster monster)
    {
        if (monster == null)
        {
            _outputManager.AddLogEntry("There are no enemies to attack in this room.");
            return;
        }

        while (monster is ITargetable targetableMonster && targetableMonster.Health > 0)
        {
            // Execute abilities before attacking
            foreach (var ability in _player.Abilities.Where(a => a.IsActive))
            {
                _playerService.UseAbility(_player, ability, targetableMonster);
                _outputManager.AddLogEntry($"{_player.Name} used ability {ability.Name}.");
            }

            _playerService.Attack(_player, targetableMonster);
            _outputManager.AddLogEntry($"{_player.Name} attacked {monster.Name}.");
            Thread.Sleep(2000); // Pause for 2 seconds

            if (targetableMonster.Health <= 0)
            {
                _outputManager.AddLogEntry($"{monster.Name} has been defeated!");
                break;
            }

            // Simulate enemy attack
            _playerService.MonsterAttack(monster, _player);
            _outputManager.AddLogEntry($"{monster.Name} attacked {_player.Name}.");
            Thread.Sleep(2000); // Pause for 2 seconds

            if (_player.Health <= 0)
            {
                _outputManager.AddLogEntry($"{_player.Name} has been defeated!");
                Environment.Exit(0);
            }
        }

        // Remove the monster from the room and the database
        var currentRoom = _context.Rooms.Include(r => r.Monsters).FirstOrDefault(r => r.Id == monster.RoomId);
        if (currentRoom != null)
        {
            currentRoom.Monsters.Remove(monster);
            _context.Monsters.Remove(monster);
            _context.SaveChanges();
            _outputManager.AddLogEntry($"{monster.Name} has been removed from the room.");
        }
    }

    private void SetupGame()
    {
        _outputManager.AddLogEntry("1. Select Existing Character");
        _outputManager.AddLogEntry("2. Create New Character");
        var input = _outputManager.GetUserInput("Choose an option:");

        switch (input)
        {
            case "1":
                SelectCharacter();
                break;
            case "2":
                AddCharacter();
                break;
            default:
                _outputManager.AddLogEntry("Invalid selection. Please choose a valid option.");
                SetupGame();
                return;
        }

        _outputManager.AddLogEntry($"{_player.Name} has entered the game.");

        // Assign player to a random room
        AssignPlayerToRandomRoom();

        // Load monsters into random rooms
        LoadMonsters();

        // Load map
        _mapManager.LoadInitialRoom(_player.RoomId ?? 1);
        _mapManager.DisplayMap();

        // Pause before starting the game loop
        Thread.Sleep(500);
        GameLoop();
    }
    private void SelectCharacter()
    {
        var players = _context.Players.ToList();

        if (!players.Any())
        {
            _outputManager.AddLogEntry("No characters found. Please create a new character.");
            AddCharacter();
            return;
        }

        _outputManager.AddLogEntry("Select a character:");
        for (int i = 0; i < players.Count; i++)
        {
            _outputManager.AddLogEntry($"{i + 1}. {players[i].Name}");
        }

        var input = _outputManager.GetUserInput("Choose a character:");

        if (int.TryParse(input, out var selectedIndex) && selectedIndex > 0 && selectedIndex <= players.Count)
        {
            _player = players[selectedIndex - 1];
        }
        else
        {
            _outputManager.AddLogEntry("Invalid selection. Please choose a valid option.");
            SelectCharacter();
        }
    }

    private void AssignPlayerToRandomRoom()
    {
        var rooms = _context.Rooms.ToList();
        if (rooms.Any())
        {
            var random = new Random();
            var randomRoom = rooms[random.Next(rooms.Count)];
            _player.RoomId = randomRoom.Id;
            _context.SaveChanges();
            _outputManager.AddLogEntry($"Player {_player.Name} has been assigned to room {randomRoom.Name}.");
        }
        else
        {
            _outputManager.AddLogEntry("No rooms available to assign the player.");
        }
    }

    private void LoadMonsters()
    {
        var random = new Random();
        int numberOfMonsters = 5; // Load exactly 5 goblins

        var rooms = _context.Rooms.ToList();
        var assignedRooms = new HashSet<int>();

        for (int i = 0; i < numberOfMonsters; i++)
        {
            int randomRoomIndex;
            do
            {
                randomRoomIndex = random.Next(rooms.Count);
            } while (assignedRooms.Contains(rooms[randomRoomIndex].Id));

            var randomRoom = rooms[randomRoomIndex];
            assignedRooms.Add(randomRoom.Id);

            var goblin = new Goblin
            {
                Name = "Goblin",
                Health = 20,
                Attack = 10,
                RoomId = randomRoom.Id
            };
            _context.Monsters.Add(goblin);

            // Log the monster's location
            _outputManager.AddLogEntry($"Goblin added to room '{randomRoom.Name}'.");

            // Add the goblin to the room's Monsters collection
            randomRoom.Monsters.Add(goblin);
        }
        _context.SaveChanges();

        // Wait for 3 seconds after the last monster is loaded
        Thread.Sleep(3000);
    }


    public void AddCharacter()
    {
        var name = _outputManager.GetUserInput("Enter character name:");
        if (string.IsNullOrEmpty(name))
        {
            _outputManager.AddLogEntry("Character name cannot be empty.");
            return;
        }

        var healthInput = _outputManager.GetUserInput("Enter character health:");
        if (!int.TryParse(healthInput, out var health))
        {
            _outputManager.AddLogEntry("Invalid health. Please enter a valid number.");
            return;
        }

        var attackInput = _outputManager.GetUserInput("Enter character attack:");
        if (!int.TryParse(attackInput, out var attack))
        {
            _outputManager.AddLogEntry("Invalid attack. Please enter a valid number.");
            return;
        }

        var defenseInput = _outputManager.GetUserInput("Enter character defense:");
        if (!int.TryParse(defenseInput, out var defense))
        {
            _outputManager.AddLogEntry("Invalid defense. Please enter a valid number.");
            return;
        }

        var weaponName = _outputManager.GetUserInput("Enter weapon name:");
        if (string.IsNullOrEmpty(weaponName))
        {
            _outputManager.AddLogEntry("Weapon name cannot be empty.");
            return;
        }

        var weaponAttackInput = _outputManager.GetUserInput("Enter weapon attack:");
        if (!int.TryParse(weaponAttackInput, out var weaponAttack))
        {
            _outputManager.AddLogEntry("Invalid weapon attack. Please enter a valid number.");
            return;
        }

        var weaponDefenseInput = _outputManager.GetUserInput("Enter weapon defense:");
        if (!int.TryParse(weaponDefenseInput, out var weaponDefense))
        {
            _outputManager.AddLogEntry("Invalid weapon defense. Please enter a valid number.");
            return;
        }

        var weapon = new Item
        {
            Name = weaponName,
            Type = "Weapon",
            Attack = weaponAttack,
            Defense = weaponDefense,
            Weight = 1.0m, // Assuming a default weight
            Value = 100 // Assuming a default value
        };

        var player = new Player
        {
            Name = name,
            Health = health,
            Attack = attack,
            Defense = defense,
            Experience = 0, // Assuming new characters start with 0 experience
            RoomId = null, // Assuming new characters are not assigned to a room initially
            Inventory = new Inventory(),
            Equipment = new Equipment { Weapon = weapon },
            Abilities = new List<Ability>()
        };

        _context.Items.Add(weapon);
        _context.Players.Add(player);
        _context.SaveChanges();

        _player = player;

        _outputManager.AddLogEntry($"Character '{name}' added with Health: {health}, Attack: {attack}, Defense: {defense}, and equipped with weapon '{weaponName}' (Attack: {weaponAttack}, Defense: {weaponDefense}).");
    }

    public void FindCharacter()
    {
        var name = _outputManager.GetUserInput("Enter character name to search:");

        var player = _context.Players
                             .Include(p => p.Room)
                             .FirstOrDefault(p => p.Name.ToLower() == name.ToLower());

        if (player != null)
        {
            _outputManager.AddLogEntry($"Character found: ID: {player.Id}, Name: {player.Name}, Health: {player.Health}, Experience: {player.Experience}");
        }
        else
        {
            _outputManager.AddLogEntry($"Character with name '{name}' not found.");
        }
    }

    public void UpdateCharacterAttributes()
    {
        var name = _outputManager.GetUserInput("Enter character name to update:");

        var player = _context.Players.Include(p => p.Equipment).FirstOrDefault(p => p.Name.ToLower() == name.ToLower());

        if (player != null)
        {
            var newHealthInput = _outputManager.GetUserInput($"Current Health: {player.Health}. Enter new health:");
            if (int.TryParse(newHealthInput, out var newHealth))
            {
                player.Health = newHealth;
                _context.SaveChanges();
                _outputManager.AddLogEntry($"Character '{player.Name}' health updated to {newHealth}.");
            }
            else
            {
                _outputManager.AddLogEntry("Invalid health. Please enter a valid number.");
            }

            var newAttackInput = _outputManager.GetUserInput("Enter new attack:");
            if (int.TryParse(newAttackInput, out var newAttack))
            {
                player.Attack = newAttack;
                _context.SaveChanges();
                _outputManager.AddLogEntry($"Character '{player.Name}' attack updated to {newAttack}.");
            }
            else
            {
                _outputManager.AddLogEntry("Invalid attack. Please enter a valid number.");
            }

            var newDefenseInput = _outputManager.GetUserInput("Enter new defense:");
            if (int.TryParse(newDefenseInput, out var newDefense))
            {
                player.Defense = newDefense;
                _context.SaveChanges();
                _outputManager.AddLogEntry($"Character '{player.Name}' defense updated to {newDefense}.");
            }
            else
            {
                _outputManager.AddLogEntry("Invalid defense. Please enter a valid number.");
            }

            if (player.Equipment.Weapon != null)
            {
                var changeWeaponInput = _outputManager.GetUserInput($"Current Weapon: {player.Equipment.Weapon.Name}. Do you want to change the weapon? (yes/no):");
                if (changeWeaponInput.ToLower() == "yes")
                {
                    UpdateWeapon(player);
                }
            }
            else
            {
                var addWeaponInput = _outputManager.GetUserInput("No weapon equipped. Do you want to add a weapon? (yes/no):");
                if (addWeaponInput.ToLower() == "yes")
                {
                    UpdateWeapon(player);
                }
            }
        }
        else
        {
            _outputManager.AddLogEntry($"Character with name '{name}' not found.");
        }
    }

    private void UpdateWeapon(Player player)
    {
        var weaponName = _outputManager.GetUserInput("Enter weapon name:");
        if (string.IsNullOrEmpty(weaponName))
        {
            _outputManager.AddLogEntry("Weapon name cannot be empty.");
            return;
        }

        var weaponAttackInput = _outputManager.GetUserInput("Enter weapon attack:");
        if (!int.TryParse(weaponAttackInput, out var weaponAttack))
        {
            _outputManager.AddLogEntry("Invalid weapon attack. Please enter a valid number.");
            return;
        }

        var weaponDefenseInput = _outputManager.GetUserInput("Enter weapon defense:");
        if (!int.TryParse(weaponDefenseInput, out var weaponDefense))
        {
            _outputManager.AddLogEntry("Invalid weapon defense. Please enter a valid number.");
            return;
        }

        var weapon = new Item
        {
            Name = weaponName,
            Type = "Weapon",
            Attack = weaponAttack,
            Defense = weaponDefense,
            Weight = 1.0m, // Assuming a default weight
            Value = 100 // Assuming a default value
        };

        _context.Items.Add(weapon);
        player.Equipment.Weapon = weapon;
        _context.SaveChanges();

        _outputManager.AddLogEntry($"Weapon '{weaponName}' (Attack: {weaponAttack}, Defense: {weaponDefense}) has been equipped to character '{player.Name}'.");
    }

    public void DisplayAllCharacters()
    {
        var players = _context.Players.Include(p => p.Room).ToList();

        if (players.Any())
        {
            foreach (var player in players)
            {
                _outputManager.AddLogEntry($"ID: {player.Id}, Name: {player.Name}, Health: {player.Health}, Attack: {player.Attack}, Defense: {player.Defense}, Experience: {player.Experience}, Room: {player.Room?.Name ?? "None"}");
            }
        }
        else
        {
            _outputManager.AddLogEntry("No characters found.");
        }
    }

    public void AssignAbilityToCharacter()
    {
        var playerName = _outputManager.GetUserInput("Enter character name to assign ability:");

        var player = _context.Players.Include(p => p.Abilities).FirstOrDefault(p => p.Name.ToLower() == playerName.ToLower());

        if (player == null)
        {
            _outputManager.AddLogEntry($"Character with name '{playerName}' not found.");
            return;
        }

        var abilityName = _outputManager.GetUserInput("Enter ability name:");

        var ability = _context.Abilities.FirstOrDefault(a => a.Name.ToLower() == abilityName.ToLower());

        if (ability == null)
        {
            _outputManager.AddLogEntry($"Ability with name '{abilityName}' not found.");
            return;
        }

        player.AssignAbility(ability);
        _context.SaveChanges();

        _outputManager.AddLogEntry($"Ability '{ability.Name}' assigned to character '{player.Name}'.");
    }

    public void DisplayCharacterAbilities()
    {
        var playerName = _outputManager.GetUserInput("Enter character name to display abilities:");

        var player = _context.Players.Include(p => p.Abilities).FirstOrDefault(p => p.Name.ToLower() == playerName.ToLower());

        if (player == null)
        {
            _outputManager.AddLogEntry($"Character with name '{playerName}' not found.");
            return;
        }

        if (player.Abilities.Any())
        {
            _outputManager.AddLogEntry($"Abilities for {player.Name}:");
            foreach (var ability in player.Abilities)
            {
                _outputManager.AddLogEntry($"Name: {ability.Name}, Attack Bonus: {ability.AttackBonus}, Defense Bonus: {ability.DefenseBonus}");
            }
        }
        else
        {
            _outputManager.AddLogEntry($"Character '{player.Name}' has no abilities.");
        }
    }

    public void AddRoom()
    {
        var name = _outputManager.GetUserInput("Enter room name:");
        if (string.IsNullOrEmpty(name))
        {
            _outputManager.AddLogEntry("Room name cannot be empty.");
            return;
        }

        var description = _outputManager.GetUserInput("Enter room description:");

        int? northId = GetRoomConnection("north");
        int? southId = GetRoomConnection("south");
        int? eastId = GetRoomConnection("east");
        int? westId = GetRoomConnection("west");

        var room = new Room(name, description)
        {
            NorthId = northId,
            SouthId = southId,
            EastId = eastId,
            WestId = westId
        };

        _context.Rooms.Add(room);
        _context.SaveChanges();

        _outputManager.AddLogEntry($"Room '{name}' added with Description: {description}.");
    }
    private int? GetRoomConnection(string direction)
    {
        var input = _outputManager.GetUserInput($"Do you want to attach a room to the {direction}? (yes/no):");
        if (input.ToLower() == "yes")
        {
            var roomIdInput = _outputManager.GetUserInput($"Enter the ID of the room to the {direction}:");
            if (int.TryParse(roomIdInput, out var roomId))
            {
                var room = _context.Rooms.FirstOrDefault(r => r.Id == roomId);
                if (room != null)
                {
                    _outputManager.AddLogEntry($"Room ID {roomId} is valid and will be attached to the {direction}.");
                    return roomId;
                }
                else
                {
                    _outputManager.AddLogEntry($"Room ID {roomId} is not valid. No room will be attached to the {direction}.");
                }
            }
            else
            {
                _outputManager.AddLogEntry("Invalid room ID. No room will be attached.");
            }
        }
        return null;
    }




    public void DisplayRoomDetails()
    {
        var roomName = _outputManager.GetUserInput("Enter room name to display details:");

        var room = _context.Rooms.Include(r => r.Players).FirstOrDefault(r => r.Name.ToLower() == roomName.ToLower());

        if (room == null)
        {
            _outputManager.AddLogEntry($"Room with name '{roomName}' not found.");
            return;
        }

        _outputManager.AddLogEntry($"Room Details for {room.Name}:");
        _outputManager.AddLogEntry($"Description: {room.Description}");
        if (room.Players.Any())
        {
            _outputManager.AddLogEntry("Inhabitants:");
            foreach (var player in room.Players)
            {
                _outputManager.AddLogEntry($"- {player.Name}");
            }
        }
        else
        {
            _outputManager.AddLogEntry("No inhabitants in this room.");
        }
    }

    public void NavigateRooms()
    {
        var currentRoom = _context.Rooms.Include(r => r.Players).Include(r => r.Monsters).FirstOrDefault(r => r.Id == _player.RoomId);

        while (true)
        {
            if (currentRoom == null)
            {
                _outputManager.AddLogEntry("You are not in any room.");
                return;
            }

            // Display the room description when entering
            _outputManager.AddLogEntry($"You have entered {currentRoom.Name}. {currentRoom.Description}");

            // Check for monsters in the room
            var monstersInRoom = currentRoom.Monsters.ToList();
            if (monstersInRoom.Any())
            {
                _outputManager.AddLogEntry("Monsters in the room:");
                foreach (var monster in monstersInRoom)
                {
                    _outputManager.AddLogEntry($"- {monster.Name} (Health: {monster.Health}, Attack: {monster.Attack})");
                }
            }
            else
            {
                _outputManager.AddLogEntry("There are no monsters in this room.");
            }

            // List available directions and actions
            var availableDirections = new List<string>();
            if (currentRoom.NorthId.HasValue) availableDirections.Add("north");
            if (currentRoom.SouthId.HasValue) availableDirections.Add("south");
            if (currentRoom.EastId.HasValue) availableDirections.Add("east");
            if (currentRoom.WestId.HasValue) availableDirections.Add("west");

            var actions = new List<string>(availableDirections);
            if (monstersInRoom.Any()) actions.Add("attack");
            actions.Add("exit");

            var actionPrompt = "Choose an action (" + string.Join(", ", actions) + "):";
            var action = _outputManager.GetUserInput(actionPrompt);

            switch (action.ToLower())
            {
                case "north" when currentRoom.NorthId.HasValue:
                    currentRoom = _context.Rooms.Include(r => r.Players).Include(r => r.Monsters).FirstOrDefault(r => r.Id == currentRoom.NorthId);
                    break;
                case "south" when currentRoom.SouthId.HasValue:
                    currentRoom = _context.Rooms.Include(r => r.Players).Include(r => r.Monsters).FirstOrDefault(r => r.Id == currentRoom.SouthId);
                    break;
                case "east" when currentRoom.EastId.HasValue:
                    currentRoom = _context.Rooms.Include(r => r.Players).Include(r => r.Monsters).FirstOrDefault(r => r.Id == currentRoom.EastId);
                    break;
                case "west" when currentRoom.WestId.HasValue:
                    currentRoom = _context.Rooms.Include(r => r.Players).Include(r => r.Monsters).FirstOrDefault(r => r.Id == currentRoom.WestId);
                    break;
                case "attack" when monstersInRoom.Any():
                    AttackCharacter(monstersInRoom.First());
                    break;
                case "exit":
                    return;
                default:
                    _outputManager.AddLogEntry("Invalid action. Please choose a valid option.");
                    break;
            }

            if (currentRoom != null)
            {
                _player.RoomId = currentRoom.Id;
                _context.SaveChanges();
                _mapManager.UpdateCurrentRoom(currentRoom.Id); // Update the map
                _mapManager.DisplayMap(); // Display the updated map
            }
        }
    }

    public void DisplayAllRooms()
    {
        var rooms = _context.Rooms.ToList();

        if (rooms.Any())
        {
            foreach (var room in rooms)
            {
                _outputManager.AddLogEntry($"ID: {room.Id}, Name: {room.Name}, Description: {room.Description}, North: {room.NorthId}, South: {room.SouthId}, East: {room.EastId}, West: {room.WestId}");
            }
        }
        else
        {
            _outputManager.AddLogEntry("No rooms found.");
        }

        _outputManager.AddLogEntry("Press Enter to return to the previous menu...");
        _outputManager.GetUserInput(""); // Wait for the user to press Enter
    }


    public void ModifyRoomConnections()
    {
        DisplayAllRooms();

        var roomIdInput = _outputManager.GetUserInput("Enter the ID of the room you want to modify:");
        if (!int.TryParse(roomIdInput, out var roomId))
        {
            _outputManager.AddLogEntry("Invalid room ID. Please enter a valid number.");
            return;
        }

        var room = _context.Rooms.FirstOrDefault(r => r.Id == roomId);
        if (room == null)
        {
            _outputManager.AddLogEntry($"Room with ID '{roomId}' not found.");
            return;
        }

        var northIdInput = _outputManager.GetUserInput($"Current North Room ID: {room.NorthId}. Enter new North Room ID (or leave blank to keep current):");
        if (int.TryParse(northIdInput, out var northId))
        {
            room.NorthId = northId;
        }

        var southIdInput = _outputManager.GetUserInput($"Current South Room ID: {room.SouthId}. Enter new South Room ID (or leave blank to keep current):");
        if (int.TryParse(southIdInput, out var southId))
        {
            room.SouthId = southId;
        }

        var eastIdInput = _outputManager.GetUserInput($"Current East Room ID: {room.EastId}. Enter new East Room ID (or leave blank to keep current):");
        if (int.TryParse(eastIdInput, out var eastId))
        {
            room.EastId = eastId;
        }

        var westIdInput = _outputManager.GetUserInput($"Current West Room ID: {room.WestId}. Enter new West Room ID (or leave blank to keep current):");
        if (int.TryParse(westIdInput, out var westId))
        {
            room.WestId = westId;
        }

        _context.SaveChanges();
        _outputManager.AddLogEntry($"Room '{room.Name}' connections updated.");
    }

    public void CreateAbility()
    {
        var name = _outputManager.GetUserInput("Enter ability name:");
        if (string.IsNullOrEmpty(name))
        {
            _outputManager.AddLogEntry("Ability name cannot be empty.");
            return;
        }

        var description = _outputManager.GetUserInput("Enter ability description:");
        if (string.IsNullOrEmpty(description))
        {
            _outputManager.AddLogEntry("Ability description cannot be empty.");
            return;
        }

        var typeInput = _outputManager.GetUserInput("Will this ability increase attack or defense? (attack/defense):");
        if (typeInput.ToLower() != "attack" && typeInput.ToLower() != "defense")
        {
            _outputManager.AddLogEntry("Invalid type. Please enter 'attack' or 'defense'.");
            return;
        }

        var bonusInput = _outputManager.GetUserInput($"Enter the {typeInput} bonus value:");
        if (!int.TryParse(bonusInput, out var bonus))
        {
            _outputManager.AddLogEntry("Invalid bonus value. Please enter a valid number.");
            return;
        }

        var ability = new PlayerAbility
        {
            Name = name,
            Description = description,
            AbilityType = typeInput.ToLower(),
            AttackBonus = typeInput.ToLower() == "attack" ? bonus : 0,
            DefenseBonus = typeInput.ToLower() == "defense" ? bonus : 0,
            IsActive = true
        };

        _context.Abilities.Add(ability);
        _context.SaveChanges();

        _outputManager.AddLogEntry($"Ability '{name}' created with {typeInput} bonus of {bonus}.");
    }
    public void SearchItemByName()
    {
        var itemName = _outputManager.GetUserInput("Enter item name to search:");
        var items = _player.InventoryItems.Where(i => i.Name.Contains(itemName, StringComparison.OrdinalIgnoreCase)).ToList();

        if (items.Any())
        {
            _outputManager.AddLogEntry($"Items matching '{itemName}':");
            foreach (var item in items)
            {
                _outputManager.AddLogEntry($"Name: {item.Name}, Attack: {item.Attack}, Defense: {item.Defense}");
            }
        }
        else
        {
            _outputManager.AddLogEntry($"No items found matching '{itemName}'.");
        }
    }

    public void ListItemsByType()
    {
        var groupedItems = _player.InventoryItems.GroupBy(i => i.Type).ToList();

        foreach (var group in groupedItems)
        {
            _outputManager.AddLogEntry($"Type: {group.Key}");
            foreach (var item in group)
            {
                _outputManager.AddLogEntry($"Name: {item.Name}, Attack: {item.Attack}, Defense: {item.Defense}");
            }
        }
    }

    public void SortItems()
    {
        while (true)
        {
            _outputManager.AddLogEntry("Sort Options:");
            _outputManager.AddLogEntry("1. Sort by Name");
            _outputManager.AddLogEntry("2. Sort by Attack Value");
            _outputManager.AddLogEntry("3. Sort by Defense Value");
            _outputManager.AddLogEntry("4. Back to Inventory Menu");
            var input = _outputManager.GetUserInput("Choose a sorting option:");

            switch (input)
            {
                case "1":
                    var sortedByName = _player.InventoryItems.OrderBy(i => i.Name).ToList();
                    DisplaySortedItems(sortedByName);
                    break;
                case "2":
                    var sortedByAttack = _player.InventoryItems.OrderByDescending(i => i.Attack).ToList();
                    DisplaySortedItems(sortedByAttack);
                    break;
                case "3":
                    var sortedByDefense = _player.InventoryItems.OrderByDescending(i => i.Defense).ToList();
                    DisplaySortedItems(sortedByDefense);
                    break;
                case "4":
                    return;
                default:
                    _outputManager.AddLogEntry("Invalid selection. Please choose a valid option.");
                    break;
            }
        }
    }

    private void DisplaySortedItems(List<Item> items)
    {
        _outputManager.AddLogEntry("Sorted Items:");
        foreach (var item in items)
        {
            _outputManager.AddLogEntry($"Name: {item.Name}, Attack: {item.Attack}, Defense: {item.Defense}");
        }
    }

    public void DisplayInventory()
    {
        _outputManager.AddLogEntry("Inventory:");
        foreach (var item in _player.InventoryItems)
        {
            _outputManager.AddLogEntry($"Name: {item.Name}, Type: {item.Type}, Attack: {item.Attack}, Defense: {item.Defense}");
        }
    }

    public void EquipItem()
    {
        _player.ListAvailableItemTypes();
        var itemType = _outputManager.GetUserInput("Enter item type to equip:");
        var items = _player.InventoryItems.Where(i => i.Type.Equals(itemType, StringComparison.OrdinalIgnoreCase)).ToList();

        if (items.Any())
        {
            _outputManager.AddLogEntry($"Available {itemType}s:");
            foreach (var item in items)
            {
                _outputManager.AddLogEntry($"Name: {item.Name}, Attack: {item.Attack}, Defense: {item.Defense}");
            }

            var itemName = _outputManager.GetUserInput("Enter item name to equip:");
            _player.EquipItem(itemName);
        }
        else
        {
            _outputManager.AddLogEntry($"No {itemType}s found in inventory.");
        }
    }



    public void UseItem()
    {
        var itemName = _outputManager.GetUserInput("Enter item name to use:");
        _player.UseItem(itemName);
    }

    public void RemoveItem()
    {
        var itemName = _outputManager.GetUserInput("Enter item name to remove:");
        _player.RemoveItem(itemName);
    }

    public void AddEquipment()
    {
        var name = _outputManager.GetUserInput("Enter equipment name:");
        if (string.IsNullOrEmpty(name))
        {
            _outputManager.AddLogEntry("Equipment name cannot be empty.");
            return;
        }

        var type = _outputManager.GetUserInput("Enter equipment type (Weapon/Armor):");
        if (string.IsNullOrEmpty(type) || (type.ToLower() != "weapon" && type.ToLower() != "armor"))
        {
            _outputManager.AddLogEntry("Invalid equipment type. Please enter 'Weapon' or 'Armor'.");
            return;
        }

        var attackInput = _outputManager.GetUserInput("Enter equipment attack value:");
        if (!int.TryParse(attackInput, out var attack))
        {
            _outputManager.AddLogEntry("Invalid attack value. Please enter a valid number.");
            return;
        }

        var defenseInput = _outputManager.GetUserInput("Enter equipment defense value:");
        if (!int.TryParse(defenseInput, out var defense))
        {
            _outputManager.AddLogEntry("Invalid defense value. Please enter a valid number.");
            return;
        }

        var weightInput = _outputManager.GetUserInput("Enter equipment weight:");
        if (!decimal.TryParse(weightInput, out var weight))
        {
            _outputManager.AddLogEntry("Invalid weight. Please enter a valid number.");
            return;
        }

        var valueInput = _outputManager.GetUserInput("Enter equipment value:");
        if (!int.TryParse(valueInput, out var value))
        {
            _outputManager.AddLogEntry("Invalid value. Please enter a valid number.");
            return;
        }

        var equipment = new Item
        {
            Name = name,
            Type = type,
            Attack = attack,
            Defense = defense,
            Weight = weight,
            Value = value
        };

        _context.Items.Add(equipment);
        _context.SaveChanges();

        _outputManager.AddLogEntry($"Equipment '{name}' added with Type: {type}, Attack: {attack}, Defense: {defense}, Weight: {weight}, Value: {value}.");
    }

    public void ListCharactersInRoomByAttribute()
    {
        var attribute = _outputManager.GetUserInput("Enter attribute to filter by (Health, Attack, Name):");
        var currentRoom = _context.Rooms.Include(r => r.Players).FirstOrDefault(r => r.Id == _player.RoomId);

        if (currentRoom == null)
        {
            _outputManager.AddLogEntry("You are not in any room.");
            return;
        }

        var players = currentRoom.Players.AsQueryable();

        switch (attribute.ToLower())
        {
            case "health":
                players = players.OrderBy(p => p.Health);
                break;
            case "attack":
                players = players.OrderBy(p => p.Attack);
                break;
            case "name":
                players = players.OrderBy(p => p.Name);
                break;
            default:
                _outputManager.AddLogEntry("Invalid attribute. Please enter 'Health', 'Attack', or 'Name'.");
                return;
        }

        _outputManager.AddLogEntry($"Characters in room '{currentRoom.Name}' sorted by {attribute}:");
        foreach (var player in players)
        {
            _outputManager.AddLogEntry($"Name: {player.Name}, Health: {player.Health}, Attack: {player.Attack}, Defense: {player.Defense}");
        }
    }

    public void ListAllRoomsWithCharacters()
    {
        var rooms = _context.Rooms.Include(r => r.Players).ToList();

        foreach (var room in rooms)
        {
            _outputManager.AddLogEntry($"Room: {room.Name}, Description: {room.Description}");
            if (room.Players.Any())
            {
                _outputManager.AddLogEntry("Inhabitants:");
                foreach (var player in room.Players)
                {
                    _outputManager.AddLogEntry($"- {player.Name} (Health: {player.Health}, Attack: {player.Attack}, Defense: {player.Defense})");
                }
            }
            else
            {
                _outputManager.AddLogEntry("No inhabitants in this room.");
            }
        }
    }

    public void FindEquipmentAndLocation()
    {
        var itemName = _outputManager.GetUserInput("Enter the name of the item to search for:");
        var item = _context.Items.FirstOrDefault(i => i.Name.ToLower() == itemName.ToLower());

        if (item == null)
        {
            _outputManager.AddLogEntry($"Item '{itemName}' not found.");
            return;
        }

        var equipment = _context.Equipments
            .Include(e => e.Weapon)
            .Include(e => e.Armor)
            .FirstOrDefault(e => e.WeaponId == item.Id || e.ArmorId == item.Id);

        if (equipment == null)
        {
            _outputManager.AddLogEntry($"No equipment found for item '{itemName}'.");
            return;
        }

        var player = _context.Players.Include(p => p.Room).FirstOrDefault(p => p.EquipmentId == equipment.Id);

        if (player == null)
        {
            _outputManager.AddLogEntry($"No character found holding the item '{itemName}'.");
            return;
        }

        _outputManager.AddLogEntry($"Item '{itemName}' is held by character '{player.Name}' in room '{player.Room.Name}'.");
    }


}
