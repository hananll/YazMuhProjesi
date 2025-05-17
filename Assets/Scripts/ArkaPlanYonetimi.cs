using UnityEngine;
// using UnityEngine.UI; // Image component'ine doðrudan sprite atamasý yapmayacaðýmýz için bu satýr artýk gerekmeyebilir.

public class ArkaPlanYonetimi : MonoBehaviour
{
    [Tooltip("Bulanýklaþtýrýlmýþ arka plan görselini içeren GameObject")]
    public GameObject blurluArkaPlanGameObject;

    void Start()
    {
        if (blurluArkaPlanGameObject == null)
        {
            Debug.LogError("HATA: ArkaPlanYonetimi - 'blurluArkaPlanGameObject' Inspector'da ATANMAMIÞ! Script düzgün çalýþmayacak.");
            this.enabled = false; // Script'i devre dýþý býrak
            return;
        }

        // Baþlangýçta blurlu arka planý gizle (Inspector'dan zaten false yaptýysanýz bu ek bir güvence)
        blurluArkaPlanGameObject.SetActive(false);
    }

    // Bu fonksiyon blurlu arka planý gösterir veya gizler
    public void BlurluArkaPlaniAyarla(bool aktifEt)
    {
        if (!this.enabled || blurluArkaPlanGameObject == null) return; // Script devredýþýysa veya referans yoksa iþlem yapma

        blurluArkaPlanGameObject.SetActive(aktifEt);
    }
}