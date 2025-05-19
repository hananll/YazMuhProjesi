using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WebPanelController : MonoBehaviour
{
    public GameObject webPanel;
    public TMP_InputField searchInput;
    public GameObject noInternetImage;

    // Web paneli açma fonksiyonu (Google ikonuna bağlı olacak)
    public void OpenWebPanel()
    {
        webPanel.SetActive(true);
        searchInput.text = "";
        noInternetImage.SetActive(false);
    }

    // Ara butonuna bağlı olacak
    public void Search()
    {
        if (!string.IsNullOrEmpty(searchInput.text))
        {
            noInternetImage.SetActive(true);
        }
    }

    // Paneli kapatma (isteğe bağlı bir "X" butonu için)
    public void CloseWebPanel()
    {
        webPanel.SetActive(false);
        noInternetImage.SetActive(false);
    }
}
