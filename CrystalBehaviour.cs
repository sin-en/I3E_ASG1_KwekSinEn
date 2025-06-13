using UnityEngine;

public class CrystalBehaviour : MonoBehaviour
{
    int points = 0;
    private Renderer crystalRenderer;
    [SerializeField]
    Material originalMaterial;
    [SerializeField]
    Material highlightMaterial;
    [SerializeField]
    private AudioClip collectSFX;
    private bool isCollected = false;
    void Start()
    {
        crystalRenderer = GetComponent<MeshRenderer>();
        
    }

    public void Collect(PlayerBehaviour player)
    {
        if (isCollected) return;
        isCollected = true;
        Debug.Log("Crystal collected!");

        player.ModifyScore(points);
        player.CollectibleCollected();
        if (collectSFX != null)
        {
            AudioSource.PlayClipAtPoint(collectSFX, transform.position);
        }
        Destroy(gameObject, 0.1f);
    }

    public void Highlight()
    {
        if (highlightMaterial != null)
        {
            crystalRenderer.material = highlightMaterial;
        }
    }

    public void Unhighlight()
    {
        if (crystalRenderer != null)
        {
            crystalRenderer.material = originalMaterial;
        }
    }
    public int GetPoints()
    {
        return points;
    }
    public void SetPoints(int newPoints)
    {
        points = newPoints;
    }
}


