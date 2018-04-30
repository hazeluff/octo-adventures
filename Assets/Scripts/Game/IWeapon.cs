public interface IWeapon : IEquipment {

    public enum Type
    {
        // TBD
    }

    Type Type { get; }
    int Range { get; }
}
