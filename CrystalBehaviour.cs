using UnityEngine;

public class CrystalBehaviour : MonoBehaviour
{
    // Coin value that will be added to the player's score
    [SerializeField]
    int points = 0;
    private Renderer crystalRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow; // Changeable in Inspector

    void Start()
    {
        // Get the Renderer component of the coin
        crystalRenderer = GetComponent<Renderer>();
        // Store the original color of the coin
        crystalRenderer.material = new Material(crystalRenderer.material);
        originalColor = crystalRenderer.material.color;
    }

    public void Collect(PlayerBehaviour player)
    {
        // Logic for collecting the coin
        Debug.Log("Crystal collected!");

        player.ModifyScore(points);

        Destroy(gameObject);
    }

     public void Highlight()
    {
        if (crystalRenderer != null)
        {
            crystalRenderer.material.color = highlightColor;
        }
    }

    // Remove the highlight from the coin
    public void Unhighlight()
    {
        if (crystalRenderer != null)
        {
            crystalRenderer.material.color = originalColor;
        }
    }

}
