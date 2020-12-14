using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBClickableUI : MonoBehaviour {

    public delegate void UnitAction ();

    private UnitAction action;

    public void setAction(UnitAction action) {
        this.action = action;
    }

    void OnMouseDown() {
        action();
    }
}
