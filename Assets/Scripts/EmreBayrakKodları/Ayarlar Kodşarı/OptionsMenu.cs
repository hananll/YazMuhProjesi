using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Dropdown screenModeDropdown;
    public Slider volumeSlider;
   

    Resolution[] resolutions;

    void Start()
    {
        // Tüm çözünürlükleri al ve dropdown'a ekle
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        // Kayıtlı ayarları yükle
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        int savedScreenMode = PlayerPrefs.GetInt("ScreenModeIndex", 0);
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.75f);

        resolutionDropdown.value = savedResolutionIndex;
        screenModeDropdown.value = savedScreenMode;
        volumeSlider.value = savedVolume;

        ApplySettings(); // Başlangıçta uygula
    }

    public void ApplySettings()
    {
        // Ses seviyesi
        float volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volume);

        // Çözünürlük
        int resIndex = resolutionDropdown.value;
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, (FullScreenMode)screenModeDropdown.value);
        PlayerPrefs.SetInt("ResolutionIndex", resIndex);

        // Pencere modu
        PlayerPrefs.SetInt("ScreenModeIndex", screenModeDropdown.value);

        PlayerPrefs.Save();
    }
}

