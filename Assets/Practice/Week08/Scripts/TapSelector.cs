using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

// Fires a ray from the camera through the point the player tapped,
// highlights what it hits, and asks it to do its action.
public class TapSelector : MonoBehaviour
{
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private LayerMask tappable = ~0;   // ~0 means "every layer"
    [SerializeField] private CameraRig cameraRig;       // optional

    private Camera cam;
    private Highlightable selected;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Pointer.current == null) return;
        if (!Pointer.current.press.wasPressedThisFrame) return;

        // A tap on a button belongs to the UI, not to the world behind it.
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject()) return;

        HandleTap(Pointer.current.position.ReadValue());
    }

    private void HandleTap(Vector2 screenPosition)
    {
        Ray ray = cam.ScreenPointToRay(screenPosition);
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.yellow, 1f);

        if (!Physics.Raycast(ray, out RaycastHit hit, maxDistance, tappable))
        {
            Select(null);   // tapped empty space
            return;
        }

        Select(hit.collider.GetComponentInParent<Highlightable>());

        IInteractable thing = hit.collider.GetComponentInParent<IInteractable>();
        if (thing != null) thing.Interact();
    }

    private void Select(Highlightable next)
    {
        if (selected != null) selected.SetHighlighted(false);
        selected = next;
        if (selected != null) selected.SetHighlighted(true);

        if (cameraRig == null) return;
        if (selected != null) cameraRig.Focus(selected.transform.position);
        else cameraRig.ResetFocus();
    }
}
