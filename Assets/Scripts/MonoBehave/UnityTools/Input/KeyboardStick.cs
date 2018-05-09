using UnityEngine;
using System.Linq;
using System.Collections;

public class KeyboardStick : Stick {
    public override float Horizontal { get { return ((_HELD_LEFT ? -1.0f : 0.0f) + (_HELD_RIGHT ? 1.0f : 0.0f)); } }
    public override float Vertical { get { return ((_HELD_UP ? 1.0f : 0.0f) + (_HELD_DOWN ? -1.0f : 0.0f)); } }
    public override Vector2 Vector { get { return new Vector2(Horizontal, Vertical).normalized; } }

    public override bool UP { get { return HELD_UP && !last_UP; } }
    public override bool DOWN { get { return HELD_DOWN && !last_DOWN; } }
    public override bool LEFT { get { return HELD_LEFT && !last_LEFT; } }
    public override bool RIGHT { get { return HELD_RIGHT && !last_RIGHT; } }
    public override bool UP_LEFT { get { return HELD_UP_LEFT && !last_UP && !last_LEFT; } }
    public override bool UP_RIGHT { get { return HELD_UP_RIGHT && !last_UP && !last_RIGHT; } }
    public override bool DOWN_LEFT { get { return HELD_DOWN_LEFT && !last_DOWN && !last_LEFT; } }
    public override bool DOWN_RIGHT { get { return HELD_DOWN_RIGHT && !last_DOWN && !last_RIGHT; } }

    // Use _HOLD_COUNT to disable inputs if too many are pressed
    public override bool HELD_UP { get { return _HELD_UP && _HOLD_COUNT == 1; } }
    public override bool HELD_DOWN { get { return _HELD_DOWN && _HOLD_COUNT == 1; } }
    public override bool HELD_LEFT { get { return _HELD_LEFT && _HOLD_COUNT == 1; } }
    public override bool HELD_RIGHT { get { return _HELD_RIGHT && _HOLD_COUNT == 1; } }
    public override bool HELD_UP_LEFT { get { return _HELD_UP_LEFT && _HOLD_COUNT == 2; } }
    public override bool HELD_UP_RIGHT { get { return _HELD_UP_RIGHT && _HOLD_COUNT == 2; } }
    public override bool HELD_DOWN_LEFT { get { return _HELD_DOWN_LEFT && _HOLD_COUNT == 2; } }
    public override bool HELD_DOWN_RIGHT { get { return _HELD_DOWN_RIGHT && _HOLD_COUNT == 2; } }

    private int _HOLD_COUNT { get { return new bool[] { _HELD_UP, _HELD_DOWN, _HELD_LEFT, _HELD_RIGHT }.Count(b => b); } }

    private bool _HELD_UP { get { return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow); } }
    private bool _HELD_DOWN { get { return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow); } }
    private bool _HELD_LEFT { get { return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow); } }
    private bool _HELD_RIGHT { get { return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow); } }
    private bool _HELD_UP_LEFT { get { return _HELD_UP && _HELD_LEFT; } }
    private bool _HELD_UP_RIGHT { get { return _HELD_UP && _HELD_RIGHT; } }
    private bool _HELD_DOWN_LEFT { get { return _HELD_DOWN && _HELD_LEFT; } }
    private bool _HELD_DOWN_RIGHT { get { return _HELD_DOWN && _HELD_RIGHT; } }

    // Denotes the whether the button was pressed in the last Update
    protected bool last_UP = false;
    protected bool last_DOWN = false;
    protected bool last_LEFT = false;
    protected bool last_RIGHT = false;

    private KeyboardStick(string h, string v) : base(h, v) { }
    public KeyboardStick() : base() { }

    public override void update() {
        last_UP = HELD_UP;
        last_DOWN = HELD_DOWN;
        last_LEFT = HELD_LEFT;
        last_RIGHT = HELD_RIGHT;
    }
}
