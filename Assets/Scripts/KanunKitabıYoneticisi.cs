using UnityEngine;
using UnityEngine.UI;

public class KanunKitabiYoneticisi : MonoBehaviour
{
    public Button kanunKitabiButon;
    public GameObject kanunKitabiPanel;
    public GameObject[] kanunKitabiSayfalari;
    public Button kanunSonrakiSayfaButon;
    public Button kanunOncekiSayfaButon;
    public Button kanunKitabiKapatButon;
    private int aktifSayfaIndex = 0;

    void Start()
    {
        if (kanunKitabiButon != null)
        {
            kanunKitabiButon.onClick.AddListener(KanunKitabiPaneliAc);
        }
        else
        {
            Debug.LogError("Kanun Kitabı Butonu Inspector'da atanmamış!");
        }

        if (kanunKitabiKapatButon != null)
        {
            kanunKitabiKapatButon.onClick.AddListener(KanunKitabiPaneliKapat);
        }
        else
        {
            Debug.LogError("Kanun Kitabı Kapat Butonu Inspector'da atanmamış!");
        }

        if (kanunSonrakiSayfaButon != null)
        {
            kanunSonrakiSayfaButon.onClick.AddListener(SonrakiSayfa);
        }
        else
        {
            Debug.LogError("Sonraki Sayfa Butonu (Kanun Kitabı) Inspector'da atanmamış!");
        }

        if (kanunOncekiSayfaButon != null)
        {
            kanunOncekiSayfaButon.onClick.AddListener(OncekiSayfa);
        }
        else
        {
            Debug.LogError("Önceki Sayfa Butonu (Kanun Kitabı) Inspector'da atanmamış!");
        }

        if (kanunKitabiPanel != null)
        {
            kanunKitabiPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Kanun Kitabı Paneli Inspector'da atanmamış!");
        }

        if (kanunKitabiSayfalari.Length > 0)
        {
            SayfayiGoster(0);
            if (kanunOncekiSayfaButon != null)
            {
                kanunOncekiSayfaButon.interactable = false;
            }
        }
        else
        {
            Debug.LogError("Kanun Kitabı Sayfalar dizisi Inspector'da atanmamış veya boş!");
        }
    }

    void KanunKitabiPaneliAc()
    {
        Debug.Log("Kanun kitabı butonu tıklandı.");
        if (kanunKitabiPanel != null)
        {
            kanunKitabiPanel.SetActive(true);
        }
    }

    void KanunKitabiPaneliKapat()
    {
        Debug.Log("Kanun kitabı kapat butonu tıklandı.");
        if (kanunKitabiPanel != null)
        {
            kanunKitabiPanel.SetActive(false);
        }
    }

    public void SonrakiSayfa()
    {
        if (aktifSayfaIndex < kanunKitabiSayfalari.Length - 1)
        {
            aktifSayfaIndex++;
            SayfayiGoster(aktifSayfaIndex);
            if (kanunOncekiSayfaButon != null)
            {
                kanunOncekiSayfaButon.interactable = true;
            }
            if (kanunSonrakiSayfaButon != null && aktifSayfaIndex == kanunKitabiSayfalari.Length - 1)
            {
                kanunSonrakiSayfaButon.interactable = false;
            }
        }
    }

    public void OncekiSayfa()
    {
        if (aktifSayfaIndex > 0)
        {
            aktifSayfaIndex--;
            SayfayiGoster(aktifSayfaIndex);
            if (kanunSonrakiSayfaButon != null)
            {
                kanunSonrakiSayfaButon.interactable = true;
            }
            if (kanunOncekiSayfaButon != null && aktifSayfaIndex == 0)
            {
                kanunOncekiSayfaButon.interactable = false;
            }
        }
    }

    void SayfayiGoster(int sayfaIndex)
    {
        foreach (GameObject sayfa in kanunKitabiSayfalari)
        {
            if (sayfa != null)
            {
                sayfa.SetActive(false);
            }
        }

        if (sayfaIndex >= 0 && sayfaIndex < kanunKitabiSayfalari.Length && kanunKitabiSayfalari[sayfaIndex] != null)
        {
            kanunKitabiSayfalari[sayfaIndex].SetActive(true);
        }
        else
        {
            Debug.LogError("Geçersiz kanun kitabı sayfa indeksi: " + sayfaIndex);
        }
    }
}