public class Unit : IUnit
{
    // Underlying serializable fields
    string _name;
    private int _level;
    private UnitClass _class;
    private int _expCurrent;
    private int _hp;
    private int _mp;
    private int _atk;
    private int _def;
    private int _int;
    private int _res;
    private int _hit;
    private int _spd;
    private int _mv;
    private IWeapon _weapon;
    private IEquipment[] _equipments;



    public string Name { get { return _name; } set { _name = value; } }
    public int Level { get { return _level; } set { _level = value; } }
    public UnitClass Class { get { return _class; } set { _class = value; } }
    public int ExpCurrent { get { return _expCurrent; } set { _expCurrent = value; } }
    public int ExpToNext {
        get { throw new System.NotImplementedException(); } // Derive from level curve of class
    }

    public int HP { get { return _hp; } set { _hp = value; } }
    public int MP { get { return _mp; } set { _mp = value; } }
    public int Atk { get { return _atk; } set { _atk = value; } }
    public int Def { get { return _def; } set { _def = value; } }
    public int Int { get { return _int; } set { _int = value; } }
    public int Res { get { return _res; } set { _res = value; } }
    public int Hit { get { return _hit; } set { _hit = value; } }
    public int Spd { get { return _spd; } set { _spd = value; } }
    public int Mv { get { return _mv; } set { _mv = value; } }

    public IWeapon Weapon { get { return _weapon; } set { _weapon = value; } }

    public IEquipment[] Equipments { get { return _equipments; } set { _equipments = value; } }

    public float ElemRes(Element element)
    {
        throw new System.NotImplementedException();
    }

    public void GainExp(int exp)
    {
        throw new System.NotImplementedException();
    }
}
