using UnityEngine;

public class SesYoneticisi : MonoBehaviour
{
    public static SesYoneticisi Ornek { get; private set; } // Singleton örneði

    [Tooltip("Sesleri çalacak olan AudioSource component'i")]
    public AudioSource sesKaynagi;

    [Tooltip("Butonlarýn üzerine gelindiðinde çalacak ses klibi")]
    public AudioClip butonUzerineGelmeSesi;

    void Awake()
    {
        // Singleton deseni kurulumu
        if (Ornek == null)
        {
            Ornek = this;
            // Sahne geçiþlerinde bu objenin yok olmamasýný isterseniz aþaðýdaki satýrý aktif edin:
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // Zaten bir örnek varsa, bu yenisini yok et (Singleton'ýn tek olmasýný garantiler)
            Destroy(gameObject);
            return;
        }

        // AudioSource referansýný kontrol et ve ayarla
        if (sesKaynagi == null)
        {
            sesKaynagi = GetComponent<AudioSource>(); // Ayný GameObject üzerinde bir AudioSource ara
            if (sesKaynagi == null)
            {
                // AudioSource bulunamazsa, bir tane ekle (veya hata ver)
                Debug.LogWarning("SesYoneticisi: AudioSource component'i atanmamýþ veya bulunamadý. Bir tane ekleniyor.");
                sesKaynagi = gameObject.AddComponent<AudioSource>();
                sesKaynagi.playOnAwake = false; // Otomatik çalmayý kapat
            }
        }

        // Ses klibinin atanýp atanmadýðýný kontrol et
        if (butonUzerineGelmeSesi == null)
        {
            Debug.LogError("HATA: SesYoneticisi - 'Buton Üzerine Gelme Sesi' (butonUzerineGelmeSesi) Inspector'da ATANMAMIÞ! Ses çalýnamayacak.");
        }
    }

    /// <summary>
    /// Inspector'dan atanan 'butonUzerineGelmeSesi' klibini çalar.
    /// </summary>
    public void OynatButonUzerineGelmeSesi()
    {
        if (!this.enabled) return; // Script devredýþýysa iþlem yapma

        if (sesKaynagi != null && butonUzerineGelmeSesi != null && sesKaynagi.isActiveAndEnabled)
        {
            sesKaynagi.PlayOneShot(butonUzerineGelmeSesi); // Tek seferlik sesler için ideal
        }
        else
        {
            if (sesKaynagi == null || !sesKaynagi.isActiveAndEnabled)
                Debug.LogWarning("SesYoneticisi: AudioSource yok, aktif deðil veya düzgün ayarlanmamýþ. Üzerine gelme sesi oynatýlamadý.");
            if (butonUzerineGelmeSesi == null)
                Debug.LogWarning("SesYoneticisi: Buton üzerine gelme sesi (AudioClip) atanmamýþ. Ses oynatýlamadý.");
        }
    }

    // Ýleride farklý sesleri çalmak için bu metodu geniþletebilirsiniz:
    // public void SesCal(AudioClip calinacakSes)
    // {
    //     if (!this.enabled) return;
    //     if (sesKaynagi != null && calinacakSes != null && sesKaynagi.isActiveAndEnabled)
    //     {
    //         sesKaynagi.PlayOneShot(calinacakSes);
    //     }
    // }
}