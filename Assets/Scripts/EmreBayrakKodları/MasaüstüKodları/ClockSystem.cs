using TMPro;
using UnityEngine;
using System;

public class ClockSystem : MonoBehaviour
{
    public TextMeshProUGUI clockText;

    void Update()
    {
        DateTime now = DateTime.Now;
        clockText.text = now.ToString("HH:mm - dd MMMM yyyy");
    }
}

