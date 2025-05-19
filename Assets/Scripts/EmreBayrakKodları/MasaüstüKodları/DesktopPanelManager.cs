using UnityEngine;

public class DesktopPanelManager : MonoBehaviour
{
    public GameObject googleButton;
    public GameObject newsButton;

    public GameObject webPanel;
    public GameObject newsPanel;

    public void OpenGoogle()
    {
        webPanel.SetActive(true);
        googleButton.SetActive(true);
        newsButton.SetActive(false); // News gizleniyor
    }

    public void OpenNews()
    {
        newsPanel.SetActive(true);
        newsButton.SetActive(true);
        googleButton.SetActive(false); // Google gizleniyor
    }

    public void CloseAllPanels()
    {
        webPanel.SetActive(false);
        newsPanel.SetActive(false);
        googleButton.SetActive(true); // İkisi de geri geliyor
        newsButton.SetActive(true);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

    }
}
