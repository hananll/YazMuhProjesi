using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeManager : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform itemParent;
    public List<Sprite> itemSprites;

    private MergeItem selectedItem;

    void Start()
    {
        GenerateNewItem();
        GenerateNewItem();
    }

    public void GenerateNewItem()
    {
        GameObject obj = Instantiate(itemPrefab, itemParent);
        MergeItem item = obj.GetComponent<MergeItem>();

        int level = 0;
        Sprite sprite = itemSprites[level];

        Button btn = obj.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(item.OnClick);

        item.Initialize(level, sprite, this);
    }

    public void HandleItemClick(MergeItem clickedItem)
    {
        if (selectedItem == null)
        {
            selectedItem = clickedItem;
        }
        else if (selectedItem == clickedItem)
        {
            selectedItem = null;
        }
        else
        {
            TryMerge(selectedItem, clickedItem);
            selectedItem = null;
        }
    }

    void TryMerge(MergeItem item1, MergeItem item2)
    {
        if (item1.level == item2.level)
        {
            int newLevel = item1.level + 1;

            Destroy(item1.gameObject);
            Destroy(item2.gameObject);

            GameObject obj = Instantiate(itemPrefab, itemParent);
            MergeItem newItem = obj.GetComponent<MergeItem>();

            Sprite sprite = itemSprites[Mathf.Min(newLevel, itemSprites.Count - 1)];

            Button btn = obj.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(newItem.OnClick);

            newItem.Initialize(newLevel, sprite, this);
        }
    }
}
