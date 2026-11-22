using UnityEngine;
using UnityEngine.InputSystem;

// Reads how the phone is tilted, as a 2D direction for gameplay.
// On a phone it uses the accelerometer; in the Editor, the arrow keys or WASD.
public class TiltInput : MonoBehaviour
{
    [SerializeField, Range(0f, 0.3f)] private float deadZone = 0.05f;   // ignore tilts smaller than this
    [SerializeField, Range(1f, 30f)] private float smoothing = 10f;     // higher reacts faster but jitters more

    public Vector2 Tilt { get; private set; }   // each axis from about -1 to 1

    private Vector2 level;   // the reading that counts as "not tilted"

    private void OnEnable()
    {
        // Sensors start switched off to save battery, so switch this one on.
        if (Accelerometer.current != null) InputSystem.EnableDevice(Accelerometer.current);
    }

    private void OnDisable()
    {
        if (Accelerometer.current != null) InputSystem.DisableDevice(Accelerometer.current);
    }

    private void Update()
    {
        Vector2 target = ReadRaw() - level;
        if (target.magnitude < deadZone) target = Vector2.zero;
        target = Vector2.ClampMagnitude(target, 1f);

        // Move part of the way towards the new reading each frame, which smooths out the shake.
        Tilt = Vector2.Lerp(Tilt, target, smoothing * Time.deltaTime);
    }

    // Treat the way the phone is being held right now as level.
    public void Calibrate()
    {
        level = ReadRaw();
    }

    private Vector2 ReadRaw()
    {
        if (Accelerometer.current != null && Accelerometer.current.enabled)
        {
            // Measured in g: about 1 when the phone is still, because gravity is included.
            // x is across the screen and y is up it, so the x and y of gravity are the tilt.
            Vector3 g = Accelerometer.current.acceleration.ReadValue();
            return new Vector2(g.x, g.y);
        }

        Keyboard keys = Keyboard.current;   // no accelerometer: pretend with the keyboard
        if (keys == null) return Vector2.zero;

        float x = (keys.rightArrowKey.isPressed || keys.dKey.isPressed ? 1f : 0f)
                - (keys.leftArrowKey.isPressed || keys.aKey.isPressed ? 1f : 0f);
        float y = (keys.upArrowKey.isPressed || keys.wKey.isPressed ? 1f : 0f)
                - (keys.downArrowKey.isPressed || keys.sKey.isPressed ? 1f : 0f);
        return new Vector2(x, y) * 0.5f;   // a half tilt
    }
}
