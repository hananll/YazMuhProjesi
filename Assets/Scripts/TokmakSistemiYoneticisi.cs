using UnityEngine;
using UnityEngine.UI;

public class TokmakSistemiYoneticisi : MonoBehaviour
{
    public GameObject tokmakKararPanel; 
    public GameObject erteleMesajPanel;   
   //public GameObject davayiDusurPanel;  // bu ekrana barlar gelecek!
    public GameObject kararVermeAnaPanel;
    public Button ertelemeKapatButton;
    public Button karariVermeKapatButton;

    void Start()
    {
        if(ertelemeKapatButton != null)
        {
            ertelemeKapatButton.onClick.AddListener(ErtelemeMesajPaneliKapat);
        }

        if(karariVermeKapatButton != null)
        {
            karariVermeKapatButton.onClick.AddListener(KararvermeAnaPanelKapat);
        }
    }
    public void TokmakTiklandi()
    {
        Debug.Log("Tokmaða týklandý!");
        if (tokmakKararPanel != null)
        {
            tokmakKararPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Tokmak Karar Paneli Inspector'da atanmamýþ!");
        }
    }

    public void ErteleButonunaTiklandi()
    {
        Debug.Log("Davayý Ertele butonuna týklandý!");
        if (erteleMesajPanel != null)
        {
            erteleMesajPanel.SetActive(true);
            // Tokmak karar panelini kapatabiliriz (isteðe baðlý)
            if (tokmakKararPanel != null)
            {
                tokmakKararPanel.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("Ertele Mesaj Paneli Inspector'da atanmamýþ!");
        }
    }

    public void DusurButonunaTiklandi()
    {
        Debug.Log("Davayý Düþür butonuna týklandý!");
        // Burada hukuka uygunluk, kamuoyu tepkisi, vicdan gibi deðerleri etkileyecek kodlar yazýlabilir (þimdilik sadece log).
        Debug.Log("Oyun sistem deðerleri etkilendi (þimdilik görsel yok).");
        // Tokmak karar panelini kapatabiliriz (isteðe baðlý)
        if (tokmakKararPanel != null)
        {
            tokmakKararPanel.SetActive(false);
        }
    }

    public void KararVerButonunaTiklandi()
    {
        Debug.Log("Karar Ver butonuna týklandý!");
        if (kararVermeAnaPanel != null)
        {
            kararVermeAnaPanel.SetActive(true);
            // Tokmak karar panelini kapatabiliriz (isteðe baðlý)
            if (tokmakKararPanel != null)
            {
                tokmakKararPanel.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("Karar Verme Ana Paneli Inspector'da atanmamýþ!");
        }
    }

    // Ýsteðe baðlý olarak panelleri kapatmak için genel bir fonksiyon yazabiliriz
    public void PaneliKapat(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogError("Kapatýlacak panel atanmamýþ!");
        }
    }
    public void ErtelemeMesajPaneliKapat()
    {
        if(erteleMesajPanel != null)
        {
            erteleMesajPanel.SetActive(false);
        }
    }

    public void KararvermeAnaPanelKapat()
    {
        if(kararVermeAnaPanel != null)
        {
            kararVermeAnaPanel.SetActive(false);
        }
    }
}