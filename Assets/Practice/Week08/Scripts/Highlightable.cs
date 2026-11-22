using UnityEngine;

// Tints an object's material while it is selected.
public class Highlightable : MonoBehaviour
{
    [SerializeField] private Color highlightColour = new Color(1f, 0.8f, 0.2f);

    private Renderer rend;
    private Color normalColour;

    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
        normalColour = rend.material.color;
    }

    public void SetHighlighted(bool on)
    {
        rend.material.color = on ? highlightColour : normalColour;
    }
}
