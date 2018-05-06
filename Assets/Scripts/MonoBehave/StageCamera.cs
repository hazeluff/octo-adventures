using UnityEngine;

[ExecuteInEditMode]
public class StageCamera : MonoBehaviour {

    public float angle = 30;

#if UNITY_EDITOR
    void Update () {
        if (!Application.isPlaying) { // Does not update in play mode
            if (Mathf.Abs(this.transform.localRotation.eulerAngles.x - this.angle) > 0.1f) {
                setAngle(angle);
            }
        }
	}
#endif

    public void setAngle(float angle) {
        this.transform.localPosition = StageCameraHelper.CalculateNewPosition(this.transform.localPosition, angle);
        Vector3 newRotation = this.transform.localEulerAngles;
        newRotation.x = angle;
        this.transform.localEulerAngles = newRotation;
    }
}

static class StageCameraHelper {
    /// <summary>
    /// Takes the a position (its x and z) and determines how high (y) it is for the camera
    /// at the defined angle to still be pointing at the local 0,0.
    /// </summary>
    /// <param name="currentPosition">position of the camera</param>
    /// <param name="angle">angle the camera needs to be looking down at</param>
    /// <returns>the new position the camera needs to be to accomodate the angle</returns>
    public static Vector3 CalculateNewPosition(Vector3 currentPosition, float angle) {
        float baseDistance = new Vector2(currentPosition.x, currentPosition.z).magnitude;
        float y = baseDistance * Mathf.Tan(Mathf.Deg2Rad * angle);
        Vector3 result = new Vector3(
            currentPosition.x,
            y,
            currentPosition.z);
        return result;
    }
}
