using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PanelYonetici : MonoBehaviour
{
    public GameObject newsPanel;
    public GameObject webPanel;
    public GameObject desktopPanel;
    public void OpenDesktop()
    {
        desktopPanel.SetActive(true);
    }
    public void OpenGoogle()
    {
        webPanel.SetActive(true);
    }
    public void CloseDeskop()
    {
        desktopPanel.SetActive(false);
    }
    public void OpenNews()
    {
        newsPanel.SetActive(true);
    }
    public void CloseNews()
    {
        newsPanel.SetActive(false);
    }
}
