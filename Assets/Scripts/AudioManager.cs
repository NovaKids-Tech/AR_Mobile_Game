using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    public AudioSource musicSource; // Arkaplan müziği için
    public AudioClip backgroundMusic; // Inspector'dan atanacak müzik
    
    [Range(0f, 1f)]
    public float musicVolume = 0.5f; // Müzik ses seviyesi

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetupAudio()
    {
        // AudioSource yoksa ekle
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
        }
        
        // Arkaplan müziği ayarları
        musicSource.clip = backgroundMusic;
        musicSource.volume = musicVolume;
        musicSource.loop = true; // Sürekli çalsın
        musicSource.playOnAwake = true;
        
        // Müziği başlat
        musicSource.Play();
    }

    // Müzik sesini ayarlamak için
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }
    }

    // Müziği açıp kapatmak için
    public void ToggleMusic(bool isOn)
    {
        if (musicSource != null)
        {
            if (isOn)
                musicSource.Play();
            else
                musicSource.Stop();
        }
    }
} 