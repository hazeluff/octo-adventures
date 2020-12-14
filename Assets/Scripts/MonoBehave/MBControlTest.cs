using UnityEngine;

public class MBControlTest : MonoBehaviour {
    private static CustomLogger LOGGER = new CustomLogger(typeof(MBControlTest).ToString());

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(InputManager.get().A) {
            LOGGER.info("a");
        }
        if (InputManager.get().B) {
            LOGGER.info("b");
        }
        if (InputManager.get().X) {
            LOGGER.info("x");
        }
        if (InputManager.get().Y) {
            LOGGER.info("y");
        }

        LOGGER.info("Left Stick: " + InputManager.get().LEFT_STICK.Vector);
        LOGGER.info("Right Stick: " + InputManager.get().RIGHT_STICK.Vector);
        LOGGER.info("D Pad: " + InputManager.get().DPAD.Vector);
        LOGGER.info("Keyboard \"Stick\"" + InputManager.get().D_PAD.Vector);
    }
}
