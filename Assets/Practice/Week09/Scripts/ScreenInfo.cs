using UnityEngine;
using UnityEngine.UI;

// Shows the facts about this screen that a mobile layout depends on.
public class ScreenInfo : MonoBehaviour
{
    [SerializeField] private Text readout;

    private void Update()
    {
        // Long side divided by short side, so portrait and landscape give the same number.
        float aspect = (float)Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
        Rect safe = Screen.safeArea;

        readout.text = $"Screen: {Screen.width} x {Screen.height} pixels\n" +
                       $"Aspect: {aspect:0.00} : 1\n" +
                       $"DPI: {Screen.dpi:0}\n" +
                       $"Safe area: from ({safe.x:0}, {safe.y:0}), {safe.width:0} x {safe.height:0}\n" +
                       $"Orientation: {Screen.orientation}";
    }
}
