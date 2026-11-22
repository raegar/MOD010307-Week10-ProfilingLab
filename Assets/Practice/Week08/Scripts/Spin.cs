using UnityEngine;

// Tap it and it spins round once.
public class Spin : MonoBehaviour, IInteractable
{
    [SerializeField] private float degreesPerSecond = 720f;

    private float degreesLeft;

    public void Interact()
    {
        degreesLeft = 360f;
    }

    private void Update()
    {
        if (degreesLeft <= 0f) return;

        float turn = Mathf.Min(degreesPerSecond * Time.deltaTime, degreesLeft);
        transform.Rotate(0f, turn, 0f, Space.World);
        degreesLeft -= turn;
    }
}
