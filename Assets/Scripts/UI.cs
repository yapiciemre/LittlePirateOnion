using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance; // UI s�n�f�n�n statik bir �rne�i (singleton) - UI'ye kolayca eri�im sa�lamak i�in kullan�l�r

    [SerializeField] private TextMeshProUGUI timerText; // Oyun s�resini g�sterecek TextMesh Pro bile�eni
    [SerializeField] private TextMeshProUGUI scoreText; // Skoru g�sterecek TextMesh Pro bile�eni
    [SerializeField] private TextMeshProUGUI ammoText; // Mermi bilgisini g�sterecek TextMesh Pro bile�eni

    private int scoreValue; // Oyuncunun skorunu takip eden de�i�ken
    private float startTime; // Oyunun ba�lad��� zaman� saklamak i�in de�i�ken

    [SerializeField] private GameObject tryAgainButton; // Oyun bitti�inde ekrana gelen "Tekrar Dene" butonu

    public bool IsGameOver { get; private set; } // Oyun bitmi� mi?

    private void Awake()
    {
        instance = this; // Bu s�n�f�n instance'�n� statik de�i�ken olarak ayarla
    }

    void Start()
    {
        // Oyunun ba�lad��� zaman� ayarla
        startTime = Time.time;
    }

    void Update()
    {
        if (!IsGameOver) // Oyun bitmemi�se zaman sayac�n� g�ncelle
        {
            float elapsedTime = Time.time - startTime; // Ge�en s�reyi hesapla
            timerText.text = elapsedTime.ToString("0"); // Zaman� g�sterecek �ekilde formatla
        }
    }

    public void AddScore()
    {
        scoreValue++; // Skoru bir art�r
        scoreText.text = scoreValue.ToString("#,#"); // Skoru, binlik ay�r�c� kullanarak formatla ve g�ster
    }

    public void UpdateAmmoInfo(int currentBullets, int maxBullets)
    {
        // �u anki mermi say�s�n� ve maksimum mermi say�s�n� ammoText bile�enine yaz
        ammoText.text = currentBullets + " / " + maxBullets;
    }

    public void OpenGameOverScreen()
    {
        Time.timeScale = 0; // Oyunu durdur (zaman �l�e�ini s�f�rla)
        tryAgainButton.SetActive(true); // "Tekrar Dene" butonunu ekranda aktif hale getir
        IsGameOver = true; // Oyun bitmi� olarak i�aretle
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Zaman �l�e�ini s�f�rla, b�ylece oyun tekrar normal h�zda �al���r
        SceneManager.LoadScene(0); // Sahneyi yeniden y�kleyerek oyunu ba�tan ba�lat
        scoreValue = 0; // Skoru s�f�rla
        scoreText.text = scoreValue.ToString("#,#"); // Skoru s�f�r olarak g�ster
        IsGameOver = false; // Oyun durumunu s�f�rla

        // Zamanlay�c�y� s�f�rla
        startTime = Time.time;
    }
}
