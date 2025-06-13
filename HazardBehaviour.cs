using UnityEngine;

public class HazardBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioClip hazardSFX;
    [SerializeField]
    private AudioClip ambientSFX;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        if (ambientSFX != null)
        {
            audioSource.clip = ambientSFX;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                Debug.Log("Player entered hazard");

                if (hazardSFX != null)
                {
                    audioSource.PlayOneShot(hazardSFX);
                }
            }
            player.DieAndRespawn();
        }
    }
}