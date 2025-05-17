using UnityEngine;
using UnityEngine.UI;

public class KanunKitabiYoneticisi : MonoBehaviour
{
    [Header("Ana Panel ve Butonlar")]
    public Button kanunKitabiButon;
    public GameObject kanunKitabiPanel;
    public Button kanunKitabiKapatButon;

    [Header("Sayfa Yönetimi")]
    public GameObject[] kanunKitabiSayfalari;
    public Button kanunSonrakiSayfaButon;
    public Button kanunOncekiSayfaButon;
    private int aktifSayfaIndex = 0;

    [Header("Diğer Sistemlerle Entegrasyon")]
    public ArkaPlanYonetimi arkaPlanYoneticisi;
    public CanvasGroup arkaPlanCanvasGroup; // Arka plandaki diğer UI'ları kontrol etmek için

    void Start()
    {
        // Temel referansların atanıp atanmadığını kontrol et
        if (kanunKitabiPanel == null)
            Debug.LogError("HATA: KanunKitabiYoneticisi - Kanun Kitabı Paneli (kanunKitabiPanel) Inspector'da ATANMAMIŞ!");
        if (kanunKitabiButon == null)
            Debug.LogError("HATA: KanunKitabiYoneticisi - Kanun Kitabı Butonu (kanunKitabiButon) Inspector'da ATANMAMIŞ!");
        if (kanunKitabiKapatButon == null)
            Debug.LogError("HATA: KanunKitabiYoneticisi - Kanun Kitabı Kapat Butonu (kanunKitabiKapatButon) Inspector'da ATANMAMIŞ!");

        if (arkaPlanYoneticisi == null)
            Debug.LogError("HATA: KanunKitabiYoneticisi - Arka Plan Yoneticisi Inspector'da ATANMAMIŞ!");
        if (arkaPlanCanvasGroup == null)
            Debug.LogWarning("UYARI: KanunKitabiYoneticisi - Arka Plan UI Group (arkaPlanCanvasGroup) Inspector'da ATANMAMIŞ! Diğer UI elemanları devre dışı bırakılamayacak/etkinleştirilemeyecek.");

        // Listener'ları ekle
        if (kanunKitabiButon != null)
            kanunKitabiButon.onClick.AddListener(KanunKitabiPaneliAc);

        if (kanunKitabiKapatButon != null)
            kanunKitabiKapatButon.onClick.AddListener(KanunKitabiPaneliKapat);

        if (kanunSonrakiSayfaButon != null)
            kanunSonrakiSayfaButon.onClick.AddListener(SonrakiSayfa);
        else if (kanunKitabiSayfalari != null && kanunKitabiSayfalari.Length > 0) // Sadece sayfalar varsa butonun olmaması hata olabilir
            Debug.LogError("HATA: KanunKitabiYoneticisi - Sonraki Sayfa Butonu (kanunSonrakiSayfaButon) Inspector'da atanmamış!");

        if (kanunOncekiSayfaButon != null)
            kanunOncekiSayfaButon.onClick.AddListener(OncekiSayfa);
        else if (kanunKitabiSayfalari != null && kanunKitabiSayfalari.Length > 0) // Sadece sayfalar varsa butonun olmaması hata olabilir
            Debug.LogError("HATA: KanunKitabiYoneticisi - Önceki Sayfa Butonu (kanunOncekiSayfaButon) Inspector'da atanmamış!");

        // Kanun Kitabı Paneli başlangıçta kapalı olsun
        if (kanunKitabiPanel != null)
            kanunKitabiPanel.SetActive(false);

        // Sayfaların ve sayfa butonlarının başlangıç durumunu ayarla
        if (kanunKitabiSayfalari == null || kanunKitabiSayfalari.Length == 0)
        {
            Debug.LogWarning("UYARI: KanunKitabiYoneticisi - Kanun Kitabı Sayfalar dizisi (kanunKitabiSayfalari) Inspector'da atanmamış veya boş!");
            if (kanunSonrakiSayfaButon != null) kanunSonrakiSayfaButon.interactable = false;
            if (kanunOncekiSayfaButon != null) kanunOncekiSayfaButon.interactable = false;
        }
        else
        {
            SayfayiGoster(0); // İlk sayfayı göster (panel kapalıyken bile içerik hazır olsun)
            if (kanunOncekiSayfaButon != null)
                kanunOncekiSayfaButon.interactable = false;
            if (kanunSonrakiSayfaButon != null)
                kanunSonrakiSayfaButon.interactable = kanunKitabiSayfalari.Length > 1;
        }
    }

