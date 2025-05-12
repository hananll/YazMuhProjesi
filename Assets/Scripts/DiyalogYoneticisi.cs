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

    public Image sanikGorselBaslangic; // Dava ba�lar ba�lamaz g�r�nen san�k g�rseli (panel d���nda)
    public Button mikrofonButon;

    public float harfHiz = 0.05f;

    public List<KonusmaMetniData> diyalogMetinleri;
    private int mevcutMetinIndex = 0;
    private Coroutine mevcutMetinAnimasyonu;

    void Start()
    {
        // Panelleri ba�lang��ta kapat
        if (hakimKonusmaPanel != null) hakimKonusmaPanel.SetActive(false);
        if (sanikKonusmaPanel != null) sanikKonusmaPanel.SetActive(false);

        // Ba�lang�� san�k g�rselini aktif et (Inspector'da atanm�� olmal�)
        if (sanikGorselBaslangic != null) sanikGorselBaslangic.gameObject.SetActive(true);

        // Mikrofon butonuna t�klama olay�n� ba�la
        if (mikrofonButon != null)
        {
            mikrofonButon.onClick.AddListener(IlkKonusmayiBaslat);
        }
        else
        {
            Debug.LogError("Mikrofon Butonu Inspector'da atanmam��!");
        }

        // Devam et butonlar�n�n olaylar�n� ba�la
        if (hakimDevamEtButon != null) hakimDevamEtButon.onClick.AddListener(SonrakiMetniGoster);
        if (sanikDevamEtButon != null) sanikDevamEtButon.onClick.AddListener(SonrakiMetniGoster);

        // Diyalog metinleri kontrol�
        if (diyalogMetinleri == null || diyalogMetinleri.Count == 0)
        {
            Debug.LogError("Diyalog Metinleri Inspector'da atanmam�� veya bo�!");
        }
    }

    void IlkKonusmayiBaslat()
    {
        Debug.Log("�lk konu�ma ba�lat�l�yor.");
        if (mikrofonButon != null)
        {
            mikrofonButon.interactable = false;
        }
        // �lk diyalog ba�lad���nda ba�lang�� san�k g�rselini gizle (iste�e ba�l�)
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

        // Konu�mac�ya g�re paneli aktif et ve metni g�ster
        if (mevcutKonusma.konusmaciAdi == "Hakim")
        {
            if (hakimKonusmaPanel != null) hakimKonusmaPanel.SetActive(true);
            if (hakimAdiText != null) hakimAdiText.text = mevcutKonusma.konusmaciAdi;
            if (hakimMetinText != null) mevcutMetinAnimasyonu = StartCoroutine(MetniHarfHarfGoster(hakimMetinText, mevcutKonusma.metin));
        }
        else if (mevcutKonusma.konusmaciAdi == "Sanık")
        {
            if (sanikKonusmaPanel != null) sanikKonusmaPanel.SetActive(true);
            if (sanikAdiText != null) sanikAdiText.text = mevcutKonusma.konusmaciAdi;
            if (sanikMetinText != null) mevcutMetinAnimasyonu = StartCoroutine(MetniHarfHarfGoster(sanikMetinText, mevcutKonusma.metin));
        }
        else
        {
            Debug.LogError("Bilinmeyen konu�mac�: " + mevcutKonusma.konusmaciAdi);
        }

        // Devam et butonlar�n� inaktif yap
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

        // Animasyon bittikten sonra do�ru devam et butonunu aktif et
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
        // Diyalog bittikten sonra ba�lang�� san�k g�rselini tekrar g�r�n�r yapabiliriz (iste�e ba�l�)
        if (sanikGorselBaslangic != null) sanikGorselBaslangic.gameObject.SetActive(true);
        if (mikrofonButon != null) mikrofonButon.interactable = true;
        mevcutMetinIndex = 0;
    }
}