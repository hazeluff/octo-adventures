using System.Collections.Generic;

public interface ITile {
    bool Traversable { get; }
    int MoveCost(IStageUnit unit);
    List<ITileEffect> Effects { get; }
}