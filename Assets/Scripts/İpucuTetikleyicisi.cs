using UnityEngine;
using UnityEngine.EventSystems; // IPointerEnterHandler ve IPointerExitHandler arayüzleri için

public class IpucuTetikleyici : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea(3, 5)] // Inspector'da çok satırlı metin kutusu için
    public string ipucuMesaji;

    // Fare butonun üzerine geldiğinde bu fonksiyon çağrılır
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(ipucuMesaji) && IpucuYoneticisi.Ornek != null)
        {
            // Fare imlecinin mevcut pozisyonunu IpucuYoneticisi'ne gönder
            IpucuYoneticisi.Ornek.IpucunuGoster(ipucuMesaji, Input.mousePosition);
        }
    }

    // Fare butonun üzerinden ayrıldığında bu fonksiyon çağrılır
    public void OnPointerExit(PointerEventData eventData)
    {
        if (IpucuYoneticisi.Ornek != null)
        {
            IpucuYoneticisi.Ornek.IpucunuGizle();
        }
    }
}