using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroller : MonoBehaviour
{
    public float scrollSpeed = 30f;

    void Update()
    {
        // Yukarı doğru kaydırma (local pozisyon)
        transform.localPosition += Vector3.up * scrollSpeed * Time.deltaTime;
    }
}


