using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
/*
public class CourtManager : MonoBehaviour
{
    public static CourtManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [Header("Karakterler ve Pozisyonlar")]
    public GameObject[] characters;            // Karakter prefabları (ayakta + oturuyor birlikte)
    public Transform[] seatPositions;          // Her karakterin oturma pozisyonu
    public Transform podiumPosition;           // Kürsü pozisyonu

    [Header("Görseller")]
    public GameObject[] oturmaGorselleri;      // Otururken görünen objeler
    public GameObject[] ayaktaGorselleri;      // Kürsüde görünen objeler

    [Header("Diyaloglar")]
    public DialogueTrigger[] dialogueTriggers;

    [Header("Paneller")]
    public GameObject raporPaneli;
    public GameObject mikrofonPaneli;
    public GameObject kitapPaneli;

    [HideInInspector]
    public GameObject currentCharacter; // Mevcut kürsüdeki karakter

    public void CallCharacter(int index)
    {
        // 1. Mevcut karakter varsa eski pozisyona geri gönder ve oturma görselini aç
        if (currentCharacter != null)
        {
            int oldIndex = System.Array.IndexOf(characters, currentCharacter);
            if (oldIndex >= 0 && oldIndex < seatPositions.Length)
            {
                currentCharacter.transform.position = seatPositions[oldIndex].position;
                GorselGuncelle(oldIndex, false); // oturur görsel
            }
        }

        // 2. Yeni karakteri kürsüye getir
        currentCharacter = characters[index];
        currentCharacter.transform.position = podiumPosition.position;
        GorselGuncelle(index, true); // ayakta görsel

        // 3. Diyaloğu başlat
        if (dialogueTriggers.Length > index)
        {
            dialogueTriggers[index].TriggerDialogue();
        }
    }

    void GorselGuncelle(int karakterIndex, bool ayaktaMi)
    {
        if (karakterIndex < 0 || karakterIndex >= characters.Length) return;

        // Ayakta ve oturur görselleri aktif/pasif yap
        if (ayaktaGorselleri[karakterIndex] != null)
            ayaktaGorselleri[karakterIndex].SetActive(ayaktaMi);

        if (oturmaGorselleri[karakterIndex] != null)
            oturmaGorselleri[karakterIndex].SetActive(!ayaktaMi);
    }

    // Panel Açma Fonksiyonları
    public void RaporuAc()
    {
        raporPaneli?.SetActive(true);
    }

    public void MikrofonAc()
    {
        mikrofonPaneli?.SetActive(true);
    }

    public void KitapAc()
    {
        kitapPaneli?.SetActive(true);
    }
}
*/
using UnityEngine;

public class CourtManager : MonoBehaviour
{
    // 🔹 Singleton erişimi
    public static CourtManager Instance { get; private set; }

    [System.Serializable]
    public class CharacterData
    {
        public string name;
        public GameObject characterObject;
        public Transform seatPosition;
        public GameObject standingVisual;
        public GameObject sittingVisual;
    }

    public CharacterData[] characters;
    public Transform podiumPosition;

    [HideInInspector] public CharacterData currentCharacter;

    private void Awake()
    {
        // Singleton kurulumu
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Başlangıçta herkes otursun
        foreach (var character in characters)
        {
            if (character.characterObject != null && character.seatPosition != null)
            {
                character.characterObject.transform.position = character.seatPosition.position;
                character.standingVisual.SetActive(false);
                character.sittingVisual.SetActive(true);
            }
        }

        currentCharacter = null;
    }

    public void CallCharacter(int index)
    {
        if (index < 0 || index >= characters.Length)
        {
            Debug.LogWarning("Geçersiz karakter indexi!");
            return;
        }

        // Önceki karakteri oturt
        if (currentCharacter != null)
        {
            currentCharacter.characterObject.transform.position = currentCharacter.seatPosition.position;
            currentCharacter.standingVisual.SetActive(false);
            currentCharacter.sittingVisual.SetActive(true);
        }

        // Yeni karakteri çağır
        CharacterData newCharacter = characters[index];

        newCharacter.characterObject.transform.position = podiumPosition.position;
        newCharacter.standingVisual.SetActive(true);
        newCharacter.sittingVisual.SetActive(false);

        currentCharacter = newCharacter;
    }
}
