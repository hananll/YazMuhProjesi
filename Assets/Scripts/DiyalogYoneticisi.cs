using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DiyalogYoneticisi : MonoBehaviour
{
    public GameObject hakimKonusmaPanel;
    public TextMeshProUGUI hakimMetinText;
    public TextMeshProUGUI hakimAdiText;
    public Button hakimDevamEtButon;

    public GameObject sanikKonusmaPanel;
    public TextMeshProUGUI sanikMetinText;
    public TextMeshProUGUI sanikAdiText;
    public Button sanikDevamEtButon;

    public Image sanikGorselBaslangic; // Dava baþlar baþlamaz görünen sanýk görseli (panel dýþýnda)
    public Button mikrofonButon;

    public float harfHiz = 0.05f;

    public List<KonusmaMetniData> diyalogMetinleri;
    private int mevcutMetinIndex = 0;
    private Coroutine mevcutMetinAnimasyonu;

    void Start()
    {
        // Panelleri baþlangýçta kapat
        if (hakimKonusmaPanel != null) hakimKonusmaPanel.SetActive(false);
        if (sanikKonusmaPanel != null) sanikKonusmaPanel.SetActive(false);

        // Baþlangýç sanýk görselini aktif et (Inspector'da atanmýþ olmalý)
        if (sanikGorselBaslangic != null) sanikGorselBaslangic.gameObject.SetActive(true);

        // Mikrofon butonuna týklama olayýný baðla
        if (mikrofonButon != null)
        {
            mikrofonButon.onClick.AddListener(IlkKonusmayiBaslat);
        }
        else
        {
            Debug.LogError("Mikrofon Butonu Inspector'da atanmamýþ!");
        }

        // Devam et butonlarýnýn olaylarýný baðla
        if (hakimDevamEtButon != null) hakimDevamEtButon.onClick.AddListener(SonrakiMetniGoster);
        if (sanikDevamEtButon != null) sanikDevamEtButon.onClick.AddListener(SonrakiMetniGoster);

        // Diyalog metinleri kontrolü
        if (diyalogMetinleri == null || diyalogMetinleri.Count == 0)
        {
            Debug.LogError("Diyalog Metinleri Inspector'da atanmamýþ veya boþ!");
        }
    }

    void IlkKonusmayiBaslat()
    {
        Debug.Log("Ýlk konuþma baþlatýlýyor.");
        if (mikrofonButon != null)
        {
            mikrofonButon.interactable = false;
        }
        // Ýlk diyalog baþladýðýnda baþlangýç sanýk görselini gizle (isteðe baðlý)
        // if (sanikGorselBaslangic != null) sanikGorselBaslangic.gameObject.SetActive(false);
        mevcutMetinIndex = 0;
        MevcutMetniGoster();
    }

    void SonrakiMetniGoster()
    {
        mevcutMetinIndex++;
        if (mevcutMetinIndex < diyalogMetinleri.Count)
        {
            MevcutMetniGoster();
        }
        else
        {
            DiyaloguBitir();
        }
    }

    void MevcutMetniGoster()
    {
        if (mevcutMetinIndex >= diyalogMetinleri.Count)
        {
            DiyaloguBitir();
            return;
        }

        KonusmaMetniData mevcutKonusma = diyalogMetinleri[mevcutMetinIndex];

        // Panelleri kapat
        if (hakimKonusmaPanel != null) hakimKonusmaPanel.SetActive(false);
        if (sanikKonusmaPanel != null) sanikKonusmaPanel.SetActive(false);

        // Konuþmacýya göre paneli aktif et ve metni göster
        if (mevcutKonusma.konusmaciAdi == "Hakim")
        {
            if (hakimKonusmaPanel != null) hakimKonusmaPanel.SetActive(true);
            if (hakimAdiText != null) hakimAdiText.text = mevcutKonusma.konusmaciAdi;
            if (hakimMetinText != null) mevcutMetinAnimasyonu = StartCoroutine(MetniHarfHarfGoster(hakimMetinText, mevcutKonusma.metin));
        }
        else if (mevcutKonusma.konusmaciAdi == "Sanýk")
        {
            if (sanikKonusmaPanel != null) sanikKonusmaPanel.SetActive(true);
            if (sanikAdiText != null) sanikAdiText.text = mevcutKonusma.konusmaciAdi;
            if (sanikMetinText != null) mevcutMetinAnimasyonu = StartCoroutine(MetniHarfHarfGoster(sanikMetinText, mevcutKonusma.metin));
        }
        else
        {
            Debug.LogError("Bilinmeyen konuþmacý: " + mevcutKonusma.konusmaciAdi);
        }

        // Devam et butonlarýný inaktif yap
        if (hakimDevamEtButon != null) hakimDevamEtButon.interactable = false;
        if (sanikDevamEtButon != null) sanikDevamEtButon.interactable = false;
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

        // Animasyon bittikten sonra doðru devam et butonunu aktif et
        if (metinAlani == hakimMetinText && hakimDevamEtButon != null && hakimKonusmaPanel.activeSelf)
        {
            hakimDevamEtButon.interactable = true;
        }
        else if (metinAlani == sanikMetinText && sanikDevamEtButon != null && sanikKonusmaPanel.activeSelf)
        {
            sanikDevamEtButon.interactable = true;
        }
    }

    void DiyaloguBitir()
    {
        Debug.Log("Diyalog sona erdi.");
        if (hakimKonusmaPanel != null) hakimKonusmaPanel.SetActive(false);
        if (sanikKonusmaPanel != null) sanikKonusmaPanel.SetActive(false);
        // Diyalog bittikten sonra baþlangýç sanýk görselini tekrar görünür yapabiliriz (isteðe baðlý)
        if (sanikGorselBaslangic != null) sanikGorselBaslangic.gameObject.SetActive(true);
        if (mikrofonButon != null) mikrofonButon.interactable = true;
        mevcutMetinIndex = 0;
    }
}