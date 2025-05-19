using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject[] allCharacters; // Tüm karakter objeleri (hem kürsüde hem yerlerinde olanlar)
    public Transform courtStandPoint; // Kürsüdeki pozisyon
    
    
    private GameObject currentCharacter;

    public void CallCharacter(GameObject characterPrefab)
    {
        // Mevcut karakter varsa yerine dönsün
        if (currentCharacter != null)
        {
            currentCharacter.SetActive(false);
        }

        // Yeni karakteri aktif et
        characterPrefab.SetActive(true);
        characterPrefab.transform.position = courtStandPoint.position;
        currentCharacter = characterPrefab;

        
    }
}

