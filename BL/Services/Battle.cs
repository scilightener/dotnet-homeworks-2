using Models;
using Models.Dtos;

namespace BL.Services;

public class Battle : IBattle
{
    private ICreature? Player { get; }
    private ICreature? Monster { get; }

    public Battle() { }

    public Battle(Player player, Monster monster)
    {
        Player = player;
        Monster = monster;
    }

    public ResultDto GetResult()
    {
        var result = new ResultDto { Logs = new List<Log>() };
        while (Player.HitPoints > 0 && Monster.HitPoints > 0)
        {
            if (Player.HitPoints > 0)
            {
                var log = ProcessRound(false);
                log.CharacterName = Player.Name;
                log.EnemyName = Monster.Name;
                result.Logs.Add(log);
            }

            if (Monster.HitPoints > 0)
            {
                var log = ProcessRound(true);
                log.CharacterName = Monster.Name;
                log.EnemyName = Player.Name;
                result.Logs.Add(log);
            }
        }

        result.Winner = Player.HitPoints > 0 ? Character.Player : Character.Monster;
        return result;
    }
    
    private Log ProcessRound(bool swap=false)
    {
        var active = Player;
        var passive = Monster;
        if (swap) (active, passive) = (passive, active);
        var log = new Log();
        var rnd = new Random();
        var throws = int.Parse(active.Damage.Split('d')[0]);
        var dice = int.Parse(active.Damage.Split('d')[1]);
        log.HitType = new HitType[active.AttackPerRound];
        log.Damage = new int[active.AttackPerRound];
        log.EnemyHp = new int[active.AttackPerRound];
        log.DiceRoll = new int[active.AttackPerRound];
        log.AttackModifier = active.AttackModifier;
        for (var i = 0; i < active.AttackPerRound && passive.HitPoints > 0; i++)
        {
            var hitRoll = rnd.Next(1, 21);
            log.DiceRoll[i] = hitRoll;
            log.EnemyHp[i] = passive.HitPoints;
            if (hitRoll == 1 || log.DiceRoll[i] < passive.ArmorClass)
            {
                log.HitType[i] = HitType.Miss;
                continue;
            }

            if (hitRoll == 20)
                log.HitType[i] = HitType.Critical;
            else log.HitType[i] = HitType.Hit;
            for (var _ = 0; _ < throws; _++)
                log.Damage[i] += (rnd.Next(1, dice + 1) + active.DamageModifier) * hitRoll / 10;
            passive.HitPoints -= Math.Min(passive.HitPoints, log.Damage[i]);
            log.EnemyHp[i] = passive.HitPoints;
            if (passive.HitPoints == 0) break;
        }

        return log;
    }
    
    // private Log ProcessRound(bool swap=false)
    // {
    //     var active = Player!;
    //     var passive = Monster!;
    //     if (swap) (active, passive) = (passive, active);
    //     var log = new Log();
    //     var rnd = new Random();
    //     var throws = int.Parse(active.Damage.Split('d')[0]);
    //     var dice = int.Parse(active.Damage.Split('d')[1]);
    //     log.AttackModifier = active.AttackModifier;
    //     log.Damage = new int[active.AttackPerRound];
    //     log.HitType = new HitType[active.AttackPerRound];
    //     log.
    //     for (var i = 0;
    //          i < active.AttackPerRound/* + passive.AttackPerRound*/ && passive.HitPoints > 0;
    //          // Math.Min(passive.HitPoints, active.HitPoints) > 0;
    //          i++)
    //     {
    //         // var attack = ProcessAttack(active, passive);
    //         // // log.Attacks.Add(attack);
    //         // if (passive.HitPoints > 0) (active, passive) = (passive, active);
    //
    //         // log.HitType.Add(HitType.Miss);
    //         // log.EnemyHp.Add(passive.HitPoints);
    //         // log.Damage.Add(0);
    //         var hitRoll = rnd.Next(1, 21);
    //         log.DiceRoll = hitRoll;
    //         if (hitRoll == 1 || log.DiceRoll < passive.ArmorClass)
    //         {
    //             log.HitType[i] = HitType.Miss;
    //             continue;
    //         }
    //         
    //         if (hitRoll == 20)
    //             log.HitType[i] = HitType.Critical;
    //         else log.HitType[i] = HitType.Hit;
    //         for (var _ = 0; _ < throws; _++)
    //             log.Damage[i] += (rnd.Next(1, dice + 1) + active.DamageModifier) * hitRoll / 10;
    //         passive.HitPoints = Math.Max(passive.HitPoints - log.Damage[i], 0);
    //         log.EnemyHp[i] = passive.HitPoints;
    //         if (passive.HitPoints == 0) break;
    //     }
    //
    //     return log;
    // }

    private Attack ProcessAttack(ICreature active, ICreature passive)
    {
        var attack = new Attack();
        attack.EnemyHp = passive.HitPoints;
        attack.Active = active is Player ? Character.Player : Character.Monster;
        var rnd = new Random();
        var hitRoll = rnd.Next(1, 21);
        attack.DiceRoll = hitRoll;
        if (hitRoll == 1 || hitRoll + active.AttackModifier < passive.ArmorClass)
        {
            attack.HitType = HitType.Miss;
            return attack;
        }

        if (hitRoll == 20) attack.HitType = HitType.Critical;
        else attack.HitType = HitType.Hit;
        var dice = int.Parse(active.Damage.Split('d')[1]);
        attack.Damage = (rnd.Next(1, dice + 1) + active.DamageModifier) * hitRoll / 10;
        passive.HitPoints = Math.Max(passive.HitPoints - attack.Damage, 0);
        attack.EnemyHp = passive.HitPoints;
        return attack;
    }
}