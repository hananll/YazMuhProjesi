/*using UnityEngine;
using UnityEngine.UI;

public class DesktopCursorChanger : MonoBehaviour
{
    public Texture2D customCursor;
    public Vector2 cursorHotspot = Vector2.zero;
    public GameObject desktopPanel;      // Panelin referansı
    public GameObject googlePanel;
    public GameObject newsPanel;

    private bool cursorActive = false;

    void Update()
    {
        // Eğer masaüstü paneli açıksa özel imleç aktif olsun
        if (desktopPanel.activeSelf)
        {
            if (!cursorActive)
            {
                Cursor.SetCursor(customCursor, cursorHotspot, CursorMode.Auto);
                cursorActive = true;
            }
        }
        else
        {
            if (cursorActive)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                cursorActive = false;
            }
        }
    }

    // Masaüstü paneli kapatıldığında cursor'u resetlemek için
    public void CloseDesktopPanel()
    {
        desktopPanel.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        cursorActive = false;
    }

    // Google paneli kapandığında sadece cursor'u resetleme!
    public void CloseGooglePanel()
    {
        googlePanel.SetActive(false);
        // desktopPanel açıksa cursor kalsın
    }

    // News paneli kapandığında sadece cursor'u resetleme!
    public void CloseNewsPanel()
    {
        newsPanel.SetActive(false);
        // desktopPanel açıksa cursor kalsın
    }
}
*/
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public GameObject desktopPanel;
    public Texture2D customCursor;
    public Vector2 hotspot = Vector2.zero;

    private bool isCursorCustom = false;

    void Update()
    {
        if (desktopPanel.activeSelf)
        {
            // Eğer özel cursor aktif değilse uygula
            if (!isCursorCustom)
            {
                Cursor.SetCursor(customCursor, hotspot, CursorMode.Auto);
                isCursorCustom = true;
            }
        }
        else
        {
            // Masaüstü kapalıysa default cursor'a dön
            if (isCursorCustom)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                isCursorCustom = false;
            }
        }
    }
}
