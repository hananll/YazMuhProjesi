using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundCont : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    [Header("Müzik Ayarları")]
    public bool ileriBaşlat = true;
    public float baslangicSuresi = 10f;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (ileriBaşlat && audioSource != null && audioSource.clip != null)
        {
            baslangicSuresi = Mathf.Clamp(baslangicSuresi, 0, audioSource.clip.length - 0.1f);
            audioSource.time = baslangicSuresi;
            audioSource.Play();
        }
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }
}
