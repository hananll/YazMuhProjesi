using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidePanelController : MonoBehaviour
{
    public RectTransform sidePanel;
    private bool isOpen = false;
    private Vector2 visiblePos;
    private Vector2 hiddenPos;

    private void Start()
    {
        float width = sidePanel.rect.width;

        visiblePos = Vector2.zero;
        hiddenPos = new Vector2(-width, 0);

        sidePanel.anchoredPosition = hiddenPos;
    }

    public void TogglePanel()
    {
        StopAllCoroutines();
        StartCoroutine(SlideTo(isOpen ? hiddenPos : visiblePos));
        isOpen = !isOpen;
    }

    private System.Collections.IEnumerator SlideTo(Vector2 target)
    {
        while (Vector2.Distance(sidePanel.anchoredPosition, target) > 0.1f)
        {
            sidePanel.anchoredPosition = Vector2.Lerp(
                sidePanel.anchoredPosition,
                target,
                Time.deltaTime * 10
            );
            yield return null;
        }

        sidePanel.anchoredPosition = target;
    }
    public void ClosePanel()
{
    if (isOpen)  // Eğer açıksa kapat
    {
        StopAllCoroutines();
        StartCoroutine(SlideTo(hiddenPos));
        isOpen = false;
    }
}

}