﻿using System.Collections.Generic;

public interface IStageUnit {

    IUnitProfile Profile { get; }
    Faction Faction { get; }
    
    List<IUserStatusEffect> StatusEffects { get; }

    int c_HP { get; }
    int c_EP { get; }
    int c_Mv { get; }

    int c_Atk { get; }
    int c_Def { get; }
    int c_Acc { get; }

    int c_Eva { get; }
    int c_Spd { get; }
    int c_Rng { get; }

    float c_ElemRes(Element element);

    void Init(MapCoordinate pos);

    // State
    int Cooldown { get; }
    int LastTurn { get; }
    void InitCooldown(int highestUnitSpd);
    void AddCooldown(int amt);
    void ReduceCooldown(int amt);
    void SetLastTurn(int turn);

    // Actions
    // Move
    bool Moved { get; }
    MapCoordinate Position { get; }
    MapCoordinate LastPos { get; }
    void MoveTo(MapCoordinate newPos);
    //Attack
    bool Attacked { get; }
    void Attack(IStageUnit target);
    void ReceiveAttack(IDamageSource damage);

    void ResetForTurn();

}