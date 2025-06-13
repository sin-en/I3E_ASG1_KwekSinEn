using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip bgmClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null && bgmClip != null)
        {
            audioSource.clip = bgmClip;
            audioSource.loop = true;
            audioSource.playOnAwake = true;
            audioSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip, Vector3 position)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position);
        }
    }
}
