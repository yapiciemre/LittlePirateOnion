using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    // Inspector �zerinden ayarlanabilir hedef sprite dizisi
    [SerializeField] private Sprite[] targetSprite;

    // Hedeflerin rastgele yerle�tirilece�i alan� belirlemek i�in BoxCollider2D referans�
    [SerializeField] private BoxCollider2D cd;

    // Hedef prefab'�n�n referans�
    [SerializeField] private GameObject targetPrefab;

    // Hedefin olu�turulmas� aras�ndaki bekleme s�resi (so�uma s�resi)
    [SerializeField] private float cooldown;

    private float timer; // Geri say�m i�in kullan�lacak zamanlay�c�

    private int treasureCreated; // �u ana kadar olu�turulan hedef say�s�
    private int treasureMilestone = 10; // Her milestone'da (10 hedef) bir kez cooldown'� azaltmak i�in kullan�lan e�ik de�eri

    void Update()
    {
        // Zamanlay�c�y� azalt
        timer -= Time.deltaTime;

        // Zamanlay�c� s�f�r�n alt�na d��t�yse yeni bir hedef olu�tur
        if (timer < 0)
        {
            timer = cooldown; // Zamanlay�c�y� tekrar so�uma s�resi kadar ayarla
            treasureCreated++; // Olu�turulan hedef say�s�n� art�r

            // E�er olu�turulan hedef say�s� milestone'u ge�tiyse ve so�uma s�resi 0.5 saniyeden b�y�kse
            if (treasureCreated > treasureMilestone && cooldown > .5f)
            {
                treasureMilestone += 10; // Milestone'u 10 art�r
                cooldown -= .3f; // So�uma s�resini 0.3 saniye azalt
            }

            // Yeni bir hedef nesnesi olu�tur
            GameObject newTarget = Instantiate(targetPrefab);

            // Hedefin x pozisyonunu collider'�n yatay s�n�rlar� aras�nda rastgele se�
            float randomX = Random.Range(cd.bounds.min.x, cd.bounds.max.x);

            // Yeni hedefin pozisyonunu ayarla (x pozisyonunu rastgele, y pozisyonunu sabit tut)
            newTarget.transform.position = new Vector2(randomX, transform.position.y);

            // Hedefin sprite'�n� rastgele bir sprite ile de�i�tir
            int randomIndex = Random.Range(0, targetSprite.Length);
            newTarget.GetComponent<SpriteRenderer>().sprite = targetSprite[randomIndex];
        }
    }
}
