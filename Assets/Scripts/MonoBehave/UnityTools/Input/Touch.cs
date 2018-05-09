using UnityEngine;

public class Touch {

    public const int MOUSE_INPUT = -201;
    public const int CONTROLLER_INPUT = -202;

    // For PC mouse controls
    public TouchPhase Phase { get; set; }
    public Vector2 Position { get; set; }
    public int Id { get; private set; }

    public Touch () {}

    public Touch (int id) {
        Id = id;
    }

    public Vector3 getScenePosition(float z) {
        return Camera.main.ScreenToWorldPoint(new Vector3(Position.x, Position.y, z - Camera.main.transform.position.z));
    }
}
