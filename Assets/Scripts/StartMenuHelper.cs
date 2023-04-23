using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuHelper : MonoBehaviour
{

    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
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
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreenMode);
    }
}
