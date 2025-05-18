// Script Adı: KararGirisPaneliYoneticisi.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using UnityEngine.SceneManagement;

public class KararGirisPaneliYoneticisi : MonoBehaviour
{
    [Header("Panel Referansı")]
    public GameObject kararGirisPaneliObject;

    [Header("Giriş Alanları")]
    // public TMP_InputField hapisSuresiInputField; // ESKİSİ - KALDIRILDI VEYA YORUMA ALINDI
    public TMP_InputField hapisYilInputField;    // YENİ - Yıl için
    public TMP_InputField hapisAyInputField;     // YENİ - Ay için
    public TMP_InputField paraCezasiInputField;

    [Header("Butonlar")]
    public Button hukumVerButonu;
    public Button beraatButonu;
    public Button panelKapatButonu;

    [Header("Bağlantılı Yöneticiler")]
    public ArkaPlanYonetimi arkaPlanYoneticisi;
    public CanvasGroup digerAnaUIArayuzuCanvasGroup;

    private int _aktifDava_IdealHapisGun;
    private float _aktifDava_IdealParaCezasi;
    private bool _aktifDava_IdealBeraatOlmalydi;

    // Yıl ve ay için gün sabitleri (oyun mantığınıza göre değiştirebilirsiniz)
    private const int BIR_YIL_GUN_SAYISI = 365;
    private const int BIR_AY_GUN_SAYISI = 30; // Ortalama olarak

    void Awake()
    {
        if (kararGirisPaneliObject == null) kararGirisPaneliObject = this.gameObject;

        // Referans kontrolleri
        if (hapisYilInputField == null) Debug.LogError("HATA: KararGirisPaneliYoneticisi - Hapis Yıl InputField ATANMAMIŞ!");
        if (hapisAyInputField == null) Debug.LogError("HATA: KararGirisPaneliYoneticisi - Hapis Ay InputField ATANMAMIŞ!");
        if (paraCezasiInputField == null) Debug.LogError("HATA: KararGirisPaneliYoneticisi - Para Cezası InputField ATANMAMIŞ!");
        if (hukumVerButonu == null) Debug.LogError("HATA: KararGirisPaneliYoneticisi - Hüküm Ver Butonu ATANMAMIŞ!");
        if (beraatButonu == null) Debug.LogError("HATA: KararGirisPaneliYoneticisi - Beraat Butonu ATANMAMIŞ!");
        if (panelKapatButonu == null) Debug.LogError("HATA: KararGirisPaneliYoneticisi - Panel Kapat Butonu ATANMAMIŞ!");

        if (arkaPlanYoneticisi == null && ArkaPlanYonetimi.Ornek == null) Debug.LogWarning("UYARI: KararGirisPaneliYoneticisi - Arka Plan Yöneticisi ATANMAMIŞ ve Singleton bulunamadı.");
        else if (arkaPlanYoneticisi == null) arkaPlanYoneticisi = ArkaPlanYonetimi.Ornek;

        if (digerAnaUIArayuzuCanvasGroup == null) Debug.LogWarning("UYARI: KararGirisPaneliYoneticisi - Diğer Ana UI CanvasGroup ATANMAMIŞ.");

        // Listener'lar
        if (hukumVerButonu != null) hukumVerButonu.onClick.AddListener(HukmuOnayla);
        if (beraatButonu != null) beraatButonu.onClick.AddListener(BeraatKarariVerildi);
        if (panelKapatButonu != null) panelKapatButonu.onClick.AddListener(KararPaneliniKapat);

        if (kararGirisPaneliObject != null) kararGirisPaneliObject.SetActive(false);
    }

    public void KararPaneliniGoster(int idealHapisGun, float idealPara, bool idealBeraat)
    {
        _aktifDava_IdealHapisGun = idealHapisGun;
        _aktifDava_IdealParaCezasi = idealPara;
        _aktifDava_IdealBeraatOlmalydi = idealBeraat;

        if (kararGirisPaneliObject == null) return;
        kararGirisPaneliObject.SetActive(true);

        // Yeni input field'ları temizle
        if (hapisYilInputField != null) hapisYilInputField.text = "";
        if (hapisAyInputField != null) hapisAyInputField.text = "";
        if (paraCezasiInputField != null) paraCezasiInputField.text = "";

        ArkaPlanVeDigerUIAyarla(true);

        // if (hapisYilInputField != null && hapisYilInputField.IsInteractable()) hapisYilInputField.Select();
    }

    public void KararPaneliniKapat()
    {
        if (kararGirisPaneliObject == null) return;
        kararGirisPaneliObject.SetActive(false);
        ArkaPlanVeDigerUIAyarla(false);
        SceneManager.LoadScene(1);
    }

