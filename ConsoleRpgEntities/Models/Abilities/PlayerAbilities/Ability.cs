using ConsoleRpgEntities.Models.Characters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleRpgEntities.Models.Abilities.PlayerAbilities
{
    public abstract class Ability : IAbility
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string AbilityType { get; set; }
        public int AttackBonus { get; set; }
        public int DefenseBonus { get; set; }
        public bool IsActive { get; set; }
        public int Duration { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
