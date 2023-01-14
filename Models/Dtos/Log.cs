namespace Models.Dtos;

public class Log
{
    public string CharacterName { get; set; }
    public int DiceRoll { get; set; }
    public int AttackModifier { get; set; }
    public int[] Damage { get; set; }
    public HitType[] HitType { get; set; }
    public string EnemyName { get; set; }
    public int EnemyHp { get; set; }
}