using UnityEngine;

public class BookBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioClip collectSFX;
    private bool isCollected = false;
    public void Collect(PlayerBehaviour player)
    {
        if (isCollected) return;
        isCollected = true;
        Debug.Log("Book collected!");

        
        player.hasBook = true; 
        if (collectSFX != null)
        {
            AudioSource.PlayClipAtPoint(collectSFX, transform.position);
        }
        Destroy(gameObject);
    }
}
