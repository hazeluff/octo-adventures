public interface IEquipment {
    int HP { get; }
    int MP { get; }
    int Atk { get; }
    int Def { get; }
    int Int { get; }
    int Res { get; }
    int Hit { get; }
    int Spd { get; }

    int Mv { get; }

    float ElemRes(Element element);
}
