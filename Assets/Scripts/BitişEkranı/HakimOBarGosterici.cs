using UnityEngine;
using UnityEngine.UI;

public class HakimOBarGosterici : MonoBehaviour
{
    public Slider kamuoyuGuvenSlider;
    public Slider hukukGuvenSlider;

    void Start()
    {
        if (BarYoneticisi.kayitliKamuoyuDegeri >= 0)
            kamuoyuGuvenSlider.value = BarYoneticisi.kayitliKamuoyuDegeri;

        if (BarYoneticisi.kayitliHukukDegeri >= 0)
            hukukGuvenSlider.value = BarYoneticisi.kayitliHukukDegeri;
    }
}
