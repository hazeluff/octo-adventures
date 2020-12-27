﻿using UnityEngine;

[CreateAssetMenu(fileName = "UnitProfile", menuName = "Unit", order = 1)]
public class UnitProfile : ScriptableObject, IUnitProfile
{
    // Underlying serializable fields
    [SerializeField]
    private string _name;
    [SerializeField]
    private int _level;
    [SerializeField]
    private UnitClass _class;
    [SerializeField]
    private int _expCurrent;
    [SerializeField]
    private int _hp;
    [SerializeField]
    private int _ep;
    [SerializeField]
    private int _atk;
    [SerializeField]
    private int _def;
    [SerializeField]
    private int _acc;
    [SerializeField]
    private int _eva;
    [SerializeField]
    private int _spd;
    [SerializeField]
    private int _rng;
    [SerializeField]
    private int _mv;
    [SerializeField]
    private Weapon _weapon;
    [SerializeField]
    private IEquipment[] _equipments;

    public string Name { get { return _name; } set { _name = value; } }
    public int Lvl { get { return _level; } set { _level = value; } }
    public UnitClass Class { get { return _class; } set { _class = value; } }
    public int ExpCurrent { get { return _expCurrent; } set { _expCurrent = value; } }
    public int ExpToNext {
        get { return Lvl * 10; }
    }

    public void GainExp(int exp) {
        throw new System.NotImplementedException();
    }

    public int HP { get { return _hp; } set { _hp = value; } }
    public int EP { get { return _ep; } set { _ep = value; } }
    public int Atk { get { return _atk; } set { _atk = value; } }
    public int Def { get { return _def; } set { _def = value; } }
    public int Acc { get { return _acc; } set { _acc = value; } }
    public int Eva { get { return _eva; } set { _eva = value; } }
    public int Spd { get { return _spd; } set { _spd = value; } }
    public int Rng { get { return _rng; } set { _rng = value; } }
    public int Mv { get { return _mv; } set { _mv = value; } }

    public Weapon Weapon { get { return _weapon; } set { _weapon = value; } }

    public IEquipment[] Equipments { get { return _equipments; } set { _equipments = value; } }

    public float ElemRes(Element element) {
        throw new System.NotImplementedException();
    }
}
