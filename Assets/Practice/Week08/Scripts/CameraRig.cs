using UnityEngine;

// Glides the camera to look at a point, keeping the same angle,
// and can shake it for a moment as feedback.
public class CameraRig : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0f, 10f, -10f); // from the focus point
    [SerializeField] private float smoothTime = 0.3f;                     // seconds to (nearly) arrive
    [SerializeField] private float shakeStrength = 0.15f;

    private Vector3 focusPoint;     // where the camera is looking
    private Vector3 basePosition;   // where the camera would be without any shake
    private Vector3 velocity;       // SmoothDamp keeps track of this for us
    private float shakeTimeLeft;

    private void Start()
    {
        basePosition = focusPoint + offset;
        transform.position = basePosition;
        transform.LookAt(focusPoint);
    }

    public void Focus(Vector3 point) => focusPoint = point;
    public void ResetFocus() => focusPoint = Vector3.zero;
    public void Shake(float seconds) => shakeTimeLeft = seconds;

    private void LateUpdate()
    {
        basePosition = Vector3.SmoothDamp(basePosition, focusPoint + offset, ref velocity, smoothTime);

        Vector3 shake = Vector3.zero;
        if (shakeTimeLeft > 0f)
        {
            shakeTimeLeft -= Time.deltaTime;
            shake = Random.insideUnitSphere * shakeStrength;
        }

        transform.position = basePosition + shake;
    }
}
