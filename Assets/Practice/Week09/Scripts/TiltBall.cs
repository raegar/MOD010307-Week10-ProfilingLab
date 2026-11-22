using UnityEngine;

// Rolls this object downhill: pushes its Rigidbody2D the way the phone is tilted.
[RequireComponent(typeof(Rigidbody2D))]
public class TiltBall : MonoBehaviour
{
    [SerializeField] private TiltInput tilt;
    [SerializeField] private float force = 20f;
    [SerializeField] private float buzzSpeed = 6f;   // only a hard knock buzzes

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        body.AddForce(tilt.Tilt * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude >= buzzSpeed) Haptics.Buzz();
    }
}
