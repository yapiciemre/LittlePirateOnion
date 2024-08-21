using System.Collections;
using UnityEngine;

public class ChatIconController : MonoBehaviour
{
    public GameObject chatIcon; // ChatIcon'u temsil eden GameObject
    public float displayDuration = 30f; // ChatIcon'un kaç saniye boyunca görüneceði
    private bool hasShownChatIcon = false; // ChatIcon'un gösterilip gösterilmediðini izlemek için bir flag

    private void Start()
    {
        chatIcon.SetActive(false); // Oyun baþladýðýnda ChatIcon gizli
    }

    public void ShowChatIcon()
    {
        if (!hasShownChatIcon) // ChatIcon daha önce gösterilmemiþse
        {
            chatIcon.SetActive(true); // ChatIcon'u görünür yap
            StartCoroutine(HideChatIconAfterDelay()); // 30 saniye sonra ChatIcon'u gizleyen Coroutine baþlat
            hasShownChatIcon = true; // Artýk ChatIcon gösterildi
        }
    }

    private IEnumerator HideChatIconAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration); // Belirtilen süreyi bekle
        chatIcon.SetActive(false); // ChatIcon'u gizle
    }
}
