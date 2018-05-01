using System.Collections.Generic;

public interface IMap {
    Dictionary<MapCoordinate, ITile> Tiles { get; }
}
