using System.Collections.Generic;

public interface IStageUnit : IUnit {
    List<IUserStatusEffect> Status { get; }

    int HPCurrent { get; }
    int MPCurrent { get; }
    int AtkCurrent { get; }
    int DefCurrent { get; }
    int IntCurrent { get; }
    int ResCurrent { get; }
    int HitCurrent { get; }
    int SpdCurrent { get; }
    int MvCurrent { get; }

    float ElemResCurrent(Element element);
}