    void HukmuOnayla()
    {
        if (hapisYilInputField == null || hapisAyInputField == null || paraCezasiInputField == null)
        {
            Debug.LogError("HATA: KararGirisPaneliYoneticisi - InputField referansları eksik, hüküm onaylanamıyor.");
            return;
        }

        // Yıl ve Ay girdilerini al
        string hapisYilYazisi = hapisYilInputField.text;
        string hapisAyYazisi = hapisAyInputField.text;
        string paraCezasiYazisi = paraCezasiInputField.text;

        int girilenYil = 0;
        int girilenAy = 0;
        int toplamHapisGunu = 0; // Oyuncunun girdiği toplam gün
        float paraCezasiMiktari = 0f;

        // Yılı parse et
        if (!string.IsNullOrEmpty(hapisYilYazisi))
        {
            if (int.TryParse(hapisYilYazisi, out girilenYil))
            {
                if (girilenYil < 0) girilenYil = 0;
            }
            else
            {
                Debug.LogWarning("Geçersiz hapis yılı formatı: " + hapisYilYazisi + ". Yıl 0 olarak kabul edilecek.");
                girilenYil = 0;
            }
        }

        // Ayı parse et
        if (!string.IsNullOrEmpty(hapisAyYazisi))
        {
            if (int.TryParse(hapisAyYazisi, out girilenAy))
            {
                if (girilenAy < 0) girilenAy = 0;
                // Opsiyonel: Ay 11'den büyükse yıla ekleyip ay'ı mod 12 yapabilirsiniz. Şimdilik basit tutalım.
                // if (girilenAy >= 12) { girilenYil += girilenAy / 12; girilenAy %= 12; }
            }
            else
            {
                Debug.LogWarning("Geçersiz hapis ayı formatı: " + hapisAyYazisi + ". Ay 0 olarak kabul edilecek.");
                girilenAy = 0;
            }
        }

        // Toplam hapis gününü hesapla
        toplamHapisGunu = (girilenYil * BIR_YIL_GUN_SAYISI) + (girilenAy * BIR_AY_GUN_SAYISI);

        // Para cezasını parse et
        if (!string.IsNullOrEmpty(paraCezasiYazisi)) { /* ... (öncekiyle aynı para cezası parse etme logiği) ... */ string duzeltilmisParaYazisi = paraCezasiYazisi.Replace(',', '.'); if (!float.TryParse(duzeltilmisParaYazisi, NumberStyles.Any, CultureInfo.InvariantCulture, out paraCezasiMiktari)) { paraCezasiMiktari = 0f; Debug.LogWarning("Geçersiz para cezası formatı: " + paraCezasiYazisi); } if (paraCezasiMiktari < 0) paraCezasiMiktari = 0f; }

        Debug.Log("===== HÜKÜM (CEZALI) ONAYLANDI =====");
        Debug.Log($"Girilen Hapis: {girilenYil} Yıl, {girilenAy} Ay (Toplam {toplamHapisGunu} gün)");
        Debug.Log("Verilen Para Cezası: " + paraCezasiMiktari.ToString("F2") + " ₺");
        Debug.Log("===================================");

        if (BarYoneticisi.Ornek != null)
        {
            BarYoneticisi.Ornek.NihaiKararaGoreBarlariGuncelle(
                toplamHapisGunu, // Oyuncunun verdiği toplam gün
                paraCezasiMiktari,
                false, // beraat değil
                _aktifDava_IdealHapisGun,
                _aktifDava_IdealParaCezasi,
                _aktifDava_IdealBeraatOlmalydi
            );
        }
        else { Debug.LogError("HATA: BarYoneticisi.Ornek bulunamadı, barlar güncellenemedi! (Hüküm Onayla)"); }

        KararPaneliniKapat();
        SceneManager.LoadScene(1);

    }

    void BeraatKarariVerildi()
    {
        Debug.Log("===== BERAAT KARARI VERİLDİ =====");

        if (BarYoneticisi.Ornek != null)
        {
            BarYoneticisi.Ornek.NihaiKararaGoreBarlariGuncelle(
                0, 0, true, // beraat kararı
                _aktifDava_IdealHapisGun,
                _aktifDava_IdealParaCezasi,
                _aktifDava_IdealBeraatOlmalydi
            );
        }
        else { Debug.LogError("HATA: BarYoneticisi.Ornek bulunamadı, barlar güncellenemedi! (Beraat)"); }

        KararPaneliniKapat();
        SceneManager.LoadScene(1);

    }

    void ArkaPlanVeDigerUIAyarla(bool panelAcikMi)
    {
        if (arkaPlanYoneticisi != null)
            arkaPlanYoneticisi.BlurluArkaPlaniAyarla(panelAcikMi);

        if (digerAnaUIArayuzuCanvasGroup != null)
        {
            digerAnaUIArayuzuCanvasGroup.interactable = !panelAcikMi;
            digerAnaUIArayuzuCanvasGroup.blocksRaycasts = !panelAcikMi;
        }
    }
}