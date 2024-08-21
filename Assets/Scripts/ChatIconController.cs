using System.Collections;
using UnityEngine;

public class ChatIconController : MonoBehaviour
{
    public GameObject chatIcon; // ChatIcon'u temsil eden GameObject
    public float displayDuration = 30f; // ChatIcon'un ka� saniye boyunca g�r�nece�i
    private bool hasShownChatIcon = false; // ChatIcon'un g�sterilip g�sterilmedi�ini izlemek i�in bir flag

    private void Start()
    {
        chatIcon.SetActive(false); // Oyun ba�lad���nda ChatIcon gizli
    }

    public void ShowChatIcon()
    {
        if (!hasShownChatIcon) // ChatIcon daha �nce g�sterilmemi�se
        {
            chatIcon.SetActive(true); // ChatIcon'u g�r�n�r yap
            StartCoroutine(HideChatIconAfterDelay()); // 30 saniye sonra ChatIcon'u gizleyen Coroutine ba�lat
            hasShownChatIcon = true; // Art�k ChatIcon g�sterildi
        }
    }

    private IEnumerator HideChatIconAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration); // Belirtilen s�reyi bekle
        chatIcon.SetActive(false); // ChatIcon'u gizle
    }
}
