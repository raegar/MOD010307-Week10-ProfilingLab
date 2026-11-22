using UnityEngine;

// For a 2D (orthographic) camera: always shows the whole play area,
// whatever shape the screen is. Taller phones see a bit extra above and
// below it; wider screens see a bit extra at the sides.
[RequireComponent(typeof(Camera))]
public class CameraFit : MonoBehaviour
{
    [SerializeField] private float playWidth = 9f;     // world units that must fit across
    [SerializeField] private float playHeight = 16f;   // world units that must fit up and down

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        // orthographicSize is HALF the height the camera shows, in world units.
        // cam.aspect is the screen's width divided by its height.
        float sizeToFitHeight = playHeight / 2f;
        float sizeToFitWidth = playWidth / cam.aspect / 2f;

        cam.orthographicSize = Mathf.Max(sizeToFitHeight, sizeToFitWidth);
    }
}
