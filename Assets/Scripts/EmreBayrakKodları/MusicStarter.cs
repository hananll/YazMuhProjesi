    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCallButton : MonoBehaviour
{
    public int characterIndex; // Inspector'dan belirleyeceğiz
    int index;
    public void CallCharacter()
    {
        CourtManager.Instance.CallCharacter(index);
    }
}

