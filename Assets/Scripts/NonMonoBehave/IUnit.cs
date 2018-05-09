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
    int Atk { get; } // Double Atk over Def => Ignore Def
    int Def { get; } // Double Def over Atk => 0 dmg
    int Int { get; } // Double Int over Res => Ignore Res
    int Res { get; } // Double Res over Int => 0 dmg
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
