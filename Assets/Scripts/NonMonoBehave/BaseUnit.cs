﻿using UnityEngine;

[CreateAssetMenu(fileName = "BaseUnit", menuName = "Unit", order = 1)]
public class BaseUnit : ScriptableObject, IBaseUnit
{
    // Underlying serializable fields
    [SerializeField]
    private string _name;
    [SerializeField]
    private UnitClass _class;
    [SerializeField]
    private int _baseHp;
    [SerializeField]
    private int _baseEp;
    [SerializeField]
    private int _baseAtk;
    [SerializeField]
    private int _baseDef;
    [SerializeField]
    private int _baseAcc;
    [SerializeField]
    private int _baseEva;
    [SerializeField]
    private int _baseSpd;
    [SerializeField]
    private int _baseRng;
    [SerializeField]
    private int _mv;

    public string Name { get { return _name; } }
    public UnitClass Class { get { return _class; } }
    
    public int BaseHp { get { return _baseHp; } }
    public int HpAt(int lvl) { return BaseHp + (int) Mathf.Floor(BaseHp * Mathf.Pow(1.1f, lvl - 1)); }
    
    public int BaseEp { get { return _baseEp; } }
    public int EpAt(int lvl) { return BaseEp + (int) Mathf.Floor(BaseEp * Mathf.Pow(1.1f, lvl - 1)); }
    
    public int BaseAtk { get { return _baseAtk; } }
    public int AtkAt(int lvl) { return BaseAtk + (int) Mathf.Floor(BaseAtk * Mathf.Pow(1.1f, lvl - 1)); }
    
    public int BaseDef { get { return _baseDef; } }
    public int DefAt(int lvl) { return BaseDef + (int) Mathf.Floor(BaseDef * Mathf.Pow(1.1f, lvl - 1)); }
    
    public int BaseAcc { get { return _baseAcc; } }
    public int AccAt(int lvl) { return BaseAcc + (int) Mathf.Floor(BaseAcc * Mathf.Pow(1.1f, lvl - 1)); }
    
    public int BaseEva { get { return _baseEva; } }
    public int EvaAt(int lvl) { return BaseEva + (int) Mathf.Floor(BaseEva * Mathf.Pow(1.1f, lvl - 1)); }
    
    public int BaseSpd { get { return _baseSpd; } }
    public int SpdAt(int lvl) { return BaseSpd + (int) Mathf.Floor(BaseSpd * Mathf.Pow(1.1f, lvl - 1)); }
    
    public int BaseRng { get { return _baseRng; } }
    public int RngAt(int lvl) { return BaseRng + (int) Mathf.Floor(BaseRng * Mathf.Pow(1.1f, lvl - 1)); }


    public int Mv { get { return _mv; } set { _mv = value; } }

    public float ElemRes(Element element) {
        throw new System.NotImplementedException();
    }
}
