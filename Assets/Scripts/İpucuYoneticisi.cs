using UnityEngine;
using TMPro; // TextMeshPro için

public class IpucuYoneticisi : MonoBehaviour
{
    // Singleton (Tekil Örnek) deseni için
    public static IpucuYoneticisi Ornek { get; private set; }

    public GameObject ipucuPaneliNesnesi;   // Inspector'dan IpucuPaneli'ni sürükleyin
    public TextMeshProUGUI ipucuMetinAlani; // Inspector'dan IpucuMetni'ni sürükleyin
    public RectTransform tuvalRectTransform; // Ana Canvas'ınızın RectTransform'unu sürükleyin
    public Vector2 kaydirmaMiktari = new Vector2(20f, -20f); // İpucunun fare imlecinden uzaklığı

    void Awake()
    {
        if (Ornek == null)
        {
            Ornek = this;
            if (ipucuPaneliNesnesi != null)
                ipucuPaneliNesnesi.SetActive(false); // Başlangıçta gizli olduğundan emin ol
        }
        else
        {
            Destroy(gameObject); // Zaten bir örnek varsa bunu yok et
        }
    }

    public void IpucunuGoster(string mesaj, Vector2 fareKonumu)
    {
        if (ipucuPaneliNesnesi == null || ipucuMetinAlani == null) return;

        ipucuMetinAlani.text = mesaj;
        ipucuPaneliNesnesi.SetActive(true);

        RectTransform ipucuRectTransform = ipucuPaneliNesnesi.GetComponent<RectTransform>();

        // İpucu panelinin pozisyonunu fare konumuna ve kaydırma miktarına göre ayarla
        // Daha iyi pozisyonlama için ipucuRectTransform'un pivotunu sol üste (0,1) ayarlayabilirsiniz.
        // Bu durumda aşağıdaki satır daha iyi sonuç verebilir:
        // ipucuRectTransform.anchoredPosition = fareKonumu / tuvalRectTransform.localScale.x + kaydirmaMiktari;
        // Ancak en basit haliyle:
        ipucuRectTransform.position = fareKonumu + kaydirmaMiktari;


        // İpucunun ekran dışına taşmasını engellemek için (isteğe bağlı ama önerilir)
        IpucunuEkranaSigdir(ipucuRectTransform);
    }

    public void IpucunuGizle()
    {
        if (ipucuPaneliNesnesi != null)
        {
            ipucuPaneliNesnesi.SetActive(false);
        }
    }

    private void IpucunuEkranaSigdir(RectTransform ipucuRect)
    {
        // Canvas'ın Screen Space - Overlay modunda olduğu varsayılmıştır.
        // Diğer modlar için RectTransformUtility.ScreenPointToLocalPointInRectangle gibi fonksiyonlar gerekebilir.
        Vector3[] ipucuKoseNoktalari = new Vector3[4];
        ipucuRect.GetWorldCorners(ipucuKoseNoktalari); // Dünya koordinatlarındaki köşeleri al

        // Canvas boyutlarını al (Screen Space Overlay için Screen.width ve Screen.height kullanılabilir)
        float ekranGenisligi = Screen.width;
        float ekranYuksekligi = Screen.height;

        // Mevcut pozisyonu dünya koordinatlarında tutalım
        Vector3 duzeltilmisPozisyon = ipucuRect.position;

        // Pivot noktasına göre ayarlama (sol üst pivot (0,1) için)
        float ipucuGenisligi = ipucuRect.rect.width * ipucuRect.lossyScale.x;
        float ipucuYuksekligi = ipucuRect.rect.height * ipucuRect.lossyScale.y;

        // Soldan taşma
        if (ipucuKoseNoktalari[0].x < 0) // Sol alt köşe X
            duzeltilmisPozisyon.x += Mathf.Abs(ipucuKoseNoktalari[0].x);
        // Sağdan taşma
        if (ipucuKoseNoktalari[2].x > ekranGenisligi) // Sağ üst köşe X
            duzeltilmisPozisyon.x -= (ipucuKoseNoktalari[2].x - ekranGenisligi);
        // Alttan taşma
        if (ipucuKoseNoktalari[0].y < 0) // Sol alt köşe Y
            duzeltilmisPozisyon.y += Mathf.Abs(ipucuKoseNoktalari[0].y);
        // Üstten taşma
        if (ipucuKoseNoktalari[1].y > ekranYuksekligi) // Sol üst köşe Y
            duzeltilmisPozisyon.y -= (ipucuKoseNoktalari[1].y - ekranYuksekligi);

        ipucuRect.position = duzeltilmisPozisyon;
    }
}