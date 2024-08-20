using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // 2D bir collider ba�ka bir collider ile temas etti�inde tetiklenir
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // E�er �arp��an nesnenin etiketi "Target" veya "Player" ise
        if (collision.tag == "Target" || collision.tag == "Player")
        {
            // UI s�n�f�ndaki OpenGameOverScreen metodunu �a��rarak oyun sonu ekran�n� a�
            UI.instance.OpenGameOverScreen();
        }
    }
}
