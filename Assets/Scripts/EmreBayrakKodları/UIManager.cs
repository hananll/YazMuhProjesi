using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GavelManager gavelManager; // Inspector’dan atanacak

    public void StartGame()
    {
        // Gavel animasyon + ses + sonra sahne geçişi
        gavelManager.PlayGavelThen(() =>
        {
            StartCoroutine(LoadSceneAfterDelay("MahkemeSalonuScene", 1f)); // 0.3f: animasyon süresi
        });
    }

    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    // Diğer butonlar aynı kalabilir
    public void AyarlarButton()
    {
        gavelManager.PlayGavelThen(() =>
        {
            StartCoroutine(LoadSceneAfterDelay("Options", 0.3f));
        });
    }

    public void MenuyeDon()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OdanaDon()
    {
        SceneManager.LoadScene("HakiminOdasi");
    }
    public void DavayaDon()
    {
        SceneManager.LoadScene("MahkemeSalonuScene");
    }
    public void EmekEkrani()
    {
        SceneManager.LoadScene("Emek");
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
}
