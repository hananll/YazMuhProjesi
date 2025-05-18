// DavaVeriYapilari.cs
using UnityEngine; // [System.Serializable] ve Header için

// Bu class MonoBehaviour OLMAYACAK. Sadece veri yapılarını içerecek.
// Bu yüzden bir GameObject'e eklemenize gerek yok.

[System.Serializable] // Inspector'da görülebilmesi ve ScriptableObject'lerde kullanılabilmesi için
public class DavaDetayVerisi
{
    [Header("Dava Kimliği ve Açıklaması")]
    public string davaID; // Davayı ayırt etmek için benzersiz bir kimlik
    [TextArea(3, 5)]
    public string davaAciklamasi; // Davanın kısa bir özeti

    [Header("İdeal Nihai Karar Bilgileri")]
    [Tooltip("İdeal hapis süresi (gün olarak)")]
    public int idealHapisGun;
    [Tooltip("İdeal para cezası miktarı")]
    public float idealParaCezasi;
    [Tooltip("Bu dava için ideal sonucun beraat olup olmadığı")]
    public bool idealBeraatOlmalydi; // Eğer bu true ise, yukarıdaki hapis ve para 0 olmalı

    [Header("Ara Karar Etkileri (Bar Değişim Miktarları)")]
    [Tooltip("Davayı erteleme ve TUTUKLU yargılamaya karar verilirse Kamuoyu Güven Barı'na etki")]
    public float ertelemeTutuklu_KamuoyuEtkisi = -10f;
    [Tooltip("Davayı erteleme ve TUTUKLU yargılamaya karar verilirse Hukuk Güven Barı'na etki")]
    public float ertelemeTutuklu_HukukEtkisi = -5f;

    [Tooltip("Davayı erteleme ve TUTUKSUZ yargılamaya karar verilirse Kamuoyu Güven Barı'na etki")]
    public float ertelemeTutuksuz_KamuoyuEtkisi = 0f; // Kullanıcı "değişmeyecek" demişti, 0 veya küçük pozitif olabilir
    [Tooltip("Davayı erteleme ve TUTUKSUZ yargılamaya karar verilirse Hukuk Güven Barı'na etki")]
    public float ertelemeTutuksuz_HukukEtkisi = 2f;  // Belki hukuka uygun bir ara karar olarak hafif pozitif

    [Tooltip("Davanın düşürülmesinin delillere göre haklı olup olmadığı")]
    public bool davayiDusurmekGecerliMi = false;
    [Tooltip("Davayı düşürme kararı GEÇERLİ ise Kamuoyu Güven Barı'na etki")]
    public float davayiDusurGecerli_KamuoyuEtkisi = -5f;
    [Tooltip("Davayı düşürme kararı GEÇERLİ ise Hukuk Güven Barı'na etki")]
    public float davayiDusurGecerli_HukukEtkisi = 5f; // Geçerli bir düşürme hukuka güveni artırabilir
    [Tooltip("Davayı düşürme kararı GEÇERSİZ ise Kamuoyu Güven Barı'na etki")]
    public float davayiDusurGecersiz_KamuoyuEtkisi = -25f;
    [Tooltip("Davayı düşürme kararı GEÇERSİZ ise Hukuk Güven Barı'na etki")]
    public float davayiDusurGecersiz_HukukEtkisi = -35f;
}