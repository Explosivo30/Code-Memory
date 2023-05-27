using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;


public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    Resolution[] resolutions;

    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] SpriteRenderer spriteFullScreen;

    bool isFullScreen = true;
    private void Start()
    {
        
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }
        

        resolutionDropdown.AddOptions(options);
    }

    public void SetResolution(int resolutionIndex)
    {
        
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        
        audioMixer.SetFloat("volume", volume);   
    }

    public void SetQuality(int qualityIndex)
    {
        
        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log(QualitySettings.GetQualityLevel());
    }

    public void SetFullScreen()
    {
        Debug.Log("Screen");
        Debug.Log(isFullScreen);
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
        if(isFullScreen == false)
        {
            spriteFullScreen.enabled = false;
        }
        else
        {
            spriteFullScreen.enabled = true;
        }
    }


}
