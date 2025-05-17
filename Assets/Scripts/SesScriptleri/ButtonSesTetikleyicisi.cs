using UnityEngine;
using UnityEngine.EventSystems; // Fare olaylarýný (PointerEnter) yakalamak için bu arayüz gerekli

public class ButonSesTetikleyici : MonoBehaviour, IPointerEnterHandler // IPointerEnterHandler arayüzünü implement ediyoruz
{
    // Fare imleci bu UI elemanýnýn üzerine geldiðinde bu fonksiyon otomatik olarak çaðrýlýr
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (SesYoneticisi.Ornek != null)
        {
            SesYoneticisi.Ornek.OynatButonUzerineGelmeSesi();
        }
        else
        {
            // Bu uyarý, SesYoneticisi'nin sahnedeki kurulumunda bir sorun varsa görünür.
            Debug.LogWarning("ButonSesTetikleyici: SesYoneticisi.Ornek bulunamadý! Ses çalýnamýyor.");
        }
    }
}