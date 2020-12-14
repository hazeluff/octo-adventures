using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBUnit : MonoBehaviour, IMBUnit {

    private MeshRenderer _renderer;

    private MBStage stage;

    [SerializeField]
    private StageUnit _unit;
    public IStageUnit Unit { get { return _unit; } }
    private bool _moved = false;
    public bool Moved { get { return _moved; } }

    private void Awake() {
        _renderer = GetComponent<MeshRenderer>();

        _unit.Init();
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
        _moved = true;
    }

    void OnMouseUp () {
        stage.ClickUnit(this);
    }

    public void ResetForTurn() {
        _moved = false;
    }
}
