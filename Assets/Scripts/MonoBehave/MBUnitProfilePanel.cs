using System;
using UnityEngine;
using UnityEngine.UI;

public class MBUnitProfilePanel : MonoBehaviour {

    [SerializeField]
    private MBStage stage;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Text unitName;
    [SerializeField]
    private Text unitHP;
    [SerializeField]
    private Text unitFaction;

    private int selectedUnitHash;

    void Update() {
        panel.SetActive(stage.AnySelected());
        if (!stage.AnySelected()) {
            return;
        }
        IStageUnit selectedUnit = stage.GetSelected();
        int newSelectedUnitHash = selectedUnit.GetHashCode();
        if (selectedUnitHash != newSelectedUnitHash) {
            selectedUnitHash = newSelectedUnitHash;
            unitName.text = selectedUnit.Profile.Name;
            unitHP.text = String.Format("{0} / {1}", selectedUnit.c_HP, selectedUnit.Profile.HP);
            unitFaction.text = selectedUnit.Faction.Name;
        }
    }
}
