using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Tile", menuName = "Stage/Tile", order = 1)]
public class Tile : ScriptableObject, ITile {
    [SerializeField]
    private bool traversable;
    public bool Traversable { get { return traversable; } }
    [SerializeField]
    private int moveCost;
    public int MoveCost(IStageUnit unit) {
        return moveCost;
    }
    public List<ITileEffect> Effects { get { return new List<ITileEffect>(); } }

}