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
        Debug.Log("Tokma�a t�kland�!");
        if (tokmakKararPanel != null)
        {
            tokmakKararPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Tokmak Karar Paneli Inspector'da atanmam��!");
        }
    }

    public void ErteleButonunaTiklandi()
    {
        Debug.Log("Davay� Ertele butonuna t�kland�!");
        if (erteleMesajPanel != null)
        {
            erteleMesajPanel.SetActive(true);
            // Tokmak karar panelini kapatabiliriz (iste�e ba�l�)
            if (tokmakKararPanel != null)
            {
                tokmakKararPanel.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("Ertele Mesaj Paneli Inspector'da atanmam��!");
        }
    }

    public void DusurButonunaTiklandi()
    {
        Debug.Log("Davay� D���r butonuna t�kland�!");
        // Burada hukuka uygunluk, kamuoyu tepkisi, vicdan gibi de�erleri etkileyecek kodlar yaz�labilir (�imdilik sadece log).
        Debug.Log("Oyun sistem de�erleri etkilendi (�imdilik g�rsel yok).");
        // Tokmak karar panelini kapatabiliriz (iste�e ba�l�)
        if (tokmakKararPanel != null)
        {
            tokmakKararPanel.SetActive(false);
        }
    }

    public void KararVerButonunaTiklandi()
    {
        Debug.Log("Karar Ver butonuna t�kland�!");
        if (kararVermeAnaPanel != null)
        {
            kararVermeAnaPanel.SetActive(true);
            // Tokmak karar panelini kapatabiliriz (iste�e ba�l�)
            if (tokmakKararPanel != null)
            {
                tokmakKararPanel.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("Karar Verme Ana Paneli Inspector'da atanmam��!");
        }
    }

    // �ste�e ba�l� olarak panelleri kapatmak i�in genel bir fonksiyon yazabiliriz
    public void PaneliKapat(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
        else
        {
            Debug.LogError("Kapat�lacak panel atanmam��!");
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