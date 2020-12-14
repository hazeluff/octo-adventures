using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[ExecuteInEditMode]
public class MBStageCamera : MonoBehaviour {
    private Transform anchorTransform;
    private new Camera camera;

    private const float ROTATION_DURATION = 0.5f;
    [SerializeField][Range(0, 3)]
    private int orientation = 0;
    public int Orientation { get { return orientation; } set { orientation = value; } }
    private float[] orientationMap = new float[] { 0f, 90f, 180f, 270f };
    private float RotationAngle { get { return orientationMap[Orientation]; } }
    public bool Rotating { get { return rotationCoroutine != null; } }
    private Coroutine rotationCoroutine;

    private const float TILT_DURATION = 0.5f;
    [SerializeField][Range(0, 3)]
    private int tilt = 1;
    private float[] angleMap = new float[] { 20f, 30f, 45f, 60f };
    private float TiltAngle { get { return angleMap[tilt]; } }
    private float CurrentAngle { get { return transform.localEulerAngles.x; } }
    public bool Tilting { get { return tiltCoroutine != null; } }
    private Coroutine tiltCoroutine;

    private const float ZOOM_DURATION = 0.5f;
    [SerializeField][Range(0,3)]
    private int zoom = 1;
    private float[] zoomMap = new float[] { 2f, 3f, 4.5f, 6f };
    private float ZoomDistance { get { return zoomMap[zoom]; } }
    public bool Zooming { get { return zoomCoroutine != null; } }
    private Coroutine zoomCoroutine;

    private Vector3 newPosition = Vector3.zero;
    
    private void Awake() {
        anchorTransform = transform.parent;
        camera = GetComponent<Camera>();
        Orientation = 0;
        newPosition = transform.parent.localPosition;
    }

    private void Start() {
        SetTilt(TiltAngle);
    }

    void Update () {
#if UNITY_EDITOR
        SetTilt(TiltAngle);
        SetZoom(ZoomDistance);
        SetRotation(RotationAngle);
#endif
        MoveCamera();
    }

    private void MoveCamera() {
        transform.parent.localPosition = Vector3.Lerp(transform.parent.localPosition, newPosition, 0.5f);
    }

    public void MoveTo(Vector3 newPosition) {
        this.newPosition = newPosition;
    }

    public void Rotate(bool clockwise) {
        if (rotationCoroutine != null) {
            return;
        }

        if (clockwise) {
            if (++Orientation >= orientationMap.Length) {
                Orientation = 0;
            }
        } else {
            if (--Orientation < 0) {
                Orientation = orientationMap.Length - 1;
            }
        }
        rotationCoroutine = StartCoroutine(RotateCamera(clockwise));
    }

    IEnumerator RotateCamera(bool clockwise) {
        float startRotation = anchorTransform.localEulerAngles.y;
        float targetAngle = RotationAngle;
        if (clockwise) {
            if (targetAngle < startRotation) {
                targetAngle += 360f;
            }
        } else {
            if (targetAngle > startRotation) {
                targetAngle -= 360f;
            }
        }
        float targetRotation = targetAngle;
        float time = 0.0f;
        while(time < ROTATION_DURATION) {
            time += Time.deltaTime;
            float ratio = time / ROTATION_DURATION;
            ratio = Mathf.Sin(ratio * Mathf.PI / 2.0f);
            SetRotation(Mathf.Lerp(startRotation, targetRotation, ratio));
            yield return null;
        }
        SetRotation(targetRotation);
        rotationCoroutine = null;
    }

    private void SetRotation(float angle) {
        Vector3 originalRotation = anchorTransform.localEulerAngles;
        anchorTransform.localEulerAngles = new Vector3(originalRotation.x, angle, originalRotation.z);
    }

    public void Tilt(bool up) {
        if (tiltCoroutine != null) {
            return;
        }

        if (up) {
            if (++tilt >= angleMap.Length) {
                tilt = 0;
            }
        } else {
            if (--tilt < 0) {
                tilt = angleMap.Length - 1;
            }
        }

        tiltCoroutine = StartCoroutine(TiltCamera());
    }

    IEnumerator TiltCamera() {
        float startAngle = transform.localEulerAngles.x;
        float targetAngle = TiltAngle;
        float time = 0.0f;
        while (time < TILT_DURATION) {
            time += Time.deltaTime;
            float ratio = time / TILT_DURATION;
            ratio = Mathf.Sin(ratio * Mathf.PI / 2.0f);
            SetTilt(Mathf.Lerp(startAngle, targetAngle, ratio));
            yield return null;
        }
        SetTilt(targetAngle);
        tiltCoroutine = null;
    }

    private void SetTilt(float tiltAngle) {
        transform.localPosition = StageCameraHelper.CalculateNewPosition(transform.localPosition, tiltAngle);
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x = tiltAngle;
        transform.localEulerAngles = newRotation;
    }

    public void Zoom(bool inward) {
        if (zoomCoroutine != null) {
            return;
        }

        if (inward) {
            if (++zoom >= zoomMap.Length) {
                zoom = 0;
            }
        } else {
            if (--zoom < 0) {
                zoom = zoomMap.Length - 1;
            }
        }
        zoomCoroutine = StartCoroutine(ZoomCamera());
    }

    IEnumerator ZoomCamera() {
        float startZoom = camera.orthographicSize;
        float endZoom = ZoomDistance;
        float time = 0.0f;
        while (time < ZOOM_DURATION) {
            time += Time.deltaTime;
            float ratio = time / ZOOM_DURATION;
            ratio = Mathf.Sin(ratio * Mathf.PI / 2.0f);
            SetZoom(Mathf.Lerp(startZoom, endZoom, ratio));
            yield return null;
        }
        SetZoom(endZoom);
        zoomCoroutine = null;
    }

    private void SetZoom(float zoom) {
        camera.orthographicSize = zoom;
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
        return result.normalized * 5.0f;
    }
}
