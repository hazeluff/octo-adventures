using UnityEngine;
using System.Collections;

public class AnalogueStick : Stick {

    public override float Horizontal { get { return Input.GetAxis(h_name); } }
    public override float Vertical { get { return Input.GetAxis(v_name); } }

    public override bool UP { get { return (HELD_UP && !last_UP); } }
    public override bool DOWN { get { return (HELD_DOWN && !last_DOWN); } }
    public override bool LEFT { get { return (HELD_LEFT && !last_LEFT); } }
    public override bool RIGHT { get { return (HELD_RIGHT && !last_RIGHT); } }
    public override bool UP_LEFT { get { return UP && LEFT; } }
    public override bool UP_RIGHT { get { return UP && RIGHT; } }
    public override bool DOWN_LEFT { get { return DOWN && LEFT; } }
    public override bool DOWN_RIGHT { get { return DOWN && RIGHT; } }

    public override bool HELD_UP { get { return Input.GetAxis(v_name) > 0.0f; } }
    public override bool HELD_DOWN { get { return Input.GetAxis(v_name) < 0.0f; } }
    public override bool HELD_LEFT { get { return Input.GetAxis(h_name) < 0.0f; } }
    public override bool HELD_RIGHT { get { return Input.GetAxis(h_name) > 0.0f; } }
    public override bool HELD_UP_LEFT { get { return HELD_UP && HELD_LEFT; } }
    public override bool HELD_UP_RIGHT { get { return HELD_UP && HELD_RIGHT; } }
    public override bool HELD_DOWN_LEFT { get { return HELD_DOWN && HELD_LEFT; } }
    public override bool HELD_DOWN_RIGHT { get { return HELD_DOWN && HELD_RIGHT; } }
    
    protected bool last_UP = false;
    protected bool last_DOWN = false;
    protected bool last_LEFT = false;
    protected bool last_RIGHT = false;

    public AnalogueStick(string h, string v) : base(h, v) { }


    public override void UpdateState() {
        last_UP = HELD_UP;
        last_DOWN = HELD_DOWN;
        last_LEFT = HELD_LEFT;
        last_RIGHT = HELD_RIGHT;
    }
}
