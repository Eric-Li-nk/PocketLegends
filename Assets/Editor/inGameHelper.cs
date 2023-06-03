using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class inGameHelper : EditorWindow
{

    [MenuItem("Tools/inGameHelper")]
    public static void ShowWindow()
    {
        GetWindow<inGameHelper>().Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Add Player"))
        {
            
        }
    }
}
