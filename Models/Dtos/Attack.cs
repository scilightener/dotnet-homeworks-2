namespace Models.Dtos;

public class Attack
{
    public Character Active { get; set; }
    public HitType HitType { get; set; }
    public int Damage { get; set; }
    public int EnemyHp { get; set; }
    public int DiceRoll { get; set; }
}