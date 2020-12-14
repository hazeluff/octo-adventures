using System.Collections.Generic;

public interface IStageUnit {

    IUnitProfile Profile { get; }
    Faction Faction { get; }

    MapCoordinate Position { get; }
    
    List<IUserStatusEffect> StatusEffects { get; }

    int c_HP { get; }
    int c_MP { get; }
    int c_Atk { get; }
    int c_Def { get; }
    int c_Int { get; }
    int c_Res { get; }
    int c_Hit { get; }
    int c_Spd { get; }
    int c_Mv { get; }
    float c_ElemRes(Element element);

    // Actions
    void Attack(IStageUnit target);
    void ReceiveAttack(IDamageSource damage);
}