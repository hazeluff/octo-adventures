using UnityEngine;
using System.Collections;

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
    public bool AnyInput { get { return A || B || X || Y || RB || LB || BACK || START || DPAD.anyInput || LEFT_STICK.anyInput || RIGHT_STICK.anyInput || RT > 0.0f || LT > 0.0f; } }

    public readonly Stick KEYBOARD_STICK = new KeyboardStick();
    public readonly AnalogueStick LEFT_STICK = new AnalogueStick("Horizontal", "Vertical");
    public readonly AnalogueStick RIGHT_STICK = new AnalogueStick("R_Horizontal", "R_Vertical");
    public readonly Stick DPAD = new XBoxDPad("D_Horizontal", "D_Vertical");
    public float LT { get { return Input.GetAxis("LT"); } }
    public float RT { get { return Input.GetAxis("RT"); } }
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

    // Touch related
    private const string TOUCH_DOWN_METHOD_NAME = "OnTouchDown";
    private const string TOUCH_OVER_METHOD_NAME = "OnTouchOver";

    private Vector3 lastMousePosition = new Vector3(0.0f, 0.0f, 0.0f);
    private const float moveThreshold = 0.01f;
    Touch currentTouch;

    void Awake() {
        if (input == null) {
            DontDestroyOnLoad(gameObject);
            input = this;        
        } else if(input != this) {
            Destroy(gameObject);
        }
        currentTouch = new Touch(Touch.MOUSE_INPUT);
        currentTouch.Phase = TouchPhase.Ended;
    }

    public static InputManager get() {
        return input;
    }

    public enum MouseState {
        NEUTRAL, PRESSED, HELD
    }

    private void Update() {
        // PC (mouse) controls
        // Emulate mouse movement/clicks as Touches

        // Set phase of touch
        if (Input.GetMouseButtonDown(0)) {
            currentTouch = new Touch(Touch.MOUSE_INPUT);
            currentTouch.Phase = TouchPhase.Began;
        } else if (Input.GetMouseButtonUp(0)) {
            currentTouch.Phase = TouchPhase.Ended;
        } else if (Input.GetMouseButton(0)) {
            if ((Input.mousePosition - lastMousePosition).magnitude < moveThreshold) {
                currentTouch.Phase = TouchPhase.Stationary;
            } else {
                currentTouch.Phase = TouchPhase.Moved;
            }
        }

        // Set position of touch
        currentTouch.Position = Input.mousePosition;

        // If the touch is inside the collider of a object, then tell that object that there is a new touch command.
        foreach (Camera camera in Camera.allCameras) {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, ~touchLayers.value)) {
                GameObject recipient = hit.transform.gameObject;
                if (currentTouch.Phase == TouchPhase.Began) {
                    recipient.SendMessage(TOUCH_DOWN_METHOD_NAME, currentTouch, SendMessageOptions.DontRequireReceiver);
                } else {
                    Touch touch = new Touch(Touch.MOUSE_INPUT);
                    touch.Position = Input.mousePosition;
                    touch.Phase = (Input.mousePosition - lastMousePosition).magnitude < moveThreshold ? TouchPhase.Stationary : TouchPhase.Moved;
                    recipient.SendMessage(TOUCH_OVER_METHOD_NAME, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
        lastMousePosition = Input.mousePosition;

        /*
        // Tablet  controls
        if (Input.touchCount > 0) {
            UnityEngine.Touch? uTouch = Input.touches.FirstOrDefault(touch => touch.fingerId == currentTouch.Id);
            if(!uTouch.HasValue) {
                uTouch = Input.touches[0];
                currentTouch.Phase = TouchPhase.Ended; // End the old touch
                currentTouch = new Touch(uTouch.GetValueOrDefault().fingerId); // Set a new touch
            }
            currentTouch.Position = uTouch.GetValueOrDefault().position;
            currentTouch.Phase = uTouch.GetValueOrDefault().phase;

            foreach (Camera camera in Camera.allCameras) {
                Ray ray = camera.ScreenPointToRay(currentTouch.Position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, ~touchLayers.value)) {
                    GameObject recipient = hit.transform.gameObject;
                    if (currentTouch.Phase == TouchPhase.Began) {
                        LOGGER.trace("Touched: " + recipient.name);
                        recipient.SendMessage("OnTouchDown", currentTouch, SendMessageOptions.DontRequireReceiver);
                    } else {
                        recipient.SendMessage(TOUCH_OVER_METHOD_NAME, currentTouch, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
        */
    }

    void LateUpdate() {
        LEFT_STICK.update();
        RIGHT_STICK.update();
        DPAD.update();
    }


}
