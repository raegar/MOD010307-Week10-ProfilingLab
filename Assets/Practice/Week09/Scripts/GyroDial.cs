using UnityEngine;
using UnityEngine.InputSystem;
using Gyro = UnityEngine.InputSystem.Gyroscope;   // UnityEngine has an older Gyroscope type too

// Turns this object the opposite way to the phone, so it seems to stay
// still in the room while you twist the phone, like a compass needle.
public class GyroDial : MonoBehaviour
{
    [SerializeField] private float keyDegreesPerSecond = 90f;   // Q and E in the Editor

    private float angle;   // degrees

    private void OnEnable()
    {
        if (Gyro.current != null) InputSystem.EnableDevice(Gyro.current);
    }

    private void OnDisable()
    {
        if (Gyro.current != null) InputSystem.DisableDevice(Gyro.current);
    }

    private void Update()
    {
        // The gyroscope gives a SPEED of turning. Speed x time = how far it turned this frame.
        angle -= TwistSpeed() * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void Recentre()
    {
        angle = 0f;
    }

    // How fast the phone is turning about the line pointing out of its screen, in degrees per second.
    private float TwistSpeed()
    {
        if (Gyro.current != null && Gyro.current.enabled)
            return Gyro.current.angularVelocity.ReadValue().z * Mathf.Rad2Deg;   // radians to degrees

        Keyboard keys = Keyboard.current;
        if (keys == null) return 0f;
        float twist = (keys.qKey.isPressed ? 1f : 0f) - (keys.eKey.isPressed ? 1f : 0f);
        return twist * keyDegreesPerSecond;
    }
}
