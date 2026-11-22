using UnityEngine;
using UnityEngine.UI;

// Connects a UI Toggle to the Haptics setting, so players can turn vibration off.
[RequireComponent(typeof(Toggle))]
public class HapticsToggle : MonoBehaviour
{
    private void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        toggle.isOn = Haptics.Enabled;
        toggle.onValueChanged.AddListener(OnChanged);
    }

    private void OnChanged(bool on)
    {
        Haptics.Enabled = on;
        if (on) Haptics.Buzz();   // a sample, so the player feels what they switched on
    }
}
