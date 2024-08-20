using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance; // Singleton deseni için instance
    private AudioSource audioSource; // AudioSource bileþeni referansý

    private void Awake()
    {
        // Singleton deseni: Projede sadece bir MusicManager olmasýný saðlar
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Scene deðiþse bile bu GameObject yok edilmez
        }
        else
        {
            Destroy(gameObject); // Eðer baþka bir MusicManager varsa, bu GameObject yok edilir
        }

        // AudioSource bileþenini al
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Oyun baþladýðýnda müziði baþlat
        PlayMusic();
    }

    public void PlayMusic()
    {
        // Eðer AudioSource mevcutsa ve müzik çalmýyorsa, müziði baþlat
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        // Eðer AudioSource mevcutsa ve müzik çalýyorsa, müziði durdur
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void RestartMusic()
    {
        // Müziði durdur ve tekrar baþlat
        StopMusic();
        PlayMusic();
    }
}
