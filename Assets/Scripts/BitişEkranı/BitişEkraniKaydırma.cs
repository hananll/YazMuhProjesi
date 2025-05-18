using UnityEngine;
using UnityEngine.UI;

public class BitisEkraniKaydirma : MonoBehaviour
{
    public RectTransform textTransform; // Text'in RectTransform'u
    public float hiz = 30f; // Piksel/saniye kayma hızı
    public float hedefY = 500f; // Yukarıda duracağı pozisyon

    void Start()
    {
        // Başlangıçta ekranın altında olsun (örneğin -300)
        Vector2 pos = textTransform.anchoredPosition;
        pos.y = -300f;
        textTransform.anchoredPosition = pos;
    }

    void Update()
    {
        Vector2 pos = textTransform.anchoredPosition;
        if (pos.y < hedefY)
        {
            pos.y += hiz * Time.deltaTime;
            textTransform.anchoredPosition = pos;
        }
    }
}
