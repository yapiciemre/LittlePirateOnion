using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Rigidbody2D bileþenine kolay eriþim saðlamak için property kullanýmý
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();

    void Update()
    {
        // Kurþunun yönünü, hýz vektörüne göre ayarla
        transform.right = rb.velocity;
    }

    // Kurþun baþka bir 2D collider ile temas ettiðinde tetiklenir
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eðer çarpýþýlan nesnenin etiketi "Target" ise
        if (collision.tag == "Target")
        {
            Destroy(gameObject); // Kurþunu yok et
            Destroy(collision.gameObject); // Çarptýðý hedefi yok et

            UI.instance.AddScore(); // Skoru artýrmak için UI sýnýfýndaki AddScore metodunu çaðýr
        }
    }
}
