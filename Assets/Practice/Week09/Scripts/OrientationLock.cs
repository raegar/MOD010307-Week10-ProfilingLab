using UnityEngine;

// Turns the screen to this orientation while this object is active, and
// back again when it goes. Lets one microgame be landscape in a portrait framework.
public class OrientationLock : MonoBehaviour
{
    [SerializeField] private ScreenOrientation orientation = ScreenOrientation.LandscapeLeft;

    private ScreenOrientation previous;

    private void OnEnable()
    {
        previous = Screen.orientation;
        Screen.orientation = orientation;
    }

    private void OnDisable()
    {
        Screen.orientation = previous;
    }
}
