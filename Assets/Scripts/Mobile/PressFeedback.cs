using UnityEngine;
using UnityEngine.EventSystems;

// Shrinks a button a little while it is held down. On a phone the finger
// covers the button, so its colour change is hidden, but a change of size shows.
public class PressFeedback : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float pressedScale = 0.9f;

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * pressedScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
}
