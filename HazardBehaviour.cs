using UnityEngine;

public class HazardBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioClip hazardSFX;
    [SerializeField]
    private AudioClip ambientSFX;
    private AudioSource ambientSource;

    void Start()
    {
        if (ambientSFX != null)
        {
           ambientSource = GetComponent<AudioSource>();
            if (ambientSource == null)
            {
                ambientSource = gameObject.AddComponent<AudioSource>();
            }
            ambientSource.clip = ambientSFX;
            ambientSource.loop = true;
            ambientSource.playOnAwake = true;
            ambientSource.Play();
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
                    AudioSource.PlayClipAtPoint(hazardSFX, transform.position);
                }
            }
        }
    }
}
