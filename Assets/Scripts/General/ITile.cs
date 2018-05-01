using System;
using System.Collections.Generic;

public interface ITile {
    bool Traversable { get; }
    int MoveCost { get; }
    List<ITileEffect> Effects { get; }
}