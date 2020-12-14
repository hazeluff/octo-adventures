public interface IWeapon : IEquipment {
    string Name { get; }
    WeaponType Type { get; }
    int RangeMax { get; }
    int RangeMin { get; }
}
