using System.Collections.Generic;

public interface IStageUnit : IUnit {
    MapCoordinate Position { get; }

    List<IDamageInstance> DamageInstance { get; }
    List<IUserStatusEffect> Status { get; }

    int HP_Current { get; }
    int MP_Current { get; }
    int Atk_Current { get; }
    int Def_Current { get; }
    int Int_Current { get; }
    int Res_Current { get; }
    int Hit_Current { get; }
    int Spd_Current { get; }
    int Mv_Current { get; }
    float ElemRes_Current(Element element);
}