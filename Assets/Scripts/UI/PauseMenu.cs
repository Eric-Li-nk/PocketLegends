using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject finishMenuUI;

    public Game gameData;

    private void Start()
    {
        if (IsLastTrack())
        {
            finishMenuUI.transform.Find("Next Race Button").gameObject.SetActive(false);
            finishMenuUI.transform.Find("Return to Title Button").gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(finishMenuUI.activeSelf)
                ReturnToTitle();
            else if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void NextRaceTrack()
    {
        if (!IsLastTrack())
        {
            gameData.currentRaceTrackIndex++;
            Time.timeScale = 1f;
            SceneManager.LoadScene(gameData.raceTrackList[gameData.currentRaceTrackIndex]);
        }
        else
        {
            ReturnToTitle();
        }
    }
    
    public void ReturnToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
    
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        HideCursor();
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        ShowCursor();
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private bool IsLastTrack()
    {
        return gameData.currentRaceTrackIndex >= gameData.raceTrackList.Count - 1;
    }
}
