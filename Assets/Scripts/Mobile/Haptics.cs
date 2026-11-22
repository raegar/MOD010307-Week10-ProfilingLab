using UnityEngine;

// One place for vibration, so every game can use it and the player can
// switch it off. Handheld.Vibrate is one fixed buzz (about half a second on
// most Android phones), so save it for moments that matter.
public static class Haptics
{
    private const string SettingKey = "HapticsOn";

    public static bool Enabled
    {
        get => PlayerPrefs.GetInt(SettingKey, 1) == 1;
        set
        {
            PlayerPrefs.SetInt(SettingKey, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static void Buzz()
    {
        if (!Enabled) return;

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        Handheld.Vibrate();
#else
        Debug.Log("Haptics: buzz");   // the Editor can't vibrate, so show it in the Console
#endif
    }
}
