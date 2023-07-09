using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using TMPro;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RaceTrackListManager : EditorWindow
{
    private Scene currentScene;
    private Transform raceTrackList;
    private List<string> sceneList = new List<string>();
    private StartMenuUIManager uiManager;

    private List<string> raceTrackNameList = new List<string>();

    private GUIStyle style;
    
    [MenuItem("Tools/Race Track Manager")]
    public static void ShowWindow()
    {
        GetWindow<RaceTrackListManager>().Show();
    }

    private void OnGUI()
    {
        if (currentScene.name == "StartMenu")
        {
            int i = 1;
            foreach (Transform raceTrack in raceTrackList)
            {
                string raceTrackName = raceTrackNameList[i - 1];
                raceTrackNameList[i-1] = EditorGUILayout.TextField("Race Track " + i + ":", raceTrackName);
                i++;
            }

            if (GUILayout.Button("Set Race Track List"))
            {
                i = 0;
                foreach (Transform raceTrack in raceTrackList)
                {
                    string raceTrackName = raceTrackNameList[i];
                    Button raceTrackButton = raceTrack.GetComponentInChildren<Button>();
                    if (sceneList.Contains(raceTrackName))
                    {
                        raceTrack.GetComponentInChildren<TMP_Text>().text = raceTrackName;
                        UnityEventTools.RegisterStringPersistentListener(raceTrackButton.onClick,0, uiManager.LoadScene, raceTrackName);
                    }
                    else if(raceTrackName != "")
                    {
                        Debug.LogError("Race track " + (i + 1) + " ne contient pas un nom valide ! La scène " + raceTrackName +" n'existe pas !");
                    }
                    i++;
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("Wrong Scene !", style);
        }
    }

    private void Awake()
    {
        style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
        GetSceneList();
        CheckCurrentScene();
    }

    private void OnDidOpenScene()
    { 
        CheckCurrentScene();
    }

    private void CheckCurrentScene()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "StartMenu")
        {
            GetRaceTrackList();
            GetStartMenuUIManager();
            int i = 0;
            foreach (Transform raceTrack in raceTrackList)
            {
                string raceTrackName = raceTrack.GetComponentInChildren<TMP_Text>().text;
                if (raceTrackName != "Placeholder")
                    raceTrackNameList.Add(raceTrackName);
                else
                    raceTrackNameList.Add("");
                i++;
            }
        }
    }

    private void GetSceneList()
    {
        int numberOfScenes = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < numberOfScenes; i++)
        {
            string sceneName = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            if(sceneName != "StartMenu")
                sceneList.Add(sceneName);
        }
    }

    private void GetRaceTrackList()
    {
        raceTrackList = currentScene.GetRootGameObjects()[0].transform.Find("ChooseTrackMenu").Find("TrackGridLayout");
    }

    private void GetStartMenuUIManager()
    {
        foreach(GameObject gameObject in currentScene.GetRootGameObjects())
            if (gameObject.name == "Scripts")
                uiManager = gameObject.GetComponent<StartMenuUIManager>();
    }
}
