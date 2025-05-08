using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class IlkKonusmaYoneticisi : MonoBehaviour
{
    public GameObject ilkKonusmaBalonuPanel; // Inspector'dan atanacak ilk konuþma balonu paneli
    public TextMeshProUGUI ilkKonusmaMetniTMP; // Inspector'dan atanacak ilk konuþma metni alaný
    public float harfHiz = 0.05f;             // Harflerin gelme hýzý
    public Button ilkDevamEtButon;           // Inspector'dan atanacak "Devam Et" butonu
    public Button mikrofonButon;             // Inspector'dan atanacak mikrofon butonu
    public KonusmaMetniData ilkKonusmaData;   // Inspector'dan atanacak ilk konuþma metni verisi (Scriptable Object)

    private int mevcutHarfSayisi = 0;
    private Coroutine ilkMetinAnimasyonu;

    void Start()
    {
        if (mikrofonButon != null)
        {
            mikrofonButon.onClick.AddListener(MikrofonButonunaTiklandi);
        }
        else
        {
            Debug.LogError("Mikrofon Butonu Inspector'da atanmamýþ!");
        }

        if (ilkDevamEtButon != null)
        {
            ilkDevamEtButon.gameObject.SetActive(false); // Baþlangýçta "Devam Et" butonu kapalý olsun
            ilkDevamEtButon.onClick.AddListener(IlkDevamEtButonunaTiklandi);
        }
        else
        {
            Debug.LogError("Ýlk Devam Et Butonu Inspector'da atanmamýþ!");
        }

        if (ilkKonusmaBalonuPanel != null)
        {
            ilkKonusmaBalonuPanel.SetActive(false); // Baþlangýçta konuþma balonu kapalý olsun
        }
        else
        {
            Debug.LogError("Ýlk Konuþma Balonu Paneli Inspector'da atanmamýþ!");
        }

        if (ilkKonusmaMetniTMP == null)
        {
            Debug.LogError("Ýlk Konuþma Metni TMP Inspector'da atanmamýþ!");
        }
    }

    public void MikrofonButonunaTiklandi()
    {
        Debug.Log("Mikrofon butonuna týklandý!");
        if (ilkKonusmaBalonuPanel != null && ilkKonusmaData != null)
        {
            ilkKonusmaBalonuPanel.SetActive(true);
            mevcutHarfSayisi = 0;
            ilkKonusmaMetniTMP.text = "";
            ilkMetinAnimasyonu = StartCoroutine(MetniHarfHarfGoster(ilkKonusmaData.metin));

            // Mikrofon butonunu devre dýþý býrakabiliriz (isteðe baðlý)
            if (mikrofonButon != null)
            {
                mikrofonButon.interactable = false;
            }
        }
        else
        {
            if (ilkKonusmaBalonuPanel == null)
            {
                Debug.LogError("Ýlk Konuþma Balonu Paneli Inspector'da atanmamýþ!");
            }
            if (ilkKonusmaData == null)
            {
                Debug.LogError("Ýlk Konuþma Verisi (Scriptable Object) Inspector'da atanmamýþ!");
            }
        }
    }

    private IEnumerator MetniHarfHarfGoster(string metin)
    {
        while (mevcutHarfSayisi < metin.Length)
        {
            mevcutHarfSayisi++;
            ilkKonusmaMetniTMP.text = metin.Substring(0, mevcutHarfSayisi);
            yield return new WaitForSeconds(harfHiz);
        }

        // Ýlk metin tamamlandýktan sonra "Devam Et" butonunu aktif et
        if (ilkDevamEtButon != null)
        {
            ilkDevamEtButon.gameObject.SetActive(true);
        }
    }

    public void IlkDevamEtButonunaTiklandi()
    {
        Debug.Log("Ýlk Devam Et butonuna týklandý!");
        // Burada sonraki metinleri veya olaylarý tetikleyebilirsiniz.
        // Þimdilik sadece ilk konuþma balonunu kapatýp bir sonraki aþamaya geçelim.
        if (ilkKonusmaBalonuPanel != null)
        {
            ilkKonusmaBalonuPanel.SetActive(false);
        }
    }
}