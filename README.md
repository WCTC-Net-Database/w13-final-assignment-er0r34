# Console RPG Game

## Overview

This project is a Console RPG game implemented in C# using .NET 8. The game allows users to manage characters, rooms, and inventory, as well as navigate through rooms and engage in combat with monsters. The game includes various features that align with different grading levels, from minimum requirements to advanced features.

## Features by Grading Level

### Minimum Requirements

#### Add a New Character to the Database
- **Feature**: Prompt the user to enter details for a new character (e.g., Name, Health, Attack, and Defense).
- **Implementation**: The `AddCharacter` method prompts the user for character details and saves the new character to the database.

#### Edit an Existing Character
- **Feature**: Allow users to update attributes like Health, Attack, and Defense.
- **Implementation**: The `UpdateCharacterAttributes` method allows users to update a character's attributes and saves the changes to the database.

#### Display All Characters
- **Feature**: Display all characters with relevant details.
- **Implementation**: The `DisplayAllCharacters` method retrieves and displays all characters from the database, including details such as Name, Health, Attack, Defense, and Experience.

#### Search for a Specific Character by Name
- **Feature**: Perform a case-insensitive search for a character by name.
- **Implementation**: The `FindCharacter` method allows users to search for a character by name and displays detailed information about the character.

#### Logging
- **Feature**: Log all user interactions, such as adding, editing, or displaying data.
- **Implementation**: The `OutputManager` class handles logging of all user interactions.

### "C" Level (405/500 points)

#### Add Abilities to a Character
- **Feature**: Allow users to add abilities to existing characters.
- **Implementation**: The `CreateAbility` and `AssignAbilityToCharacter` methods prompt for ability details and associate the ability with the character. The changes are saved to the database, and the addition is confirmed to the user.

#### Display Character Abilities
- **Feature**: Display all abilities for a selected character.
- **Implementation**: The `DisplayCharacterAbilities` method retrieves and displays all abilities associated with a character, including properties such as Name, Attack Bonus, and Defense Bonus.

#### Execute an Ability During an Attack
- **Feature**: Ensure abilities are executed during an attack and display the appropriate output.
- **Implementation**: The `AttackCharacter` method integrates ability execution into the attack process, ensuring abilities are used and their effects are displayed.

### "B" Level (445/500 points)

#### Add New Room
- **Feature**: Prompt the user to enter a room name, description, and other needed properties.
- **Implementation**: The `AddRoom` method prompts for room details, optionally adds a character or player to the room, saves the room to the database, and confirms the addition to the user.

#### Display Details of a Room
- **Feature**: Display all associated properties of the room.
- **Implementation**: The `DisplayRoomDetails` method retrieves and displays all properties of a room, including a list of any inhabitants.

#### Navigate the Rooms
- **Feature**: Allow the character to navigate through the rooms and display room details upon entering.
- **Implementation**: The `NavigateRooms` method allows the character to move between rooms and displays room details such as name, description, inhabitants, and special features.

### "A" Level (475/500 points)

#### List Characters in the Room by Selected Attribute
- **Feature**: Allow users to find the character in the room matching criteria (e.g., Health, Attack, Name, etc.).
- **Implementation**: The `ListCharactersInRoomByAttribute` method filters and displays characters in the room based on the selected attribute.

#### List All Rooms with All Characters in Those Rooms
- **Feature**: Group characters by their room and display them in a formatted list.
- **Implementation**: The `ListAllRoomsWithCharacters` method retrieves and displays all rooms along with their associated characters.

#### Find a Specific Piece of Equipment and List the Associated Character and Location
- **Feature**: Allow a user to specify the name of an item and output the following:
  - Character holding the item
  - Location of the character
- **Implementation**: The `FindEquipmentAndLocation` method searches for equipment and displays the associated character and their location.

## Grading Level Attempted
- **Attempted Grading Level**: "A" Level (475/500 points)
- **Features Implemented**: All required, "C" level, and "B" level features, as well as the additional "A" level features.


## Additional Features Not Listed in the Guidelines

### Map Functionality
- Feature: Display a map of the rooms and update it as the player navigates.
-	Implementation: The MapManager class handles the loading, updating, and displaying of the map. The map is updated to reflect the player's current room and any changes in room connections.

### Inventory Management
•	**Feature**: Comprehensive inventory management, including searching, listing, sorting, equipping, using, and removing items.
•	**Implementation**: The ShowInventoryMenu method provides a menu for inventory management, and various methods (SearchItemByName, ListItemsByType, SortItems, EquipItem, UseItem, RemoveItem) handle specific inventory actions.

### Room Connections Management
•	**Feature**: Modify room connections dynamically.
•	**Implementation**: The ModifyRoomConnections method allows users to update the connections between rooms, ensuring the game world can be dynamically altered.

### Monster Management
•	**Feature**: Load monsters into random rooms and handle their interactions with the player.
•	**Implementation**: The LoadMonsters method populates rooms with monsters, and the AttackCharacter method handles combat interactions between the player and monsters.

### Summary of Additional Features
•	**Map Functionality**: Display and update a map of the rooms.
•	**Inventory Management**: Comprehensive management of inventory items.
•	**Room Connections Management**: Dynamic modification of room connections.
•	**Monster Management**: Load monsters into rooms and handle combat interactions.

These additional features enhance the overall gameplay experience, providing more depth and interactivity beyond the basic requirements and grading levels.
