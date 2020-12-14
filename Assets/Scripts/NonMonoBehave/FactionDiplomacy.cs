using UnityEngine;
using System.Collections.Generic;

public class FactionDiplomacy {
    private Dictionary<Faction, Dictionary<Faction, DiplomacyStatus>> Diplomacy = 
        new Dictionary<Faction, Dictionary<Faction, DiplomacyStatus>>();
    private Dictionary<Faction, List<MBUnit>> Units = new Dictionary<Faction, List<MBUnit>>();

    public FactionDiplomacy(HashSet<Faction> factions) {
        foreach (Faction faction in factions) {
            AddFaction(faction);
        }
    }

    public List<Faction> Factions { get { return new List<Faction>(Diplomacy.Keys); } }

    public void AddFaction(Faction faction) {
        foreach (Dictionary<Faction, DiplomacyStatus> FDip in Diplomacy.Values) {
            FDip.Add(faction, DiplomacyStatus.NEUTRAL);
        }

        Dictionary<Faction, DiplomacyStatus>  statuses = new Dictionary<Faction, DiplomacyStatus>();
        foreach(Faction existingFaction in Factions) {
            statuses.Add(existingFaction, DiplomacyStatus.NEUTRAL);
        }

        Diplomacy.Add(faction, new Dictionary<Faction, DiplomacyStatus>());
        Units.Add(faction, new List<MBUnit>());
    }

    public void RegisterUnit(MBUnit unit) {
        Units[unit.Unit.Faction].Add(unit);
    }
}