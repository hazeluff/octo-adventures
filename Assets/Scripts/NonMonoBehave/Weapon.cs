using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Equipment/Weapon", order = 1)]
public class Weapon : ScriptableObject, IWeapon {
    [SerializeField]
    private string _name;
    [SerializeField]
    private WeaponType _type;
    [SerializeField]
    private int _rangeMin;
    [SerializeField]
    private int _rangeMax;
    [SerializeField]
    private int  _hp;
    [SerializeField]
    private int _mp;
    [SerializeField]
    private int _atk;
    [SerializeField]
    private int _def;
    [SerializeField]
    private int _int;
    [SerializeField]
    private int _res;
    [SerializeField]
    private int _hit;
    [SerializeField]
    private int _spd;
    [SerializeField]
    private int _mv;

    public string Name { get { return _name; } }
    public WeaponType Type { get { return _type; } }
    public int RangeMin { get { return _rangeMin; } }
    public int RangeMax { get { return _rangeMax; } }
    public int HP { get { return _hp; } }
    public int MP { get { return _mp; } }
    public int Atk { get { return _atk; } }
    public int Def { get { return _def; } }
    public int Int { get { return _int; } }
    public int Res { get { return _res; } }
    public int Hit { get { return _hit; } }
    public int Spd { get { return _spd; } }
    public int Mv { get { return _mv; } }

    public float ElemRes(Element element) {
        return 0.0f;
    }
}
