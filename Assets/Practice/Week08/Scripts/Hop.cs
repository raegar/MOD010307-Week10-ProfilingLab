using UnityEngine;

// Tap it and it jumps.
[RequireComponent(typeof(Rigidbody))]
public class Hop : MonoBehaviour, IInteractable
{
    [SerializeField] private float force = 5f;

    public void Interact()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * force, ForceMode.Impulse);
    }
}
