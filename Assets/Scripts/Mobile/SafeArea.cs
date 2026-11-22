using UnityEngine;

// Keeps this UI panel inside the screen's safe area, clear of notches,
// camera holes and rounded corners. Put it on a panel that is a direct
// child of the Canvas, and put your HUD inside that panel.
[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
    private RectTransform panel;
    private Rect applied;

    private void Awake()
    {
        panel = GetComponent<RectTransform>();
        Apply();
    }

    private void Update()
    {
        // The safe area changes when the phone rotates, so keep checking.
        if (Screen.safeArea != applied) Apply();
    }

    private void Apply()
    {
        applied = Screen.safeArea;

        // Anchors are fractions of the parent, from 0 to 1, like viewport coordinates.
        panel.anchorMin = new Vector2(applied.xMin / Screen.width, applied.yMin / Screen.height);
        panel.anchorMax = new Vector2(applied.xMax / Screen.width, applied.yMax / Screen.height);

        // No extra margin: the panel's edges sit exactly on its anchors.
        panel.offsetMin = Vector2.zero;
        panel.offsetMax = Vector2.zero;
    }
}
