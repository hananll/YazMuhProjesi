using UnityEngine;
using UnityEngine.UI; // veya TMPro için using TMPro;

public class YukariKaydirma : MonoBehaviour
{
    public RectTransform textRect;
    public float yukariHiz = 50f; // birim saniyede ne kadar yukarı çıkacak
    public float hedefYPos = 500f; // son konum Y değeri

    private Vector2 baslangicPos;

    void Start()
    {
        if (textRect == null)
            textRect = GetComponent<RectTransform>();

        baslangicPos = textRect.anchoredPosition;
    }

    void Update()
    {
        if (textRect.anchoredPosition.y < hedefYPos)
        {
            Vector2 yeniPos = textRect.anchoredPosition;
            yeniPos.y += yukariHiz * Time.deltaTime;
            textRect.anchoredPosition = yeniPos;
        }
    }
}
