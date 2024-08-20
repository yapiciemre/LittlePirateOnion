using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Rigidbody2D bile�enine kolay eri�im sa�lamak i�in property kullan�m�
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();

    void Update()
    {
        // Kur�unun y�n�n�, h�z vekt�r�ne g�re ayarla
        transform.right = rb.velocity;
    }

    // Kur�un ba�ka bir 2D collider ile temas etti�inde tetiklenir
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // E�er �arp���lan nesnenin etiketi "Target" ise
        if (collision.tag == "Target")
        {
            Destroy(gameObject); // Kur�unu yok et
            Destroy(collision.gameObject); // �arpt��� hedefi yok et

            UI.instance.AddScore(); // Skoru art�rmak i�in UI s�n�f�ndaki AddScore metodunu �a��r
        }
    }
}
