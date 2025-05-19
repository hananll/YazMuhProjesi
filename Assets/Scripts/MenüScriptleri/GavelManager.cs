using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class GavelManager : MonoBehaviour
{
    void Start()
{
    gavelAnimator.ResetTrigger("Trigger: Hit");
}

    public Animator gavelAnimator;
    public AudioSource gavelAudio;
    public float delayAfterHit = 1f;

    // Ortak animasyon fonksiyonu
    public void PlayGavelThen(Action callback)
    {
        StartCoroutine(PlayGavelAndExecute(callback));
    }

    IEnumerator PlayGavelAndExecute(Action callback)
    {
        gavelAnimator.SetTrigger("Trigger: Hit");
        gavelAudio.Play();
        yield return new WaitForSeconds(delayAfterHit);
        callback?.Invoke();
          
    }
void Update()
{
    if (Input.GetKeyDown(KeyCode.T))
    {
        Debug.Log("T tuşuna basıldı");
        gavelAnimator.SetTrigger("Trigger: Hit");
    }
}




}
