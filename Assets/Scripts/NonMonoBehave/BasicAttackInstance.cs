public class BasicAttackInstance : IDamageSource {
    public int Damage { get; private set; }
    public DamageType Type { get; private set; }

    public BasicAttackInstance(int damage, DamageType type) {
        Damage = damage;
        Type = type;
    }
}