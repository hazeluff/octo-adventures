using System.Collections.Generic;

public interface IMBUnit {
    IStageUnit Unit { get; }

    // Actions
    void Move(List<MapCoordinate> path);
}
