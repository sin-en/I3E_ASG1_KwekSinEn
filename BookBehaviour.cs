using UnityEngine;

public class BookBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioClip collectSFX;
    private AudioSource audioSource;
    private bool isCollected = false;
    public void Collect(PlayerBehaviour player)
    {
        if (isCollected) return;
        isCollected = true;
        Debug.Log("Book collected!");

        
        player.hasBook = true; 
        if (audioSource != null && collectSFX != null)
        {
            audioSource.PlayOneShot(collectSFX);
        }
        Destroy(gameObject, 0.1f);
    }
}