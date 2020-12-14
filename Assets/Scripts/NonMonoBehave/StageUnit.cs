using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StageUnit", menuName = "Stage/Unit", order = 1)]
public class StageUnit : ScriptableObject, IStageUnit {
    public const int UNINITIALIZED_START_HP = -1;

    // Underlying serializable fields
    [SerializeField]
    private UnitProfile _profile;
    [SerializeField]
    private Faction _faction;

    [SerializeField]
    private MapCoordinate _position;

    [SerializeField]
    private List<IUserStatusEffect> _statusEffects;
    public int overrideHP = UNINITIALIZED_START_HP;
    private int _currentHP;

    public IUnitProfile Profile { get { return _profile; } }
    public Faction Faction { get { return _faction; } }

    public MapCoordinate Position { get { return _position; } }

    public List<IUserStatusEffect> StatusEffects { get { throw new System.NotImplementedException(); } }
    public int c_HP { get { return _currentHP; } }
    public int c_MP { get { return _profile.MP; } }
    public int c_Atk { get { return _profile.Atk + _profile.Weapon.Atk; } }
    public int c_Def { get { return _profile.Def; } }
    public int c_Int { get { return _profile.Int; } }
    public int c_Res { get { return _profile.Res; } }
    public int c_Hit { get { return _profile.Hit; } }
    public int c_Spd { get { return _profile.Spd; } }
    public int c_Mv { get { return _profile.Mv; } }

    public float c_ElemRes(Element element) {
        throw new System.NotImplementedException();
    }

    public void Init() {
        if (overrideHP == StageUnit.UNINITIALIZED_START_HP) {
            _currentHP = Profile.HP;
        } else {      
            _currentHP = overrideHP;
        }
    }

    // Actions
    public void Attack(IStageUnit target) {
        target.ReceiveAttack(new BasicAttackInstance(c_Atk, DamageType.PHYSICAL));
    }

    public void ReceiveAttack(IDamageSource source) {
        int reducedDmg = source.Damage - c_Def;
        if ((this._currentHP = this._currentHP - reducedDmg) < 0) {
            _currentHP = 0;
        }
    }
}
