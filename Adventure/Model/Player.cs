namespace Adventure.Model;

public class Player : Character, IAttackable
{
    public int HitPoints { get; set; }
}