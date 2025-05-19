using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DosyaManager : MonoBehaviour
{
   public GameObject DosyaPanel;
    public TextMeshProUGUI DosyaNameText;
    public void OpenLaw(string lawContent)
    {
        DosyaPanel.SetActive(true);
        DosyaNameText.text = lawContent;
    }
     public void CloseLaw()
    {
        DosyaPanel.SetActive(false);
    }
}
