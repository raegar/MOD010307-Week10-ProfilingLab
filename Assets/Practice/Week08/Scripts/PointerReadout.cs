using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// Shows where the pointer is in screen, viewport and world coordinates,
// and moves a marker to that point in the 3D world.
public class PointerReadout : MonoBehaviour
{
    [SerializeField] private Text readout;
    [SerializeField] private Transform marker;
    [SerializeField] private float markerDepth = 8f;   // metres in front of the camera

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Pointer.current == null) return;   // no mouse or touchscreen

        Vector2 screen = Pointer.current.position.ReadValue();
        Vector3 viewport = cam.ScreenToViewportPoint(screen);
        Vector3 world = cam.ScreenToWorldPoint(new Vector3(screen.x, screen.y, markerDepth));

        marker.position = world;
        readout.text = $"Screen:   {screen.x:0}, {screen.y:0} pixels\n" +
                       $"Viewport: {viewport.x:0.00}, {viewport.y:0.00}\n" +
                       $"World:    {world.x:0.0}, {world.y:0.0}, {world.z:0.0}";
    }
}
