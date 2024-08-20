using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance; // Singleton deseni i�in instance
    private AudioSource audioSource; // AudioSource bile�eni referans�

    private void Awake()
    {
        // Singleton deseni: Projede sadece bir MusicManager olmas�n� sa�lar
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Scene de�i�se bile bu GameObject yok edilmez
        }
        else
        {
            Destroy(gameObject); // E�er ba�ka bir MusicManager varsa, bu GameObject yok edilir
        }

        // AudioSource bile�enini al
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Oyun ba�lad���nda m�zi�i ba�lat
        PlayMusic();
    }

    public void PlayMusic()
    {
        // E�er AudioSource mevcutsa ve m�zik �alm�yorsa, m�zi�i ba�lat
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopMusic()
    {
        // E�er AudioSource mevcutsa ve m�zik �al�yorsa, m�zi�i durdur
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void RestartMusic()
    {
        // M�zi�i durdur ve tekrar ba�lat
        StopMusic();
        PlayMusic();
    }
}
