public interface IUnit
{
    string Name { get; }
    int Level { get; }
    UnitClass Class { get; }
    int ExpCurrent { get; }
    int ExpToNext { get; }
    void GainExp(int exp);

    // Base Stats
    int HP { get; }
    int MP { get; }
    int Atk { get; }
    int Def { get; }
    int Int { get; }
    int Res { get; }
    int Hit { get; } // Double Hit stat vs => Crit
    int Spd { get; } // Double Speed stat vs => Double Attack

    // Movement
    int Mv { get; }

    // Elemental Resistance (as a %)
    float ElemRes(Element element);

    // Gear
    IWeapon Weapon { get; }
    IEquipment[] Equipments { get; }
}
