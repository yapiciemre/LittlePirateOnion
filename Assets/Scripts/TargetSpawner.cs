using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    // Inspector üzerinden ayarlanabilir hedef sprite dizisi
    [SerializeField] private Sprite[] targetSprite;

    // Hedeflerin rastgele yerleþtirileceði alaný belirlemek için BoxCollider2D referansý
    [SerializeField] private BoxCollider2D cd;

    // Hedef prefab'ýnýn referansý
    [SerializeField] private GameObject targetPrefab;

    // Hedefin oluþturulmasý arasýndaki bekleme süresi (soðuma süresi)
    [SerializeField] private float cooldown;

    private float timer; // Geri sayým için kullanýlacak zamanlayýcý

    private int treasureCreated; // Þu ana kadar oluþturulan hedef sayýsý
    private int treasureMilestone = 10; // Her milestone'da (10 hedef) bir kez cooldown'ý azaltmak için kullanýlan eþik deðeri

    void Update()
    {
        // Zamanlayýcýyý azalt
        timer -= Time.deltaTime;

        // Zamanlayýcý sýfýrýn altýna düþtüyse yeni bir hedef oluþtur
        if (timer < 0)
        {
            timer = cooldown; // Zamanlayýcýyý tekrar soðuma süresi kadar ayarla
            treasureCreated++; // Oluþturulan hedef sayýsýný artýr

            // Eðer oluþturulan hedef sayýsý milestone'u geçtiyse ve soðuma süresi 0.5 saniyeden büyükse
            if (treasureCreated > treasureMilestone && cooldown > .5f)
            {
                treasureMilestone += 10; // Milestone'u 10 artýr
                cooldown -= .3f; // Soðuma süresini 0.3 saniye azalt
            }

            // Yeni bir hedef nesnesi oluþtur
            GameObject newTarget = Instantiate(targetPrefab);

            // Hedefin x pozisyonunu collider'ýn yatay sýnýrlarý arasýnda rastgele seç
            float randomX = Random.Range(cd.bounds.min.x, cd.bounds.max.x);

            // Yeni hedefin pozisyonunu ayarla (x pozisyonunu rastgele, y pozisyonunu sabit tut)
            newTarget.transform.position = new Vector2(randomX, transform.position.y);

            // Hedefin sprite'ýný rastgele bir sprite ile deðiþtir
            int randomIndex = Random.Range(0, targetSprite.Length);
            newTarget.GetComponent<SpriteRenderer>().sprite = targetSprite[randomIndex];
        }
    }
}
