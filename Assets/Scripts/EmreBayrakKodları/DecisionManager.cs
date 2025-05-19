using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DecisionManager : MonoBehaviour
{
    public GameObject decisionPanel;
    public TMP_InputField hapisInput;
    public TMP_InputField paraInput;

    public Image fadePanel;                      // Siyah Image (alpha = 0)
    public GameObject sentencePanel;             // Ceza yazı paneli
    public TextMeshProUGUI sentenceText;         // Ceza yazısı TMP Text

    public float fadeDuration = 2f;              // Fade süresi
    public float sentenceDisplayDuration = 3f;   // Yazının kalma süresi

    private void Start()
    {
        if (fadePanel != null)
            fadePanel.color = new Color(0, 0, 0, 0);

        if (sentencePanel != null)
            sentencePanel.SetActive(false);
    }

    public void OpenDecisionPanel()
    {
        decisionPanel.SetActive(true);
    }

    public void CloseDecisionPanel()
    {
        decisionPanel.SetActive(false);
    }

    public void BeraatVer()
    {
        Debug.Log("Beraat verildi.");
        decisionPanel.SetActive(false);
        StartCoroutine(ShowSentenceAndProceed("Beraat verildi."));
    }

    public void HapisVer()
    {
        if (string.IsNullOrEmpty(hapisInput.text))
        {
            Debug.LogWarning("Hapis süresi girilmemiş!");
            return;
        }

        string yil = hapisInput.text;
        Debug.Log("Hapis cezası verildi: " + yil + " yıl.");
        decisionPanel.SetActive(false);
        StartCoroutine(ShowSentenceAndProceed($"{yil} yıl hapis cezası verildi."));
    }

    public void ParaVer()
    {
        if (string.IsNullOrEmpty(paraInput.text))
        {
            Debug.LogWarning("Para cezası girilmemiş!");
            return;
        }

        string miktar = paraInput.text;
        Debug.Log("Para cezası verildi: " + miktar + " TL.");
        decisionPanel.SetActive(false);
        StartCoroutine(ShowSentenceAndProceed($"{miktar} TL para cezası verildi."));
    }

    private IEnumerator ShowSentenceAndProceed(string sentence)
    {
        // Fade in (karart)
        yield return StartCoroutine(FadeIn());

        // Yazıyı göster
        if (sentencePanel != null && sentenceText != null)
        {
            sentenceText.text = sentence;
            sentencePanel.SetActive(true);
        }

        yield return new WaitForSeconds(sentenceDisplayDuration);

        SceneManager.LoadScene("HakiminOdasi");
    }

    private IEnumerator FadeIn()
    {
        if (fadePanel == null)
            yield break;

        float elapsed = 0f;
        Color startColor = new Color(0, 0, 0, 0);
        Color targetColor = new Color(0, 0, 0, 1);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadePanel.color = Color.Lerp(startColor, targetColor, elapsed / fadeDuration);
            yield return null;
        }

        fadePanel.color = targetColor;
    }
}
