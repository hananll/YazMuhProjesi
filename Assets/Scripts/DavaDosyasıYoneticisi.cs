using UnityEngine;
using UnityEngine.UI;

public class DavaDosyasiYoneticisi : MonoBehaviour
{
    [Header("Ana Panel ve Butonlar")]
    public Button davaDosyasiButon;         // Dava dosyasýný açan buton
    public GameObject davaDosyasiAnaPanel;  // Dava dosyasýnýn ana paneli
    public Button davaDosyasiKapatButon;   // Dava dosyasýný kapatan buton

    [Header("Sayfa Yönetimi")]
    public GameObject[] davaDosyasiSayfalari; // Sayfalarý tutan GameObject dizisi
    public Button sonrakiSayfaButon;
    public Button oncekiSayfaButon;
    private int aktifSayfaIndex = 0;

    [Header("Diðer Sistemlerle Entegrasyon")]
    public ArkaPlanYonetimi arkaPlanYoneticisi;  // ArkaPlanYonetimi script'ine referans
    public CanvasGroup arkaPlanCanvasGroup;    // Diðer UI elemanlarýný içeren CanvasGroup'a referans

    void Start()
    {
        // Temel referanslarýn atanýp atanmadýðýný kontrol et
        if (davaDosyasiAnaPanel == null)
            Debug.LogError("HATA: DavaDosyasiYoneticisi - Dava Dosyasý Ana Paneli (davaDosyasiAnaPanel) Inspector'da ATANMAMIÞ!");
        if (davaDosyasiButon == null)
            Debug.LogError("HATA: DavaDosyasiYoneticisi - Dava Dosyasý Butonu (davaDosyasiButon) Inspector'da ATANMAMIÞ!");
        if (davaDosyasiKapatButon == null)
            Debug.LogError("HATA: DavaDosyasiYoneticisi - Dava Dosyasý Kapat Butonu (davaDosyasiKapatButon) Inspector'da ATANMAMIÞ!");

        if (arkaPlanYoneticisi == null)
            Debug.LogError("HATA: DavaDosyasiYoneticisi - Arka Plan Yoneticisi Inspector'da ATANMAMIÞ!");
        if (arkaPlanCanvasGroup == null)
            Debug.LogWarning("UYARI: DavaDosyasiYoneticisi - Arka Plan UI Group (arkaPlanCanvasGroup) Inspector'da ATANMAMIÞ! Diðer UI elemanlarý devre dýþý býrakýlamayacak/etkinleþtirilemeyecek.");

        // Listener'larý ekle
        if (davaDosyasiButon != null)
            davaDosyasiButon.onClick.AddListener(DavaDosyasiPaneliAc);

        if (davaDosyasiKapatButon != null)
            davaDosyasiKapatButon.onClick.AddListener(DavaDosyasiPaneliKapat);

        if (sonrakiSayfaButon != null)
            sonrakiSayfaButon.onClick.AddListener(SonrakiSayfa);
        else if (davaDosyasiSayfalari != null && davaDosyasiSayfalari.Length > 0)
            Debug.LogError("HATA: DavaDosyasiYoneticisi - Sonraki Sayfa Butonu (Dava Dosyasý) Inspector'da atanmamýþ!");

        if (oncekiSayfaButon != null)
            oncekiSayfaButon.onClick.AddListener(OncekiSayfa);
        else if (davaDosyasiSayfalari != null && davaDosyasiSayfalari.Length > 0)
            Debug.LogError("HATA: DavaDosyasiYoneticisi - Önceki Sayfa Butonu (Dava Dosyasý) Inspector'da atanmamýþ!");

        // Dava Dosyasý Paneli baþlangýçta kapalý olsun
        if (davaDosyasiAnaPanel != null)
            davaDosyasiAnaPanel.SetActive(false);

        // Sayfalarýn ve sayfa butonlarýnýn baþlangýç durumunu ayarla
        if (davaDosyasiSayfalari == null || davaDosyasiSayfalari.Length == 0)
        {
            Debug.LogWarning("UYARI: DavaDosyasiYoneticisi - Dava Dosyasý Sayfalar dizisi Inspector'da atanmamýþ veya boþ!");
            if (sonrakiSayfaButon != null) sonrakiSayfaButon.interactable = false;
            if (oncekiSayfaButon != null) oncekiSayfaButon.interactable = false;
        }
        else
        {
            SayfayiGoster(0); // Ýlk sayfayý göster
            if (oncekiSayfaButon != null)
                oncekiSayfaButon.interactable = false;
            if (sonrakiSayfaButon != null)
                sonrakiSayfaButon.interactable = davaDosyasiSayfalari.Length > 1;
        }
    }

