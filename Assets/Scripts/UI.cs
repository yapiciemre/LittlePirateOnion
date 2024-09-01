using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance; // UI sýnýfýnýn statik bir örneði (singleton) - UI'ye kolayca eriþim saðlamak için kullanýlýr

    [SerializeField] private TextMeshProUGUI timerText; // Oyun süresini gösterecek TextMesh Pro bileþeni
    [SerializeField] private TextMeshProUGUI scoreText; // Skoru gösterecek TextMesh Pro bileþeni
    [SerializeField] private TextMeshProUGUI ammoText; // Mermi bilgisini gösterecek TextMesh Pro bileþeni

    private int scoreValue; // Oyuncunun skorunu takip eden deðiþken
    private float startTime; // Oyunun baþladýðý zamaný saklamak için deðiþken

    [SerializeField] private GameObject tryAgainButton; // Oyun bittiðinde ekrana gelen "Tekrar Dene" butonu

    public bool IsGameOver { get; private set; } // Oyun bitmiþ mi?

    // ChatIconController referansý
    private ChatIconController chatIconController;


    [Header("Reload Details")]
    [SerializeField] private BoxCollider2D reloadWindow;
    [SerializeField] private GunController gunController;
    [SerializeField] private int reloadSteps;
    [SerializeField] private UIReloadButton[] reloadButtons;


    private void Awake()
    {
        instance = this; // Bu sýnýfýn instance'ýný statik deðiþken olarak ayarla

        // ChatIconController scriptini bul
        chatIconController = FindObjectOfType<ChatIconController>();
    }

    void Start()
    {
        // Oyunun baþladýðý zamaný ayarla
        startTime = Time.time;

        reloadButtons = GetComponentsInChildren<UIReloadButton>(true);
    }

    void Update()
    {
        if (!IsGameOver) // Oyun bitmemiþse zaman sayacýný güncelle
        {
            float elapsedTime = Time.time - startTime; // Geçen süreyi hesapla
            timerText.text = elapsedTime.ToString("0"); // Zamaný gösterecek þekilde formatla
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
        scoreValue++; // Skoru bir artýr
        scoreText.text = scoreValue.ToString("#,#"); // Skoru, binlik ayýrýcý kullanarak formatla ve göster
    }

    public void UpdateAmmoInfo(int currentBullets, int maxBullets)
    {
        // Þu anki mermi sayýsýný ve maksimum mermi sayýsýný ammoText bileþenine yaz
        ammoText.text = currentBullets + " / " + maxBullets;

        // Eðer mermi biterse ve ChatIcon daha önce gösterilmediyse
        if (currentBullets == 0 && chatIconController != null)
        {
            chatIconController.ShowChatIcon();
        }
    }

    public void OpenGameOverScreen()
    {
        Time.timeScale = 0; // Oyunu durdur (zaman ölçeðini sýfýrla)
        tryAgainButton.SetActive(true); // "Tekrar Dene" butonunu ekranda aktif hale getir
        MusicManager.instance.StopMusic(); // Oyun bittiðinde müziði durdur
        IsGameOver = true; // Oyun bitmiþ olarak iþaretle

    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Zaman ölçeðini sýfýrla, böylece oyun tekrar normal hýzda çalýþýr
        SceneManager.LoadScene(1); // Sahneyi yeniden yükleyerek oyunu baþtan baþlat
        MusicManager.instance.RestartMusic(); // Oyun yeniden baþlatýldýðýnda müziði tekrar baþlat
        IsGameOver = false; // Oyun durumunu sýfýrla
        startTime = Time.time; // Zamanlayýcýyý sýfýrla
    }
}
