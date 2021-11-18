using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject devMenu;
    public AudioMixer audioMixer;
    public AudioMixer musicMixer;

    Resolution[] resolutions;
    List<string> resolutionsToDrop = new List<string>();

    public Slider volumeSlider;
    public Slider musicSlider;
    public GameObject uwaga;
    public GameObject langPick;
    void Start()
    {
        if (PlayerPrefs.HasKey("devMenu"))
        {
            PlayerPrefs.DeleteKey("devMenu");
            devMenu.SetActive(true);
        }

        if (!PlayerPrefs.HasKey("firstEnter"))
        {
            PlayerPrefs.SetInt("firstEnter", 1);
            langPick.SetActive(true);
        }
        


        PlayerPrefs.SetInt("shuffle", 0);

        
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.45f);
            volumeSlider.value = 0.45f;
        }
        else
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
            SetVolume(PlayerPrefs.GetFloat("volume"));
        }

        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetFloat("music", 0.45f);
            musicSlider.value = 0.45f;
        }
        else
        {
            musicSlider.value = PlayerPrefs.GetFloat("music");
            SetVolume(PlayerPrefs.GetFloat("music"));
        }
        SetQuality(5);

        resolutions = Screen.resolutions;

        int currentResolutionID = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            
                string resolutionToDrop = resolutions[i].width + " x " + resolutions[i].height;
                resolutionsToDrop.Add(resolutionToDrop);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionID = i;
                }
            
        }

    }

    public void SetResolution(int resolution)
    {
        Resolution resolutionsList = resolutions[resolution];
        Screen.SetResolution(resolutionsList.width, resolutionsList.height, Screen.fullScreen);
    }

    public void SetShuffle(bool isShuffling)
    {
        if (isShuffling)
            PlayerPrefs.SetInt("shuffle", 1);
        else
            PlayerPrefs.SetInt("shuffle", 0);
    }
    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("volume", volume);
        float gamevol = Mathf.Lerp(-40, 10, volume);
        uwaga.SetActive(false);
        if(volume == 0)
        {
            gamevol = -80;
            uwaga.SetActive(true);
        }
        audioMixer.SetFloat("wholevol", gamevol);
    }
    public void SetMusic(float volume)
    {
        PlayerPrefs.SetFloat("music", volume);
        float gamevol = Mathf.Lerp(-40, 10, volume);
        if (volume == 0)
        {
            gamevol = -80;
        }
        musicMixer.SetFloat("music", gamevol);
    }

    public void SetQuality(int qualityid)
    {
        QualitySettings.SetQualityLevel(qualityid);
    }

    public void SetFullScreen(bool isFullScreened)
    {
        Screen.fullScreen = isFullScreened;
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }
}

