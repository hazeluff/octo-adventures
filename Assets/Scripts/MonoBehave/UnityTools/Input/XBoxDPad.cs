using UnityEngine;
using System.Collections;

public class XBoxDPad : AnalogueStick {
#if UNITY_STANDALONE_OSX
    // Analogue component of DPad (Dpad as a "Stick")
    // OSX Does not have a Stick as the DPad. Implementation return values based off of Button inputs above.
    public override float Horizontal { get { return UP ? 1.0f : DOWN ? -1.0f : 0.0f; } }
    public override float Vertical { get { return RIGHT ? 1.0f : LEFT ? -1.0f : 0.0f; } }
    public override Vector2 Vector { get { return new Vector2(Horizontal, Vertical).normalized; } }

    public override bool UP { get { return Input.GetKeyDown("joystick 1 button 5"); } }
    public override bool DOWN { get { return Input.GetKeyDown("joystick 1 button 6"); } }
    public override bool LEFT { get { return Input.GetKeyDown("joystick 1 button 7"); } }
    public override bool RIGHT { get { return Input.GetKeyDown("joystick 1 button 8"); } }

    public override bool HELD_UP { get { return Input.GetKey("joystick 1 button 5");  } }
    public override bool HELD_DOWN { get { return Input.GetKey("joystick 1 button 6"); } }
    public override bool HELD_LEFT { get { return Input.GetKey("joystick 1 button 7"); } }
    public override bool HELD_RIGHT { get { return Input.GetKey("joystick 1 button 8"); } }
#endif

    public XBoxDPad(string h, string v) : base(h, v) { }
}
