public interface IWeapon : IEquipment {
    WeaponType Type { get; }
    int Range { get; }
}