    void DavaDosyasiPaneliAc()
    {
        // Debug.Log("Dava dosyasý butonu týklandý."); // Ýsteðe baðlý log
        if (davaDosyasiAnaPanel == null)
        {
            Debug.LogError("HATA: DavaDosyasiYoneticisi - DavaDosyasiPaneliAc çaðrýldý ama davaDosyasiAnaPanel NULL!");
            return;
        }

        davaDosyasiAnaPanel.SetActive(true);
        // Panelin diðer her þeyin üzerinde çizilmesi için (ayný Canvas ve sorting layer içindeyse)
        // davaDosyasiAnaPanel.transform.SetAsLastSibling(); 

        // Arka planý blur yap
        if (arkaPlanYoneticisi != null)
        {
            arkaPlanYoneticisi.BlurluArkaPlaniAyarla(true);
        }
        else
        {
            // Start içinde zaten hata logu var.
        }

        // Diðer UI elemanlarýný devre dýþý býrak
        if (arkaPlanCanvasGroup != null)
        {
            arkaPlanCanvasGroup.interactable = false;
            arkaPlanCanvasGroup.blocksRaycasts = false;
            // arkaPlanCanvasGroup.alpha = 0.7f; // Ýsteðe baðlý: Arka plan UI'ýný soluklaþtýr
        }
    }

    void DavaDosyasiPaneliKapat()
    {
        // Debug.Log("Dava dosyasý kapat butonu týklandý."); // Ýsteðe baðlý log
        if (davaDosyasiAnaPanel == null)
        {
            Debug.LogError("HATA: DavaDosyasiYoneticisi - DavaDosyasiPaneliKapat çaðrýldý ama davaDosyasiAnaPanel NULL!");
            return;
        }

        davaDosyasiAnaPanel.SetActive(false);

        // Arka planý normale döndür
        if (arkaPlanYoneticisi != null)
        {
            arkaPlanYoneticisi.BlurluArkaPlaniAyarla(false);
        }
        else
        {
            // Start içinde zaten hata logu var.
        }

        // Diðer UI elemanlarýný tekrar aktif et
        if (arkaPlanCanvasGroup != null)
        {
            arkaPlanCanvasGroup.interactable = true;
            arkaPlanCanvasGroup.blocksRaycasts = true;
            // arkaPlanCanvasGroup.alpha = 1f; // Ýsteðe baðlý: Arka plan UI'ýnýn solukluðunu geri al
        }
    }

    public void SonrakiSayfa()
    {
        if (davaDosyasiSayfalari == null || davaDosyasiSayfalari.Length == 0) return;

        if (aktifSayfaIndex < davaDosyasiSayfalari.Length - 1)
        {
            aktifSayfaIndex++;
            SayfayiGoster(aktifSayfaIndex);

            if (oncekiSayfaButon != null)
                oncekiSayfaButon.interactable = true;

            if (sonrakiSayfaButon != null && aktifSayfaIndex == davaDosyasiSayfalari.Length - 1)
                sonrakiSayfaButon.interactable = false;
        }
    }

    public void OncekiSayfa()
    {
        if (davaDosyasiSayfalari == null || davaDosyasiSayfalari.Length == 0) return;

        if (aktifSayfaIndex > 0)
        {
            aktifSayfaIndex--;
            SayfayiGoster(aktifSayfaIndex);

            if (sonrakiSayfaButon != null)
                sonrakiSayfaButon.interactable = true;

            if (oncekiSayfaButon != null && aktifSayfaIndex == 0)
                oncekiSayfaButon.interactable = false;
        }
    }

    void SayfayiGoster(int sayfaIndex)
    {
        if (davaDosyasiSayfalari == null || davaDosyasiSayfalari.Length == 0)
        {
            // Debug.LogWarning("UYARI: DavaDosyasiYoneticisi - SayfayiGoster çaðrýldý ama sayfa dizisi boþ veya atanmamýþ.");
            return;
        }

        for (int i = 0; i < davaDosyasiSayfalari.Length; i++)
        {
            if (davaDosyasiSayfalari[i] != null)
            {
                davaDosyasiSayfalari[i].SetActive(false);
            }
        }

        if (sayfaIndex >= 0 && sayfaIndex < davaDosyasiSayfalari.Length && davaDosyasiSayfalari[sayfaIndex] != null)
        {
            davaDosyasiSayfalari[sayfaIndex].SetActive(true);
        }
        else
        {
            Debug.LogError("HATA: DavaDosyasiYoneticisi - Geçersiz dava dosyasý sayfa indeksi (" + sayfaIndex + ") veya ilgili sayfa objesi NULL!");
        }
    }
}