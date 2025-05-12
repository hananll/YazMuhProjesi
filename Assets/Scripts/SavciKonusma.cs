using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SavciKonusma : MonoBehaviour
{
    public GameObject savciKonusmaPanel;
    public TextMeshProUGUI savciMetinText;
    public TextMeshProUGUI savciAdiText;
    public Button savciKapatButon;
    public Button savciButon;
    public Button savciDevamEtButon; // Yeni: Devam Et butonu

    public float harfHiz = 0.05f;

    public List<KonusmaMetniData> savciDiyalogMetinleri; // Birden fazla diyalog için liste
    private int mevcutMetinIndex = 0;
    private Coroutine mevcutMetinAnimasyonu;

    void Start()
    {
        // Savcý Paneli Baþlangýçta Kapalý Olacak
        if (savciKonusmaPanel != null) savciKonusmaPanel.SetActive(false);

        // Savcý Butonuna Týklama Olayýný Baðla
        if (savciButon != null)
        {
            savciButon.onClick.AddListener(KonusmayiBaslat);
        }
        else
        {
            Debug.LogError("Savcý Butonu Inspector'da atanmamýþ!");
        }

        // Kapat Butonuna Týklama Olayýný Baðla
        if (savciKapatButon != null)
        {
            savciKapatButon.onClick.AddListener(KonusmayiBitir);
        }
        else
        {
            Debug.LogError("Savcý Kapat Butonu Inspector'da atanmamýþ!");
        }

        // Devam Et Butonuna Týklama Olayýný Baðla
        if (savciDevamEtButon != null)
        {
            savciDevamEtButon.onClick.AddListener(SonrakiMetniGoster);
            savciDevamEtButon.gameObject.SetActive(false); // Baþlangýçta kapalý
        }
        else
        {
            Debug.LogError("Savcý Devam Et Butonu Inspector'da atanmamýþ!");
        }

        // Diyalog metinleri kontrolü
        if (savciDiyalogMetinleri == null || savciDiyalogMetinleri.Count == 0)
        {
            Debug.LogError("Savcý Diyalog Metinleri (Scriptable Object Listesi) Inspector'da atanmamýþ veya boþ!");
        }
    }

    void KonusmayiBaslat()
    {
        Debug.Log("Savcý konuþmasý baþlatýlýyor.");
        if (savciButon != null)
        {
            savciButon.interactable = false; // Baþlangýç butonunu devre dýþý býrak
        }

        if (savciKonusmaPanel != null && savciDiyalogMetinleri != null && savciDiyalogMetinleri.Count > 0)
        {
            savciKonusmaPanel.SetActive(true);
            mevcutMetinIndex = 0;
            MevcutMetniGoster();
        }
        else
        {
            Debug.LogError("Savcý konuþmasý baþlatýlamadý!");
        }
    }

    void SonrakiMetniGoster()
    {
        mevcutMetinIndex++;
        MevcutMetniGoster();
    }

    void MevcutMetniGoster()
    {
        if (mevcutMetinIndex < savciDiyalogMetinleri.Count)
        {
            KonusmaMetniData mevcutKonusma = savciDiyalogMetinleri[mevcutMetinIndex];

            // Önceki animasyonu durdur
            if (mevcutMetinAnimasyonu != null)
            {
                StopCoroutine(mevcutMetinAnimasyonu);
            }

            savciMetinText.text = ""; // Metin alanýný temizle
            savciAdiText.text = mevcutKonusma.konusmaciAdi;
            mevcutMetinAnimasyonu = StartCoroutine(MetniHarfHarfGoster(savciMetinText, mevcutKonusma.metin));
            savciDevamEtButon.gameObject.SetActive(false); // Yeni metin baþladýðýnda devam et butonu kapalý
        }
        else
        {
            KonusmayiBitir(); // Tüm metinler gösterildiyse konuþmayý bitir
        }
    }

    IEnumerator MetniHarfHarfGoster(TextMeshProUGUI metinAlani, string metin)
    {
        int harfSayisi = 0;
        metinAlani.text = "";
        while (harfSayisi < metin.Length)
        {
            harfSayisi++;
            metinAlani.text = metin.Substring(0, harfSayisi);
            yield return new WaitForSeconds(harfHiz);
        }
        savciDevamEtButon.gameObject.SetActive(true); // Metin tamamlandýktan sonra devam et butonu aktif
    }

    void KonusmayiBitir()
    {
        Debug.Log("Savcý konuþmasý bitti.");
        if (savciKonusmaPanel != null)
        {
            savciKonusmaPanel.SetActive(false);
        }
        if (savciButon != null)
        {
            savciButon.interactable = true; // Baþlangýç butonunu tekrar aktif et (isteðe baðlý)
        }
        mevcutMetinIndex = 0; // Konuþma tekrar baþlatýlýrsa indeksi sýfýrla
    }
}