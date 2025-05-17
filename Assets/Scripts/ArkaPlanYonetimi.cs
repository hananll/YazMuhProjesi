using UnityEngine;
// using UnityEngine.UI; // Image component'ine do�rudan sprite atamas� yapmayaca��m�z i�in bu sat�r art�k gerekmeyebilir.

public class ArkaPlanYonetimi : MonoBehaviour
{
    [Tooltip("Bulan�kla�t�r�lm�� arka plan g�rselini i�eren GameObject")]
    public GameObject blurluArkaPlanGameObject;

    void Start()
    {
        if (blurluArkaPlanGameObject == null)
        {
            Debug.LogError("HATA: ArkaPlanYonetimi - 'blurluArkaPlanGameObject' Inspector'da ATANMAMI�! Script d�zg�n �al��mayacak.");
            this.enabled = false; // Script'i devre d��� b�rak
            return;
        }

        // Ba�lang��ta blurlu arka plan� gizle (Inspector'dan zaten false yapt�ysan�z bu ek bir g�vence)
        blurluArkaPlanGameObject.SetActive(false);
    }

    // Bu fonksiyon blurlu arka plan� g�sterir veya gizler
    public void BlurluArkaPlaniAyarla(bool aktifEt)
    {
        if (!this.enabled || blurluArkaPlanGameObject == null) return; // Script devred���ysa veya referans yoksa i�lem yapma

        blurluArkaPlanGameObject.SetActive(aktifEt);
    }
}