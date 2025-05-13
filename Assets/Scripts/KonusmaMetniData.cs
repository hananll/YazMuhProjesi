using UnityEngine;

[CreateAssetMenu(fileName = "KonusmaMetni", menuName = "Veri/Konusma Metni")]
public class KonusmaMetniData : ScriptableObject
{
    [TextArea(3, 10)] // Inspector'da daha büyük bir metin alaný saðlar
    public string metin;
    public string konusmaciAdi;
    public Sprite konusmaciPortresi; // Ýsteðe baðlý
    // Ýleride metinle ilgili baþka veriler ekleyebilirsiniz (örneðin, bir sonraki metnin ID'si, ses dosyasý vb.).
}