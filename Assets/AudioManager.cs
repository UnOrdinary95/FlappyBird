using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("----- Audio Source -----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----- Volume -----")]
    [Range(0f, 1f)]
    [SerializeField] float musicVolume = 0.5f;
    [Range(0f, 1f)]
    [SerializeField] float sfxVolume = 0.5f;

    [Header("----- Audio Clip -----")]
    public AudioClip mainmenu;
    public AudioClip secondphase;
    public AudioClip thirdphase;
    public AudioClip shield;
    public AudioClip coin;
    public AudioClip confirm;
    public AudioClip gameover;

    [Header("----- Speaker -----")]
    public bool isOn = false;

    void Awake()
    {
        // If an instance already exists, destroy this one
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Otherwise, set this as the instance and make it persist
        instance = this;

        // By default, every GameObject is destroyed when changing scene unless marked as DontDestroyOnLoad
        DontDestroyOnLoad(gameObject);
    }

    // Plays a sound effect once using the SFX AudioSource.
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip, sfxVolume);
    }

    // Plays background music. If the same clip is already playing, does nothing.
    public void PlayMusic(AudioClip clip)
    {
        // Do nothing if same clip is already playing
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.clip = clip;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    // Stops the currently playing music.
    public void StopMusic()
    {
        musicSource.Stop();
    }
}
