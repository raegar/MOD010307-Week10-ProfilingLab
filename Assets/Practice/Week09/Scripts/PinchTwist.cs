using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

// Two fingers anywhere on the screen: pinch to resize this object, twist to turn it.
// In the Editor, the mouse wheel resizes it.
public class PinchTwist : MonoBehaviour
{
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 3f;

    private bool pinching;
    private float startDistance, startAngle;   // between the two fingers when the pinch began
    private float startScale, startRotation;   // of this object when the pinch began

    private void OnEnable() => EnhancedTouchSupport.Enable();

    private void OnDisable() => EnhancedTouchSupport.Disable();

    private void Update()
    {
        if (Touch.activeTouches.Count < 2)
        {
            pinching = false;
            MouseWheel();
            return;
        }

        Vector2 a = Touch.activeTouches[0].screenPosition;
        Vector2 b = Touch.activeTouches[1].screenPosition;
        float distance = Vector2.Distance(a, b);
        float angle = Mathf.Atan2(b.y - a.y, b.x - a.x) * Mathf.Rad2Deg;

        if (!pinching)
        {
            // The second finger has just gone down: remember where everything started.
            pinching = true;
            startDistance = Mathf.Max(distance, 1f);   // never divide by zero
            startAngle = angle;
            startScale = transform.localScale.x;
            startRotation = transform.eulerAngles.z;
            return;
        }

        // Fingers twice as far apart = twice the size. Fingers turned 30 degrees = object turned 30 degrees.
        SetScale(startScale * distance / startDistance);
        transform.rotation = Quaternion.Euler(0f, 0f, startRotation + Mathf.DeltaAngle(startAngle, angle));
    }

    private void MouseWheel()
    {
        if (Mouse.current == null) return;
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (scroll > 0f) SetScale(transform.localScale.x * 1.1f);
        if (scroll < 0f) SetScale(transform.localScale.x / 1.1f);
    }

    private void SetScale(float scale)
    {
        transform.localScale = Vector3.one * Mathf.Clamp(scale, minScale, maxScale);
    }
}
