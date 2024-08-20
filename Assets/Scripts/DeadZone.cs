using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // 2D bir collider baþka bir collider ile temas ettiðinde tetiklenir
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eðer çarpýþan nesnenin etiketi "Target" veya "Player" ise
        if (collision.tag == "Target" || collision.tag == "Player")
        {
            // UI sýnýfýndaki OpenGameOverScreen metodunu çaðýrarak oyun sonu ekranýný aç
            UI.instance.OpenGameOverScreen();
        }
    }
}
