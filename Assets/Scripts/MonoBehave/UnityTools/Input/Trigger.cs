using UnityEngine;

public class Trigger {
    private const float PRESSED_THRESHOLD = 0.8f;
    private const float DEPRESSED_THRESHOLD = 0.1f;
    public float Value { get { return Input.GetAxis(axis); } }
    public bool Pressed { get { return Value > PRESSED_THRESHOLD && depressed; } }
    private bool depressed = true;

    private readonly string axis;

    public Trigger(string axis) {
        this.axis = axis;
    }

    public void UpdateState() {
        if (Value > PRESSED_THRESHOLD) {
            depressed = false;
        } else if (Value < DEPRESSED_THRESHOLD) {
            depressed = true;
        }
    }
}
