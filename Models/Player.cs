using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models;

public class Player : ICreature
{
    [Required]
    [MaxLength(20, ErrorMessage = "The max length is 20")]
    [DisplayName("Name")]
    public string Name { get; set; }
    
    [Required]
    [Range(0, 1000, ErrorMessage = "Should be number in range from 0 to 1000")]
    [DisplayName("Hit points")]
    public int HitPoints { get; set; }
    
    [Required]
    [Range(-5, 5, ErrorMessage = "Should be number in range from -5 to 5")]
    [DisplayName("Attack modifier")]
    public int AttackModifier { get; set; }
    
    [Required]
    [Range(1, 5, ErrorMessage = "Should be number in range from 1 to 5")]
    [DisplayName("Attacks per round")]
    public int AttackPerRound { get; set; }
    
    [Required]
    [RegularExpression(@"\d+d\d+", ErrorMessage = "Wrong format. Try something like 2d6")]
    [DisplayName("Damage")]
    public string Damage { get; set; }
    
    [Required]
    [Range(-5, 5, ErrorMessage = "Should be number in range from -5 to 5")]
    [DisplayName("Damage modifier")]
    public int DamageModifier { get; set; }
    
    [Required]
    [Range(1, 1000, ErrorMessage = "Should be number in range from 0 to 1000")]
    [DisplayName("Armor class")]
    public int ArmorClass { get; set; }
}