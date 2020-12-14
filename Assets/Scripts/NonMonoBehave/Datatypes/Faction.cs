using UnityEngine;

[CreateAssetMenu(fileName = "Factions", menuName = "Faction", order = 1)]
public class Faction : ScriptableObject {
    [SerializeField]
    string _name;

    public string Name { get { return _name; } }
}
