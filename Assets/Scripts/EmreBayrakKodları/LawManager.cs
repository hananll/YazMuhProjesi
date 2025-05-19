using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LawManager : MonoBehaviour
{
    public GameObject lawPanel;
    public TextMeshProUGUI lawText;

    public void OpenLaw(string lawContent)
    {
        lawPanel.SetActive(true);
        lawText.text = lawContent;
    }

    public void CloseLaw()
    {
        lawPanel.SetActive(false);
    }
}
