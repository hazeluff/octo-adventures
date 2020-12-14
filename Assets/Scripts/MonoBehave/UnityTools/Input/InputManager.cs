using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour {
    private static CustomLogger LOGGER = new CustomLogger(typeof(InputManager).ToString());

    private static InputManager input;

    /// <summary>
    /// The layers we want to touch. (NOT IGNORE)
    /// </summary>
    public LayerMask touchLayers;
    private MouseState _MouseState = MouseState.NEUTRAL;
    public Vector3 MOUSE { get { return Input.mousePosition; } }
    public bool LEFT_CLICK { get { return Input.GetMouseButtonDown(0); } }
    public bool RIGHT_CLICK { get { return Input.GetMouseButtonDown(1); } }

    public bool AnyButton { get { return A || B || X || Y || RB || LB || BACK || START; } }
    public bool AnyInput { get { return A || B || X || Y || RB || LB || BACK || START || DPAD.AnyInput || LEFT_STICK.AnyInput || RIGHT_STICK.AnyInput || RT.Pressed || LT.Pressed; } }

    public readonly Stick D_PAD = new KeyboardStick();
    public readonly AnalogueStick LEFT_STICK = new AnalogueStick("Horizontal", "Vertical");
    public readonly AnalogueStick RIGHT_STICK = new AnalogueStick("R_Horizontal", "R_Vertical");
    public readonly Stick DPAD = new XBoxDPad("D_Horizontal", "D_Vertical");
    public readonly Trigger LT = new Trigger("LT");
    public readonly Trigger RT = new Trigger("RT");
    public bool SCROLL_UP { get { return Input.GetAxis("MouseScroll") > 0.0f; } }
    public bool SCROLL_DOWN { get { return Input.GetAxis("MouseScroll") < 0.0f; } }


    // Buttons
#if UNITY_WEBPLAYER || UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX
    public bool A { get { return Input.GetKeyDown("joystick 1 button 0") || Input.GetKeyDown(KeyCode.J); } }
    public bool B { get { return Input.GetKeyDown("joystick 1 button 1") || Input.GetKeyDown(KeyCode.K); } }
    public bool X { get { return Input.GetKeyDown("joystick 1 button 2") || Input.GetKeyDown(KeyCode.U); } }
    public bool Y { get { return Input.GetKeyDown("joystick 1 button 3") || Input.GetKeyDown(KeyCode.I); } }

    public bool LB { get { return Input.GetKeyDown("joystick 1 button 4") || Input.GetKeyDown(KeyCode.L); } }
    public bool RB { get { return Input.GetKeyDown("joystick 1 button 5") || Input.GetKeyDown(KeyCode.Semicolon); } }

    public bool BACK { get { return Input.GetKeyDown("joystick 1 button 6") || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace); } }
    public bool START { get { return Input.GetKeyDown("joystick 1 button 7") || Input.GetKeyDown(KeyCode.Return); } }
#elif UNITY_STANDALONE_OSX
    public bool A { get { return Input.GetKeyDown("joystick 1 button 16"); } }
    public bool B { get { return Input.GetKeyDown("joystick 1 button 17"); } }
    public bool Y { get { return Input.GetKeyDown("joystick 1 button 18"); } }
    public bool X { get { return Input.GetKeyDown("joystick 1 button 19"); } }

    public bool LB { get { return Input.GetKeyDown("joystick 1 button 13"); } }
    public bool RB { get { return Input.GetKeyDown("joystick 1 button 14"); } }
    
    public bool BACK { get { return Input.GetKeyDown("joystick 1 button 10"); } }
    public bool START { get { return Input.GetKeyDown("joystick 1 button 9"); } }
#endif

    void Awake() {
        if (input == null) {
            DontDestroyOnLoad(gameObject);
            input = this;        
        } else if(input != this) {
            Destroy(gameObject);
        }
    }

    public static InputManager get() {
        return input;
    }

    public enum MouseState {
        NEUTRAL, PRESSED, HELD
    }

    private void Update() {

    }

    void LateUpdate() {
        LEFT_STICK.UpdateState();
        RIGHT_STICK.UpdateState();
        DPAD.UpdateState();
        LT.UpdateState();
        RT.UpdateState();
    }


}
