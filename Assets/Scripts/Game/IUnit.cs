public interface IUnit
{
    public enum Class
    {

    }

    string Name { get; }
    int Level { get; }
    Class Class { get; }
    int ExpCurrent { get; }
    int ExpToNext { get; }

    // Base Stats
    int HP { get; }
    int HPCurrent { get; }
    int MP { get; }
    int MPCurrent { get; }
    int Atk { get; }
    int AtkCurrent { get; }
    int Def { get; }
    int DefCurrent { get; }
    int Int { get; }
    int IntCurrent { get; }
    int Res { get; }
    int ResCurrent { get; }
    int Hit { get; } // Double Hit stat vs => Crit
    int HitCurrent { get; }
    int Spd { get; } // Double Speed stat vs => Double Attack
    int SpdCurrent { get; }

    // Movement
    int Mv { get; }
    int MvCurrent { get; }

    // Elemental Resistance (as a %)
    float Res(Element element) { get; }
    float ResCurrent(Element element) { get; }

    // Gear
    IWeapon Weapon { get; }
    IEquipment[] Equipments { get; }
}
