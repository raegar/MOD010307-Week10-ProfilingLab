using MicrogameCourse.Framework;
using UnityEngine;
using UnityEngine.UI;

// Practice microgame: tap every barrel before the timer runs out.
public class BarrelHuntGame : MicrogameBehaviour
{
    [SerializeField] private Barrel[] barrels;
    [SerializeField] private TapSelector tapSelector;
    [SerializeField] private CameraRig cameraRig;
    [SerializeField] private Text progressText;

    private int found;

    public override void Begin(MicrogameSession session)
    {
        base.Begin(session);
        found = 0;
        foreach (Barrel barrel in barrels) barrel.gameObject.SetActive(true);
        tapSelector.enabled = true;   // taps only count while the game is playing
        ShowProgress();
    }

    public override void End()
    {
        base.End();
        tapSelector.enabled = false;
    }

    public void BarrelFound()
    {
        if (!IsRunning) return;

        found++;
        ShowProgress();
        cameraRig.Shake(0.2f);

        if (found == barrels.Length) Win();
    }

    private void ShowProgress()
    {
        progressText.text = $"Barrels: {found} / {barrels.Length}";
    }
}
