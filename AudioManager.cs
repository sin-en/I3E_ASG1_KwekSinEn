using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip bgm;
    [SerializeField]
    private float bgmVolume = 0.5f;
    [SerializeField]
    private AudioClip ambientClip;
    [SerializeField]
    private float sfxVolume = 1f;
    private AudioSource bgmSource;
    private AudioSource ambientSource;
    private AudioSource sfxSource;
    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupAudioSources();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PlayMusic();
    }
    private void SetupAudioSources()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;
        bgmSource.playOnAwake = false;

        ambientSource = gameObject.AddComponent<AudioSource>();
        ambientSource.loop = true;
        ambientSource.volume = bgmVolume * 0.7f;
        ambientSource.playOnAwake = false;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.volume = sfxVolume;
        sfxSource.playOnAwake = false;
    }
    public void PlayMusic()
    {
        bgmSource.Stop();
        ambientSource.Stop();

        if (bgm != null)
        {
            bgmSource.clip = bgm;
            bgmSource.Play();
        }

        if (ambientClip != null)
        {
            ambientSource.clip = ambientClip;
            ambientSource.Play();
        }

        Debug.Log("Playing background music and ambient sound");
    }
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip, volume * sfxVolume);
        }
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        if (bgmSource != null)
            bgmSource.volume = bgmVolume;
        if (ambientSource != null)
            ambientSource.volume = bgmVolume * 0.7f;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        if (sfxSource != null)
            sfxSource.volume = sfxVolume;
    }

    public void PauseAll()
    {
        bgmSource?.Pause();
        ambientSource?.Pause();
    }

    public void ResumeAll()
    {
        bgmSource?.UnPause();
        ambientSource?.UnPause();
    }

    public void StopAll()
    {
        bgmSource?.Stop();
        ambientSource?.Stop();
        sfxSource?.Stop();
    }
}
