using UnityEngine;
using UnityEngine.UI;

// Keeps a UI label above a 3D object: world position -> screen position.
public class WorldLabel : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 1.2f, 0f);

    private Camera cam;
    private Text label;

    private void Awake()
    {
        cam = Camera.main;
        label = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        Vector3 screen = cam.WorldToScreenPoint(target.position + offset);

        label.enabled = screen.z > 0f;   // z below 0 means the point is behind the camera
        transform.position = screen;     // on an Overlay canvas, UI positions are in pixels
    }
}
