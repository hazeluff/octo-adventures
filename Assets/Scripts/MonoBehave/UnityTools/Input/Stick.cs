using UnityEngine;

public abstract class Stick {
    protected readonly string h_name;
    protected readonly string v_name;

    public abstract float Horizontal { get; }
    public abstract float Vertical { get; }
    public virtual Vector2 Vector { get { return new Vector2(Horizontal, Vertical); } }
    public Vector2 Vector2 { get { return Vector; } }
    public Vector3 Vector3 { get { return new Vector3(Horizontal, Vertical, 0.0f); } }
    public bool AnyInput { get { return Horizontal != 0.0f || Vertical != 0.0f; } }

    public abstract bool UP { get; }
    public abstract bool DOWN { get; }
    public abstract bool LEFT { get; }
    public abstract bool RIGHT { get; }
    public abstract bool UP_LEFT { get; }
    public abstract bool UP_RIGHT { get; }
    public abstract bool DOWN_LEFT { get; }
    public abstract bool DOWN_RIGHT { get; }

    public abstract bool HELD_UP { get; }
    public abstract bool HELD_DOWN { get; }
    public abstract bool HELD_LEFT { get; }
    public abstract bool HELD_RIGHT { get; }
    public abstract bool HELD_UP_LEFT { get; }
    public abstract bool HELD_UP_RIGHT { get; }
    public abstract bool HELD_DOWN_LEFT { get; }
    public abstract bool HELD_DOWN_RIGHT { get; }

    protected static readonly float THRESHOLD = 0.8f;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="h">Name of Horizontal Axis InputManager</param>
    /// <param name="v">Name of Vertical Axis in InputManager</param>
    public Stick(string h, string v) {
        h_name = h;
        v_name = v;
    }

    protected Stick() : this(null,null) { }

    public abstract void UpdateState();
}