    void KanunKitabiPaneliAc()
    {
        if (kanunKitabiPanel == null)
        {
            Debug.LogError("HATA: KanunKitabiYoneticisi - KanunKitabiPaneliAc çağrıldı ama kanunKitabiPanel NULL!");
            return;
        }

        kanunKitabiPanel.SetActive(true);
        // Panelin diğer her şeyin üzerinde çizilmesi için (aynı Canvas ve sorting layer içindeyse)
        // kanunKitabiPanel.transform.SetAsLastSibling(); 

        // Arka planı blur yap
        if (arkaPlanYoneticisi != null)
        {
            arkaPlanYoneticisi.BlurluArkaPlaniAyarla(true);
        }
        else
        {
            // Start içinde zaten hata logu var, burada uyarı yeterli olabilir.
            // Debug.LogWarning("KanunKitabiYoneticisi (Acarken): Arka Plan Yoneticisi referansı NULL! Blur yapılamadı.");
        }

        // Diğer UI elemanlarını devre dışı bırak ve soluklaştır
        if (arkaPlanCanvasGroup != null)
        {
            arkaPlanCanvasGroup.interactable = false;
            arkaPlanCanvasGroup.blocksRaycasts = false; // Etkileşimleri tamamen keser
            // arkaPlanCanvasGroup.alpha = 0.5f; // İsteğe bağlı: Arka plan UI'ını %50 soluklaştır
        }
    }

    // KanunKitabiPaneliKapat fonksiyonu KanunKitabiPaneliAc dışına taşındı
    void KanunKitabiPaneliKapat()
    {
        if (kanunKitabiPanel == null)
        {
            Debug.LogError("HATA: KanunKitabiYoneticisi - KanunKitabiPaneliKapat çağrıldı ama kanunKitabiPanel NULL!");
            return;
        }

        kanunKitabiPanel.SetActive(false);

        // Arka planı normale döndür
        if (arkaPlanYoneticisi != null)
        {
            arkaPlanYoneticisi.BlurluArkaPlaniAyarla(false);
        }
        else
        {
            // Start içinde zaten hata logu var.
            // Debug.LogWarning("KanunKitabiYoneticisi (Kapatırken): Arka Plan Yoneticisi referansı NULL! Arka plan normale döndürülemedi.");
        }

        // Diğer UI elemanlarını tekrar aktif et
        if (arkaPlanCanvasGroup != null)
        {
            arkaPlanCanvasGroup.interactable = true;
            arkaPlanCanvasGroup.blocksRaycasts = true;
            // arkaPlanCanvasGroup.alpha = 1f; // İsteğe bağlı: Arka plan UI'ının solukluğunu geri al
        }
    }

    public void SonrakiSayfa()
    {
        if (kanunKitabiSayfalari == null || kanunKitabiSayfalari.Length == 0) return;

        if (aktifSayfaIndex < kanunKitabiSayfalari.Length - 1)
        {
            aktifSayfaIndex++;
            SayfayiGoster(aktifSayfaIndex);

            if (kanunOncekiSayfaButon != null)
                kanunOncekiSayfaButon.interactable = true;

            if (kanunSonrakiSayfaButon != null && aktifSayfaIndex == kanunKitabiSayfalari.Length - 1)
                kanunSonrakiSayfaButon.interactable = false;
        }
    }

    public void OncekiSayfa()
    {
        if (kanunKitabiSayfalari == null || kanunKitabiSayfalari.Length == 0) return;

        if (aktifSayfaIndex > 0)
        {
            aktifSayfaIndex--;
            SayfayiGoster(aktifSayfaIndex);

            if (kanunSonrakiSayfaButon != null)
                kanunSonrakiSayfaButon.interactable = true;

            if (kanunOncekiSayfaButon != null && aktifSayfaIndex == 0)
                kanunOncekiSayfaButon.interactable = false;
        }
    }

    void SayfayiGoster(int sayfaIndex)
    {
        if (kanunKitabiSayfalari == null || kanunKitabiSayfalari.Length == 0)
        {
            Debug.LogWarning("UYARI: KanunKitabiYoneticisi - SayfayiGoster çağrıldı ama sayfa dizisi boş veya atanmamış.");
            return;
        }

        for (int i = 0; i < kanunKitabiSayfalari.Length; i++)
        {
            if (kanunKitabiSayfalari[i] != null)
            {
                kanunKitabiSayfalari[i].SetActive(false);
            }
        }

        if (sayfaIndex >= 0 && sayfaIndex < kanunKitabiSayfalari.Length && kanunKitabiSayfalari[sayfaIndex] != null)
        {
            kanunKitabiSayfalari[sayfaIndex].SetActive(true);
        }
        else
        {
            Debug.LogError("HATA: KanunKitabiYoneticisi - Geçersiz kanun kitabı sayfa indeksi (" + sayfaIndex + ") veya ilgili sayfa objesi NULL!");
        }
    }
}