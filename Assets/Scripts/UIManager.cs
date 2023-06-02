using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // UI elements
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown languageDropdown;
    public TMP_Dropdown playerCount;
    public Toggle fullscreenToggle;
    public Toggle FPSToggle;
    public Slider audioSlider;
    public AudioMixer audioMixer;
    
    // Game data
    public Game gameData;
    
    // Resolution dropdown variables
    private Resolution[] resolutions;
    private List<string> resolutionOptions = new List<string>();
    
    // Quality dropdown variables
    private string[] qualities;
    private List<string> qualityOptions = new List<string>();
    
    private float currentVolume;
    private bool currentFPSToggle;
    
    private void Start()
    {
        GenerateResolutionDropdown();
        GenerateQualityDropdown();
        GenerateLanguageDropdown();
        LoadSettings();
        playerCount.value = gameData.playerCount - 1;
    }

    // Loads the scene with the given variable name
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    // Closes the game
    public void Quit()
    {
        Application.Quit();
    }

    // Sets the resolution
    public void SetResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreenMode);
    }
    
    // Sets the quality preset
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    
    // Sets the language
    public void SetLanguage(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
    
    // Sets the game in fullscreen
    public void SetFullscreen(bool val)
    {
        Screen.fullScreen = val;
    }
    
    // Sets the master volume
    public void SetVolume(float value)
    {
        currentVolume = value;
        audioMixer.SetFloat("MasterVolume", value);
    }

    public void SetFPSToggle(bool val)
    {
        currentFPSToggle = val;
    }

    public void SetPlayerCount(int val)
    {
        gameData.playerCount = val + 1;
    }
    
    // Saves settings set by the player in the PlayerPrefs class
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("Resolution",
            resolutionDropdown.value);
        PlayerPrefs.SetInt("Quality",
                    qualityDropdown.value);
        PlayerPrefs.SetInt("Language",
            languageDropdown.value);
        PlayerPrefs.SetInt("Fullscreen",
            Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("AudioVolume", 
            currentVolume);
        PlayerPrefs.SetInt("FPS",
            Convert.ToInt32(currentFPSToggle));
    }
    
    // Loads settings set by the player in the settings menu
    // If a settings hasn't been set, a default value is set in the else statement
    // By changing the value of the UI gameObject, onValueChanged is called each time, it is the reason why we we don't change the game's settings directly
    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("Resolution"))
            resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        else
            resolutionDropdown.value = resolutions.Length - 1;
        if (PlayerPrefs.HasKey("Quality"))
            qualityDropdown.value = PlayerPrefs.GetInt("Quality");
        else
            qualityDropdown.value = qualities.Length - 1;
        if (PlayerPrefs.HasKey("Language"))
            languageDropdown.value = PlayerPrefs.GetInt("Language");
        else
            languageDropdown.value = 0;
        if (PlayerPrefs.HasKey("Fullscreen"))
            fullscreenToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen"));
        else
            fullscreenToggle.isOn = true;
        if (PlayerPrefs.HasKey("AudioVolume"))
            audioSlider.value = PlayerPrefs.GetFloat("AudioVolume");
        else
            audioSlider.value = 0.0f;
        if (PlayerPrefs.HasKey("FPS"))
            FPSToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("FPS"));
        else
            FPSToggle.isOn = false;

    }

    public void ResetSettings()
    {
        PlayerPrefs.DeleteAll();
        LoadSettings();
    }
    
    // Populates the resolution dropdown and selects the current resolution
    private void GenerateResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;
        
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
    private void GenerateQualityDropdown()
    {
        qualityDropdown.ClearOptions();
        qualities = QualitySettings.names;
        int currentQualityIndex = 0;
        
        for (int i = 0; i < qualities.Length; i++)
        {
            qualityOptions.Add(qualities[i]);
            if(QualitySettings.GetQualitySettings().name == qualities[i])
                currentQualityIndex = i;
        }

        qualityDropdown.AddOptions(qualityOptions);
        qualityDropdown.value = currentQualityIndex;
    }

    // Populates the language dropdown and selects the current language
    private void GenerateLanguageDropdown()
    {
        LocalizationSettings.InitializationOperation.WaitForCompletion();
        
        var languages = new List<TMP_Dropdown.OptionData>();
        int currentLanguage = 0;
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            var language = LocalizationSettings.AvailableLocales.Locales[i];
            if (LocalizationSettings.SelectedLocale == language)
                currentLanguage = i;
            languages.Add(new TMP_Dropdown.OptionData(language.name));
        }
        
        languageDropdown.options = languages;
        languageDropdown.value = currentLanguage;
    }
}
