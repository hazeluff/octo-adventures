
using UnityEngine;

public class MBTileTest : MonoBehaviour {
    private static CustomLogger LOGGER = new CustomLogger(typeof(MBTileTest).ToString());

    private Touch currentTouch;
    private bool lockColor = false;

    void OnTouchDown(Touch currentTouch) {
        lockColor = !lockColor;
    }

    void OnTouchOver() {
        if(!lockColor && (currentTouch == null || currentTouch.Phase == TouchPhase.Moved)) {
            GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
    }
}
