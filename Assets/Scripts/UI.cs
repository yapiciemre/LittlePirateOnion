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

    // ChatIconController referans�
    private ChatIconController chatIconController;


    [Header("Reload Details")]
    [SerializeField] private BoxCollider2D reloadWindow;
    [SerializeField] private GunController gunController;
    [SerializeField] private int reloadSteps;
    [SerializeField] private UIReloadButton[] reloadButtons;


    private void Awake()
    {
        instance = this; // Bu s�n�f�n instance'�n� statik de�i�ken olarak ayarla

        // ChatIconController scriptini bul
        chatIconController = FindObjectOfType<ChatIconController>();
    }

    void Start()
    {
        // Oyunun ba�lad��� zaman� ayarla
        startTime = Time.time;

        reloadButtons = GetComponentsInChildren<UIReloadButton>(true);
    }

    void Update()
    {
        if (!IsGameOver) // Oyun bitmemi�se zaman sayac�n� g�ncelle
        {
            float elapsedTime = Time.time - startTime; // Ge�en s�reyi hesapla
            timerText.text = elapsedTime.ToString("0"); // Zaman� g�sterecek �ekilde formatla
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            OpenReloadUI();
        }
    }

    public void OpenReloadUI()
    {
        foreach (UIReloadButton button in reloadButtons)
        {
            button.gameObject.SetActive(true);

            float randomX = Random.Range(reloadWindow.bounds.min.x, reloadWindow.bounds.max.x);
            float randomY = Random.Range(reloadWindow.bounds.min.y, reloadWindow.bounds.max.y);

            button.transform.position = new Vector2(randomX, randomY);
        }

        Time.timeScale = .4f;
        reloadSteps = reloadButtons.Length;
    }

    public void AttemptToReload()
    {
        if (reloadSteps > 0)
            reloadSteps--;

        if (reloadSteps <= 0)
            gunController.ReloadGun();
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

        // E�er mermi biterse ve ChatIcon daha �nce g�sterilmediyse
        if (currentBullets == 0 && chatIconController != null)
        {
            chatIconController.ShowChatIcon();
        }
    }

    public void OpenGameOverScreen()
    {
        Time.timeScale = 0; // Oyunu durdur (zaman �l�e�ini s�f�rla)
        tryAgainButton.SetActive(true); // "Tekrar Dene" butonunu ekranda aktif hale getir
        MusicManager.instance.StopMusic(); // Oyun bitti�inde m�zi�i durdur
        IsGameOver = true; // Oyun bitmi� olarak i�aretle

    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Zaman �l�e�ini s�f�rla, b�ylece oyun tekrar normal h�zda �al���r
        SceneManager.LoadScene(1); // Sahneyi yeniden y�kleyerek oyunu ba�tan ba�lat
        MusicManager.instance.RestartMusic(); // Oyun yeniden ba�lat�ld���nda m�zi�i tekrar ba�lat
        IsGameOver = false; // Oyun durumunu s�f�rla
        startTime = Time.time; // Zamanlay�c�y� s�f�rla
    }
}
