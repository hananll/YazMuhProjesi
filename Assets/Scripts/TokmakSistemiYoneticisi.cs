// Script Adı: TokmakSistemiYoneticisi.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TokmakSistemiYoneticisi : MonoBehaviour
{
    [Header("Ana Açılış Butonu")]
    [Tooltip("Oyun sahnesindeki ana 'Tokmak Vur' veya 'Karar Aşamasına Geç' butonu.")]
    public Button anaTokmakButonu;

    [Header("Paneller")]
    [Tooltip("Tokmağa tıklandığında açılacak olan ana seçenekler paneli")]
    public GameObject tokmakKararPanel;
    [Tooltip("Erteleme seçeneklerini veya mesajını gösteren panel")]
    public GameObject erteleMesajPanel;

    [Header("Tokmak Karar Paneli İçindeki Butonlar")] // YENİ HEADER VE ALANLAR
    [Tooltip("'tokmakKararPanel' içindeki 'Davayı Ertele' butonu")]
    public Button secenekErteleButonu;
    [Tooltip("'tokmakKararPanel' içindeki 'Davayı Düşür' butonu")]
    public Button secenekDusurButonu;
    [Tooltip("'tokmakKararPanel' içindeki 'Karar Ver' (giriş panelini açacak) butonu")]
    public Button secenekKararVerButonu;

    [Header("Erteleme Paneli Butonları")]
    public Button ertelemeKapatButton;
    public Button ertelemeTutukluDevamKararButonu;
    public Button ertelemeTutuksuzYargilamaKararButonu;

    [Header("Karar Giriş Paneli Entegrasyonu")]
    public KararGirisPaneliYoneticisi kararGirisPaneli;

    [Header("Bağlantılı Sistemler")]
    public BarYoneticisi barYoneticisi;
    public ArkaPlanYonetimi arkaPlanYoneticisi;
    public CanvasGroup digerAnaUIArayuzuCanvasGroup;

    [Header("Aktif Dava Verisi")]
    public DavaVerisiSO aktifDava;

    void Start()
    {
        Debug.Log("TokmakSistemiYoneticisi: Start() ÇALIŞTI.");

        // Temel referans kontrolleri
        if (tokmakKararPanel == null) Debug.LogError("HATA: TokmakSistemiYoneticisi - Tokmak Karar Paneli ATANMAMIŞ!");
        if (erteleMesajPanel == null) Debug.LogError("HATA: TokmakSistemiYoneticisi - Ertele Mesaj Paneli ATANMAMIŞ!");
        if (kararGirisPaneli == null) Debug.LogError("HATA: TokmakSistemiYoneticisi - Karar Giriş Paneli Yöneticisi ATANMAMIŞ!");

        // Tokmak Karar Paneli içindeki butonların referans kontrolü
        if (secenekErteleButonu == null) Debug.LogError("HATA: TokmakSistemiYoneticisi - Seçenek Ertele Butonu ATANMAMIŞ!");
        if (secenekDusurButonu == null) Debug.LogError("HATA: TokmakSistemiYoneticisi - Seçenek Düşür Butonu ATANMAMIŞ!");
        if (secenekKararVerButonu == null) Debug.LogError("HATA: TokmakSistemiYoneticisi - Seçenek Karar Ver Butonu ATANMAMIŞ!");

        // Diğer referans kontrolleri...
        if (barYoneticisi == null) barYoneticisi = BarYoneticisi.Ornek;
        if (barYoneticisi == null) Debug.LogError("HATA: TokmakSistemiYoneticisi - Bar Yöneticisi bulunamadı!");
        if (arkaPlanYoneticisi == null) arkaPlanYoneticisi = ArkaPlanYonetimi.Ornek;
        if (arkaPlanYoneticisi == null) Debug.LogWarning("UYARI: TokmakSistemiYoneticisi - Arka Plan Yöneticisi bulunamadı.");
        if (digerAnaUIArayuzuCanvasGroup == null) Debug.LogWarning("UYARI: TokmakSistemiYoneticisi - Diğer Ana UI CanvasGroup ATANMAMIŞ.");
        if (aktifDava == null) Debug.LogWarning("UYARI: TokmakSistemiYoneticisi - Oyun başında Aktif Dava Verisi ATANMAMIŞ!");

        // Listener'lar
        if (anaTokmakButonu != null)
        {
            anaTokmakButonu.onClick.AddListener(TokmakSecenekPaneliAc);
            Debug.Log("TokmakSistemiYoneticisi: 'anaTokmakButonu' için Listener EKLENDİ -> TokmakSecenekPaneliAc");
        }
        else
        {
            Debug.LogWarning("TokmakSistemiYoneticisi: 'anaTokmakButonu' atanmamış. TokmakSecenekPaneliAc() başka bir yerden çağrılmalı.");
        }

        // Tokmak Karar Paneli içindeki butonlar için listener'lar
        if (secenekErteleButonu != null)
            secenekErteleButonu.onClick.AddListener(ErteleButonunaTiklandi);
        if (secenekDusurButonu != null)
            secenekDusurButonu.onClick.AddListener(DusurButonunaTiklandi);
        if (secenekKararVerButonu != null)
            secenekKararVerButonu.onClick.AddListener(KararVerSecenegiTiklandi);

        // Erteleme Paneli butonları için listener'lar
        if (ertelemeKapatButton != null) ertelemeKapatButton.onClick.AddListener(ErtelemeMesajPaneliKapat);
        if (ertelemeTutukluDevamKararButonu != null) ertelemeTutukluDevamKararButonu.onClick.AddListener(ErtelemeKarari_TutukluDevam);
        if (ertelemeTutuksuzYargilamaKararButonu != null) ertelemeTutuksuzYargilamaKararButonu.onClick.AddListener(ErtelemeKarari_TutuksuzYargilama);

        // Panelleri başlangıçta kapat
        if (tokmakKararPanel != null) tokmakKararPanel.SetActive(false);
        if (erteleMesajPanel != null) erteleMesajPanel.SetActive(false);
    }

    public void AktifDavayiAyarla(DavaVerisiSO yeniDava)
    {
        aktifDava = yeniDava;
        if (aktifDava == null) Debug.LogError("HATA: TokmakSistemiYoneticisi - Aktif dava NULL olarak ayarlandı!");
    }

    public void TokmakSecenekPaneliAc()
    {
        Debug.Log("TokmakSistemiYoneticisi: TokmakSecenekPaneliAc() ÇAĞRILDI.");
        if (aktifDava == null || tokmakKararPanel == null)
        {
            Debug.LogError("HATA: TokmakSistemiYoneticisi - Aktif dava veya tokmakKararPanel atanmamış, panel açılamıyor!");
            return;
        }
        tokmakKararPanel.SetActive(true);
        ArkaPlanVeDigerUIAyarla(true);
    }

    // Bu metot, 'secenekErteleButonu' tarafından çağrılacak
    public void ErteleButonunaTiklandi()
    {
        Debug.Log("TokmakSistemiYoneticisi: ErteleButonunaTiklandi() ÇAĞRILDI.");
        if (erteleMesajPanel == null || tokmakKararPanel == null) return;
        erteleMesajPanel.SetActive(true);
        tokmakKararPanel.SetActive(false);
        

    }

    public void ErtelemeKarari_TutukluDevam()
    {
        Debug.Log("TokmakSistemiYoneticisi: ErtelemeKarari_TutukluDevam() ÇAĞRILDI.");
        if (barYoneticisi == null || aktifDava == null) return;
        barYoneticisi.DegistirKamuoyuGuven(aktifDava.detaylar.ertelemeTutuklu_KamuoyuEtkisi);
        barYoneticisi.DegistirHukukGuven(aktifDava.detaylar.ertelemeTutuklu_HukukEtkisi);
        KapatVeGenelDurumuNormaleDonder(erteleMesajPanel);
        SceneManager.LoadScene(1);

    }

    public void ErtelemeKarari_TutuksuzYargilama()
    {
        Debug.Log("TokmakSistemiYoneticisi: ErtelemeKarari_TutuksuzYargilama() ÇAĞRILDI.");
        if (barYoneticisi == null || aktifDava == null) return;
        barYoneticisi.DegistirKamuoyuGuven(aktifDava.detaylar.ertelemeTutuksuz_KamuoyuEtkisi);
        barYoneticisi.DegistirHukukGuven(aktifDava.detaylar.ertelemeTutuksuz_HukukEtkisi);
        KapatVeGenelDurumuNormaleDonder(erteleMesajPanel);
        SceneManager.LoadScene(1);

    }

    // Bu metot, 'secenekDusurButonu' tarafından çağrılacak
    public void DusurButonunaTiklandi()
    {
        Debug.Log("TokmakSistemiYoneticisi: DusurButonunaTiklandi() ÇAĞRILDI.");
        if (barYoneticisi == null || aktifDava == null) return;
        if (aktifDava.detaylar.davayiDusurmekGecerliMi)
        {
            barYoneticisi.DegistirKamuoyuGuven(aktifDava.detaylar.davayiDusurGecerli_KamuoyuEtkisi);
            barYoneticisi.DegistirHukukGuven(aktifDava.detaylar.davayiDusurGecerli_HukukEtkisi);
        }
        else
        {
            barYoneticisi.DegistirKamuoyuGuven(aktifDava.detaylar.davayiDusurGecersiz_KamuoyuEtkisi);
            barYoneticisi.DegistirHukukGuven(aktifDava.detaylar.davayiDusurGecersiz_HukukEtkisi);
        }
        KapatVeGenelDurumuNormaleDonder(tokmakKararPanel);
        SceneManager.LoadScene(1);

    }

    // Bu metot, 'secenekKararVerButonu' tarafından çağrılacak
    public void KararVerSecenegiTiklandi()
    {
        Debug.Log("TokmakSistemiYoneticisi: KararVerSecenegiTiklandi() ÇAĞRILDI.");
        if (kararGirisPaneli == null || aktifDava == null)
        {
            Debug.LogError("HATA: TokmakSistemiYoneticisi - Karar Giriş Paneli Yöneticisi veya Aktif Dava atanmamış!");
            return;
        }
        kararGirisPaneli.KararPaneliniGoster(
            aktifDava.detaylar.idealHapisGun,
            aktifDava.detaylar.idealParaCezasi,
            aktifDava.detaylar.idealBeraatOlmalydi
        );
        if (tokmakKararPanel != null)
            tokmakKararPanel.SetActive(false);
    }

    public void ErtelemeMesajPaneliKapat()
    {
        Debug.Log("TokmakSistemiYoneticisi: ErtelemeMesajPaneliKapat() ÇAĞRILDI.");
        if (erteleMesajPanel != null) erteleMesajPanel.SetActive(false);
        if (tokmakKararPanel != null) tokmakKararPanel.SetActive(true);
    }

    void KapatVeGenelDurumuNormaleDonder(GameObject kapatilacakPanel)
    {
        if (kapatilacakPanel != null) kapatilacakPanel.SetActive(false);
        ArkaPlanVeDigerUIAyarla(false);
    }

    void ArkaPlanVeDigerUIAyarla(bool modalAcikMi)
    {
        if (arkaPlanYoneticisi != null)
            arkaPlanYoneticisi.BlurluArkaPlaniAyarla(modalAcikMi);
        if (digerAnaUIArayuzuCanvasGroup != null)
        {
            digerAnaUIArayuzuCanvasGroup.interactable = !modalAcikMi;
            digerAnaUIArayuzuCanvasGroup.blocksRaycasts = !modalAcikMi;
        }
    }
}