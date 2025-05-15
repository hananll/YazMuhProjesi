using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KarakterKonusmaAnimasyonu : MonoBehaviour
{
    private Animator animator;
    private readonly int konusuyorMuParamID = Animator.StringToHash("KonusuyorMu"); // Parametre ID'sini cache'lemek daha performanslýdýr

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Bu objeye atanmýþ bir Animator component'i bulunamadý!");
            enabled = false; // Script'i devre dýþý býrak
            return;
        }
    }

    // Bu fonksiyonu diyalog sisteminizden konuþma baþladýðýnda çaðýrýn
    public void KonusmayaBasla()
    {
        if (animator != null)
        {
            animator.SetBool(konusuyorMuParamID, true);
            Debug.Log(gameObject.name + " konuþmaya baþladý animasyonu.");
        }
    }

    // Bu fonksiyonu diyalog sisteminizden konuþma bittiðinde çaðýrýn
    public void KonusmayiBitir()
    {
        if (animator != null)
        {
            animator.SetBool(konusuyorMuParamID, false);
            Debug.Log(gameObject.name + " konuþmayý bitirdi animasyonu.");
        }
    }
}
