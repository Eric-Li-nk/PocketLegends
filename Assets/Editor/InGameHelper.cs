using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameHelper : EditorWindow
{

    private Scene currentScene;

    private LeaderboardTracker leaderboardTracker;
    
    [MenuItem("Tools/In Game Helper")]
    public static void ShowWindow()
    {
        GetWindow<InGameHelper>().Show();
    }
    private void OnGUI()
    {
        if (GUILayout.Button("End Game"))
        {
            leaderboardTracker.EndGame();
        }
    }

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        GetLeaderboardTracker();
    }

    private void GetLeaderboardTracker()
    {
        foreach(GameObject gameObject in currentScene.GetRootGameObjects())
            if (gameObject.name == "Scripts")
                leaderboardTracker = gameObject.GetComponent<LeaderboardTracker>();
    }
    
}
