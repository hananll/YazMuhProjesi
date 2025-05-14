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

    public Image sanikGorselBaslangic; 
    public Button mikrofonButon;

    public float harfHiz = 0.05f;

    public List<KonusmaMetniData> diyalogMetinleri;
    private int mevcutMetinIndex = 0;
    private Coroutine mevcutMetinAnimasyonu;

    void Start()
    {

       hakimKonusmaPanel.SetActive(false);
       sanikKonusmaPanel.SetActive(false);
       sanikGorselBaslangic.gameObject.SetActive(true);
       
       

       mikrofonButon.onClick.AddListener(IlkKonusmayiBaslat);
        
        
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
     
        mikrofonButon.interactable = false;
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


        hakimKonusmaPanel.SetActive(false);
        sanikKonusmaPanel.SetActive(false);
        //Sanığın veya Hakimin Konuşması bittikten sonra panelinin açık kalmaması için yazıldı.

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
        
        hakimKonusmaPanel.SetActive(false);
        sanikKonusmaPanel.SetActive(false); 
        sanikGorselBaslangic.gameObject.SetActive(true);

        mikrofonButon.interactable = true;
        mevcutMetinIndex = 0;

    }
}