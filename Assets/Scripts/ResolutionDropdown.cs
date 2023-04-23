using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropdown : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private List<string> options = new List<string>();
    private int currentResolutionIndex;
    private void Start()
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
    }
}
