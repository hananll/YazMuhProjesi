using UnityEngine;
using UnityEngine.UI;

public class MergeItem : MonoBehaviour
{
    public Image itemImage;
    public int level;
    private MergeManager mergeManager;

    public void Initialize(int level, Sprite sprite, MergeManager manager)
    {
        this.level = level;
        itemImage.sprite = sprite;
        mergeManager = manager;
    }

    public void OnClick()
    {
        if (mergeManager == null)
        {
            Debug.LogError("mergeManager boş! Initialize() çağrılmamış olabilir.");
            return;
        }

        mergeManager.HandleItemClick(this);
    }
}
