using UnityEngine;

// A barrel in the Barrel Hunt practice game. Tapping it "finds" it.
public class Barrel : MonoBehaviour, IInteractable
{
    [SerializeField] private BarrelHuntGame game;

    public void Interact()
    {
        gameObject.SetActive(false);
        game.BarrelFound();
    }
}
