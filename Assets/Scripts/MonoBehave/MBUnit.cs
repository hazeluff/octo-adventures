﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBUnit : MonoBehaviour, IMBUnit {

    private MeshRenderer _renderer;

    private MBStage stage;

    [SerializeField]
    private StageUnit _unit;
    public IStageUnit Unit { get { return _unit; } }

    private void Awake() {
        _renderer = GetComponent<MeshRenderer>();

        switch (_unit.overrideHP) {
            case StageUnit.UNINITIALIZED_START_HP:
                _unit.setHP(_unit.UnitProfile.HP);
                break;
            default:
                _unit.setHP(_unit.overrideHP);
                break;
        }
    }

    public void setStage(MBStage stage) {
        this.stage = stage;
    }

    void Update() {
        if (stage == null) {
            return;
        }
        setColor(stage.IsSelected(this) ? Color.magenta : Color.white);
    }

    public void setColor(Color color) {
        _renderer.material.color = color;
    }

    public void Move(List<MapCoordinate> path) {
        MapCoordinate lastCoord = path[path.Count-1];
        gameObject.transform.localPosition = new Vector3(lastCoord.X, 0.25f + stage.Heights[lastCoord], lastCoord.Y);
    }

    void OnMouseUp () {
        stage.ClickUnit(this);
    }
}
