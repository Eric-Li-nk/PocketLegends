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
    // UI elements
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    public Slider audioSlider;
    public AudioMixer audioMixer;
    
    // Resolution dropdown variables
    private Resolution[] resolutions;
    private int currentResolutionIndex;
    private List<string> resolutionOptions = new List<string>();
    
    // Quality dropdown variables
    private string[] qualities;
    private int currentQualityIndex;
    private List<string> qualityOptions = new List<string>();
    
    private float currentVolume;
    
    private void Start()
    {
        GenerateResolutionDropdown();
        GenerateQualityDropdown();
        LoadSettings();
    }

    // Loads the scene named "Game"
    // eventually needs to be changed to be able to play a chosen track
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    // Closes the game
    public void Quit()
    {
        Application.Quit();
    }

    // Sets the game in fullscreen
    public void SetFullscreen(bool val)
    {
        Screen.fullScreen = val;
    }

    // Sets the resolution
    public void SetResolution(int index)
    {
        currentResolutionIndex = index;
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreenMode);
    }

    // Sets the master volume
    public void SetVolume(float value)
    {
        currentVolume = value;
        audioMixer.SetFloat("MasterVolume", value);
    }
    
    // Sets the quality preset
    public void SetQuality(int index)
    {
        currentResolutionIndex = index;
        QualitySettings.SetQualityLevel(index);
    }
    
    // Saves settings set by the player in the PlayerPrefs class
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("Resolution",
            resolutionDropdown.value);
        PlayerPrefs.SetInt("Fullscreen",
            Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("AudioVolume", 
            currentVolume);
        PlayerPrefs.SetInt("Quality",
            qualityDropdown.value);
    }
    
    // Loads settings set by the player in the settings menu
    // If a settings hasn't been set, a default value is set in the else statement
    // By changing the value of the UI gameObject, onValueChanged is called each time, it is the reason why we we don't change the game's settings directly
    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("Resolution"))
            resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        else
            resolutionDropdown.value = currentResolutionIndex;
        if (PlayerPrefs.HasKey("Fullscreen"))
            fullscreenToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen"));
        else
            fullscreenToggle.isOn = true;
        if (PlayerPrefs.HasKey("AudioVolume"))
            audioSlider.value = PlayerPrefs.GetFloat("AudioVolume");
        else
            audioSlider.value = 0.0f;
        if (PlayerPrefs.HasKey("Quality"))
            qualityDropdown.value = PlayerPrefs.GetInt("Quality");
        else
            qualityDropdown.value = qualities.Length - 1;
    }
    
    // Populates the resolution dropdown and selects the current resolution
    public void GenerateResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + ", " + resolutions[i].refreshRate + " Hz";
            resolutionOptions.Add(option);
            if (Screen.currentResolution.width == resolutions[i].width &&
                Screen.currentResolution.height == resolutions[i].height)
                currentResolutionIndex = i;
        }
        
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
    }
    
    // Populates the quality dropdown and selects the current quality preset
    public void GenerateQualityDropdown()
    {
        qualityDropdown.ClearOptions();
        qualities = QualitySettings.names;

        for (int i = 0; i < qualities.Length; i++)
        {
            qualityOptions.Add(qualities[i]);
            if(QualitySettings.GetQualitySettings().name == qualities[i])
                currentQualityIndex = i;
        }

        qualityDropdown.AddOptions(qualityOptions);
        qualityDropdown.value = currentQualityIndex;
    }
}
