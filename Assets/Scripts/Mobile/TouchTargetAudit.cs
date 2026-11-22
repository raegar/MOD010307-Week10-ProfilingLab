using UnityEngine;
using UnityEngine.UI;

// Measures every button, toggle and slider in the scene in millimetres on
// THIS screen, and warns about any that are too small for a fingertip.
public class TouchTargetAudit : MonoBehaviour
{
    [SerializeField] private float minimumMillimetres = 9f;
    [SerializeField] private float dpiIfUnknown = 160f;   // some devices report 0

    private void Start()
    {
        Canvas.ForceUpdateCanvases();   // make sure the layout has been worked out

        float dpi = Screen.dpi > 0f ? Screen.dpi : dpiIfUnknown;
        float pixelsPerMillimetre = dpi / 25.4f;   // there are 25.4 mm in an inch

        foreach (Selectable control in FindObjectsByType<Selectable>(FindObjectsSortMode.None))
        {
            Vector2 pixels = SizeOnScreen((RectTransform)control.transform);
            float width = pixels.x / pixelsPerMillimetre;
            float height = pixels.y / pixelsPerMillimetre;

            string report = $"{control.name}: {width:0.0} x {height:0.0} mm";
            if (width < minimumMillimetres || height < minimumMillimetres)
                Debug.LogWarning(report + ", too small to tap reliably", control);
            else
                Debug.Log(report + ", OK", control);
        }
    }

    // How big this UI element is on the screen, in pixels.
    private static Vector2 SizeOnScreen(RectTransform rect)
    {
        // On a Screen Space - Overlay canvas, world positions are screen pixels.
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);   // bottom-left, top-left, top-right, bottom-right
        return corners[2] - corners[0];
    }
}
