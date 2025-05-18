// Script Adý: ArkaPlanYonetimi.cs
using UnityEngine;

public class ArkaPlanYonetimi : MonoBehaviour
{
    [Tooltip("Bulanýklaþtýrýlmýþ arka plan görselini içeren GameObject")]
    public GameObject blurluArkaPlanGameObject;

    public static ArkaPlanYonetimi Ornek { get; private set; }

    void Awake()
    {
        Debug.Log("ArkaPlanYonetimi: Awake() ÇALIÞTI.");
        if (Ornek == null)
        {
            Ornek = this;
            Debug.Log("ArkaPlanYonetimi: Ornek (Singleton) ATANDI.");
        }
        else if (Ornek != this)
        {
            Debug.LogWarning("ArkaPlanYonetimi: Zaten bir örnek var, bu kopya yok ediliyor: " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        if (blurluArkaPlanGameObject == null)
        {
            Debug.LogError("HATA (ArkaPlanYonetimi): 'blurluArkaPlanGameObject' Inspector'da ATANMAMIÞ! Script düzgün çalýþmayacak.");
            this.enabled = false;
            return;
        }
        blurluArkaPlanGameObject.SetActive(false); // Baþlangýçta kesin kapalý
        Debug.Log("ArkaPlanYonetimi: Blurlu arka plan baþlangýçta de-aktif edildi.");
    }

    public void BlurluArkaPlaniAyarla(bool aktifEt)
    {
        Debug.Log("ArkaPlanYonetimi: BlurluArkaPlaniAyarla(" + aktifEt + ") ÇAÐRILDI. Obje: " + (blurluArkaPlanGameObject != null ? blurluArkaPlanGameObject.name : "NULL"));
        if (!this.enabled || blurluArkaPlanGameObject == null)
        {
            Debug.LogError("HATA (ArkaPlanYonetimi): BlurluArkaPlaniAyarla çaðrýldý ama script devredýþý veya blurluArkaPlanGameObject NULL.");
            return;
        }

        blurluArkaPlanGameObject.SetActive(aktifEt);
        Debug.Log("ArkaPlanYonetimi: " + blurluArkaPlanGameObject.name + " SetActive(" + aktifEt + ") YAPILDI.");
    }
}