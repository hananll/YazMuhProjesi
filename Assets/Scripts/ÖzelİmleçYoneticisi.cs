using UnityEngine;

public class OzelImlecYoneticisi : MonoBehaviour
{
    [Tooltip("Kullanılacak özel imleç görseli (Texture2D)")]
    public Texture2D imlecGorseli;

    [Tooltip("İmlecin 'tıklama noktasının' görselin sol üst köşesine göre X ve Y ofseti")]
    public Vector2 aktifNokta = Vector2.zero;

    [Tooltip("İmleç modu: Donanımsal veya Yazılımsal")]
    public CursorMode imlecModu = CursorMode.Auto;

    void Start()
    {
        // Oyun başlar başlamaz özel imleci ayarla
        OzelImleciAyarla();
    }

    public void OzelImleciAyarla()
    {
        if (imlecGorseli != null)
        {
            Cursor.SetCursor(imlecGorseli, aktifNokta, imlecModu);
        }
        else
        {
            Debug.LogError("OzelImlecYoneticisi: İmleç Görseli (imlecGorseli) Inspector'dan atanmamış!");
        }
    }

    // Opsiyonel: Oyun sonlandığında varsayılan sistem imlecine dönmek iyi bir pratiktir
    void OnApplicationQuit()
    {
        // Varsayılan sistem imlecine dön
        Cursor.SetCursor(null, Vector2.zero, imlecModu);
    }
}