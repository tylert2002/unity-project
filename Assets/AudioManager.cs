using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static AudioManager Instance;
    private AudioSource audioSource;
    public AudioClip backgroundMusic;
    [SerializeField] private Slider musicSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if missing
            }
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.Play(); // Play music immediately
        }
        else
        {
            Debug.LogError("Background music not assigned!");
        }

        if (musicSlider != null)
        {
            musicSlider.onValueChanged.AddListener(delegate { SetVolume(musicSlider.value); });
            musicSlider.value = 0.5f; // Set default volume
            SetVolume(musicSlider.value); // Apply default volume
        }
        else
        {
            Debug.LogError("Music Slider not assigned!");
        }
    }

    public static void SetVolume(float volume)
    {
        if (Instance != null && Instance.audioSource != null)
        {
            Instance.audioSource.volume = volume;
        }
    }

    public void PlayBackgroundMusic(bool resetSong, AudioClip audioClip = null)
    {
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }
        if (resetSong)
        {
            audioSource.Stop();
        }
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void PauseBackgroundMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }
}
