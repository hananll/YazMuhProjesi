using UnityEngine;
using UnityEngine.UI;

public class DavaDosyasiYoneticisi : MonoBehaviour
{
    public Button davaDosyasiButon;
    public GameObject davaDosyasiAnaPanel;
    public GameObject[] davaDosyasiSayfalari;
    public Button sonrakiSayfaButon;
    public Button oncekiSayfaButon;
    public Button davaDosyasiKapatButon;
    private int aktifSayfaIndex = 0;

    void Start()
    {
        if (davaDosyasiButon != null)
        {
            davaDosyasiButon.onClick.AddListener(DavaDosyasiPaneliAc);
        }
        else
        {
            Debug.LogError("Dava Dosyas� Butonu Inspector'da atanmam��!");
        }

        if (davaDosyasiKapatButon != null)
        {
            davaDosyasiKapatButon.onClick.AddListener(DavaDosyasiPaneliKapat);
        }
        else
        {
            Debug.LogError("Dava Dosyas� Kapat Butonu Inspector'da atanmam��!");
        }

        if (sonrakiSayfaButon != null)
        {
            sonrakiSayfaButon.onClick.AddListener(SonrakiSayfa);
        }
        else
        {
            Debug.LogError("Sonraki Sayfa Butonu (Dava Dosyas�) Inspector'da atanmam��!");
        }

        if (oncekiSayfaButon != null)
        {
            oncekiSayfaButon.onClick.AddListener(OncekiSayfa);
        }
        else
        {
            Debug.LogError("�nceki Sayfa Butonu (Dava Dosyas�) Inspector'da atanmam��!");
        }

        if (davaDosyasiAnaPanel != null)
        {
            davaDosyasiAnaPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Dava Dosyas� Ana Paneli Inspector'da atanmam��!");
        }

        if (davaDosyasiSayfalari.Length > 0)
        {
            SayfayiGoster(0);
            if (oncekiSayfaButon != null)
            {
                oncekiSayfaButon.interactable = false;
            }
        }
        else
        {
            Debug.LogError("Dava Dosyas� Sayfalar dizisi Inspector'da atanmam�� veya bo�!");
        }
    }

    void DavaDosyasiPaneliAc()
    {
        Debug.Log("Dava dosyas� butonu t�kland�.");
        if (davaDosyasiAnaPanel != null)
        {
            davaDosyasiAnaPanel.SetActive(true);
        }
    }

    void DavaDosyasiPaneliKapat()
    {
        Debug.Log("Dava dosyas� kapat butonu t�kland�.");
        if (davaDosyasiAnaPanel != null)
        {
            davaDosyasiAnaPanel.SetActive(false);
        }
    }

    public void SonrakiSayfa()
    {
        if (aktifSayfaIndex < davaDosyasiSayfalari.Length - 1)
        {
            aktifSayfaIndex++;
            SayfayiGoster(aktifSayfaIndex);
            if (oncekiSayfaButon != null)
            {
                oncekiSayfaButon.interactable = true;
            }
            if (sonrakiSayfaButon != null && aktifSayfaIndex == davaDosyasiSayfalari.Length - 1)
            {
                sonrakiSayfaButon.interactable = false;
            }
        }
    }

    public void OncekiSayfa()
    {
        if (aktifSayfaIndex > 0)
        {
            aktifSayfaIndex--;
            SayfayiGoster(aktifSayfaIndex);
            if (sonrakiSayfaButon != null)
            {
                sonrakiSayfaButon.interactable = true;
            }
            if (oncekiSayfaButon != null && aktifSayfaIndex == 0)
            {
                oncekiSayfaButon.interactable = false;
            }
        }
    }

    void SayfayiGoster(int sayfaIndex)
    {
        foreach (GameObject sayfa in davaDosyasiSayfalari)
        {
            if (sayfa != null)
            {
                sayfa.SetActive(false);
            }
        }

        if (sayfaIndex >= 0 && sayfaIndex < davaDosyasiSayfalari.Length && davaDosyasiSayfalari[sayfaIndex] != null)
        {
            davaDosyasiSayfalari[sayfaIndex].SetActive(true);
        }
        else
        {
            Debug.LogError("Ge�ersiz dava dosyas� sayfa indeksi: " + sayfaIndex);
        }
    }
}