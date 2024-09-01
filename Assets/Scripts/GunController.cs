using UnityEngine;
using UnityEngine.EventSystems;

public class GunController : MonoBehaviour
{
    [SerializeField] private Animator gunAnim; // Silah animat�r�
    [SerializeField] private Transform gun; // Silah�n transformu
    [SerializeField] private float gunDistance = 1f; // Silah�n karakterden uzakl���

    private bool gunFacingRight = true; // Silah�n sa�a m� yoksa sola m� bakt���n� belirler

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab; // Mermi prefab'�
    [SerializeField] private float bulletSpeed; // Merminin h�z�
    [SerializeField] private int maxBullets = 15; // Maksimum mermi say�s�
    private int currentBullets; // Mevcut mermi say�s�

    private void Start()
    {
        ReloadGun(); // Oyunun ba�lang�c�nda silah� doldur
    }

    void Update()
    {
        if (UI.instance.IsGameOver) // E�er oyun bitti ise ate� etme
            return;

        // Fare pozisyonunu d�nya koordinatlar�na �evir
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        // Silah�n a��s�n� fareye do�ru d�nd�r
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        // Silah�n pozisyonunu g�ncelle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gunDistance, 0, 0);

        // Mermi ate�leme
        if (Input.GetKeyDown(KeyCode.Mouse0) && HaveBullets())
            Shoot(direction);

        // Silah� doldurma
        //if (Input.GetKeyDown(KeyCode.R))
        //    ReloadGun();

        // Silah�n y�n�n� fare pozisyonuna g�re d�nd�r
        GunFlipController(mousePos);
    }

    // Silah�n y�n�n� fare pozisyonuna g�re ayarla
    private void GunFlipController(Vector3 mousePos)
    {
        if (mousePos.x < gun.position.x && gunFacingRight)
            GunFlip();
        else if (mousePos.x > gun.position.x && !gunFacingRight)
            GunFlip();
    }

    // Silah�n y�n�n� de�i�tir
    private void GunFlip()
    {
        gunFacingRight = !gunFacingRight;
        gun.localScale = new Vector3(gun.localScale.x, gun.localScale.y * -1, gun.localScale.z);
    }

    // Mermi ate�leme fonksiyonu
    private void Shoot(Vector3 direction)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        gunAnim.SetTrigger("Shoot"); // Ate�leme animasyonunu ba�lat
        UI.instance.UpdateAmmoInfo(currentBullets, maxBullets); // UI'yi g�ncelle

        // Yeni mermi olu�tur ve h�zland�r
        GameObject newBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;

        // Mermiyi 7 saniye sonra yok et
        Destroy(newBullet, 7);
    }

    // Silah� doldurma fonksiyonu
    public void ReloadGun()
    {
        currentBullets = maxBullets; // Mermileri yeniden doldur
        UI.instance.UpdateAmmoInfo(currentBullets, maxBullets); // UI'yi g�ncelle
        Time.timeScale = 1;
    }

    // Mermi olup olmad���n� kontrol et
    private bool HaveBullets()
    {
        if (currentBullets <= 0)
        {
            return false; // Mermi yok
        }

        currentBullets--; // Mermi say�s�n� azalt
        return true; // Mermi var
    }
}
