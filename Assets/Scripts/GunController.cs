using UnityEngine;
using UnityEngine.EventSystems;

public class GunController : MonoBehaviour
{
    [SerializeField] private Animator gunAnim; // Silah animatörü
    [SerializeField] private Transform gun; // Silahýn transformu
    [SerializeField] private float gunDistance = 1f; // Silahýn karakterden uzaklýðý

    private bool gunFacingRight = true; // Silahýn saða mý yoksa sola mý baktýðýný belirler

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab; // Mermi prefab'ý
    [SerializeField] private float bulletSpeed; // Merminin hýzý
    [SerializeField] private int maxBullets = 15; // Maksimum mermi sayýsý
    private int currentBullets; // Mevcut mermi sayýsý

    private void Start()
    {
        ReloadGun(); // Oyunun baþlangýcýnda silahý doldur
    }

    void Update()
    {
        if (UI.instance.IsGameOver) // Eðer oyun bitti ise ateþ etme
            return;

        // Fare pozisyonunu dünya koordinatlarýna çevir
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        // Silahýn açýsýný fareye doðru döndür
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        // Silahýn pozisyonunu güncelle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gunDistance, 0, 0);

        // Mermi ateþleme
        if (Input.GetKeyDown(KeyCode.Mouse0) && HaveBullets())
            Shoot(direction);

        // Silahý doldurma
        //if (Input.GetKeyDown(KeyCode.R))
        //    ReloadGun();

        // Silahýn yönünü fare pozisyonuna göre döndür
        GunFlipController(mousePos);
    }

    // Silahýn yönünü fare pozisyonuna göre ayarla
    private void GunFlipController(Vector3 mousePos)
    {
        if (mousePos.x < gun.position.x && gunFacingRight)
            GunFlip();
        else if (mousePos.x > gun.position.x && !gunFacingRight)
            GunFlip();
    }

    // Silahýn yönünü deðiþtir
    private void GunFlip()
    {
        gunFacingRight = !gunFacingRight;
        gun.localScale = new Vector3(gun.localScale.x, gun.localScale.y * -1, gun.localScale.z);
    }

    // Mermi ateþleme fonksiyonu
    private void Shoot(Vector3 direction)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        gunAnim.SetTrigger("Shoot"); // Ateþleme animasyonunu baþlat
        UI.instance.UpdateAmmoInfo(currentBullets, maxBullets); // UI'yi güncelle

        // Yeni mermi oluþtur ve hýzlandýr
        GameObject newBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;

        // Mermiyi 7 saniye sonra yok et
        Destroy(newBullet, 7);
    }

    // Silahý doldurma fonksiyonu
    public void ReloadGun()
    {
        currentBullets = maxBullets; // Mermileri yeniden doldur
        UI.instance.UpdateAmmoInfo(currentBullets, maxBullets); // UI'yi güncelle
        Time.timeScale = 1;
    }

    // Mermi olup olmadýðýný kontrol et
    private bool HaveBullets()
    {
        if (currentBullets <= 0)
        {
            return false; // Mermi yok
        }

        currentBullets--; // Mermi sayýsýný azalt
        return true; // Mermi var
    }
}
