using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuHelper : MonoBehaviour
{
    
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    public Slider audioSlider;
    public AudioMixer audioMixer;
    
    // Resolution dropdown variables
    private Resolution[] resolutions;
    private int currentResolutionIndex;
    private List<string> options = new List<string>();
    
    // Quality dropdown variables
    private string[] qualities;
    private int currentQualityIndex;
    
    private float currentVolume;
    
    private void Start()
    {
        GenerateResolutionDropdown();
        GenerateQualityDropdown();
        LoadSettings();
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void SetResolution(int index)
    {
        currentResolutionIndex = index;
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreenMode);
    }

    public void SetVolume(float value)
    {
        currentVolume = value;
        audioMixer.SetFloat("MasterVolume", value);
    }

    public void SetQuality(int index)
    {
        currentResolutionIndex = index;
        QualitySettings.SetQualityLevel(index);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("Resolution",
            currentResolutionIndex);
        PlayerPrefs.SetInt("Fullscreen",
                    Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("AudioVolume", 
                    currentVolume);
        PlayerPrefs.SetInt("Quality",
            currentQualityIndex);
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("Resolution"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
            SetResolution(PlayerPrefs.GetInt("Resolution"));
        }
        else
            resolutionDropdown.value = currentResolutionIndex;
        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen"));
            fullscreenToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen"));
        }
        else
        {
            Screen.fullScreen = true;
            fullscreenToggle.isOn = true;
        }
        if (PlayerPrefs.HasKey("AudioVolume"))
            audioSlider.value = PlayerPrefs.GetFloat("AudioVolume");
        else
            audioSlider.value = 0.0f;
        if (PlayerPrefs.HasKey("Quality"))
            qualityDropdown.value = PlayerPrefs.GetInt("Quality");
        else
            qualityDropdown.value = qualities.Length - 1;
    }

    public void GenerateResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + ", " + resolutions[i].refreshRate + " Hz";
            options.Add(option);
            if (Screen.currentResolution.width == resolutions[i].width &&
                Screen.currentResolution.height == resolutions[i].height)
                currentResolutionIndex = i;
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        options.Clear();
    }

    public void GenerateQualityDropdown()
    {
        qualityDropdown.ClearOptions();
        qualities = QualitySettings.names;

        for (int i = 0; i < qualities.Length; i++)
        {
            options.Add(qualities[i]);
            if(QualitySettings.GetQualitySettings().name == qualities[i])
                currentQualityIndex = i;
        }

        qualityDropdown.AddOptions(options);
        qualityDropdown.value = currentQualityIndex;
        options.Clear();
    }
}
