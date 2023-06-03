using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LeaderboardTracker : MonoBehaviour
{

    [Header("Gameobject which contains all characters")]
    public GameObject charactersList;

    [Header("Gameobject which contains all checkpoints")]
    public Transform checkpointList;

    [Header("UI elements that makes the leaderboard")]
    public GameObject leaderboardContent;
    public RowUI rowUI;

    private Character[] characters;
    private Transform checkpoints;
    private int totalCheckpoint;
    [HideInInspector]
    public int totalLap;
    
    public GameObject finishMenu;
    
    private void Awake()
    {
        checkpoints = checkpointList.transform;
        totalCheckpoint = GetTotalCheckpointCount();
    }

    public void Start()
    {
        characters = charactersList.GetComponentsInChildren<Character>();
    }

    private void Update()
    {
        // Updates characters distance to next checkpoint
        foreach (Character character in characters)
        {
            int characterNextCheckpointNumber =
                (character.currentCheckpoint == totalCheckpoint) ? 1 : character.currentCheckpoint + 1;
            Transform characterNextCheckpoint = GetCheckpointTransform(characterNextCheckpointNumber.ToString());
            character.distanceToNextCheckpoint =
                Vector3.Distance(character.transform.position, characterNextCheckpoint.position);
        }

        // Sorts character list by their rank in the race track
        Array.Sort(characters);
        // Sets the characters rank
        for (int i = 0; i < characters.Length; i++)
            characters[i].rank = i + 1;
        if (characters[0].currentLap == totalLap && characters[0].currentCheckpoint == totalCheckpoint)
            EndGame();
    }

    private Transform GetCheckpointTransform(string number)
    {
        foreach (Transform t in checkpoints)
            if (t.name == number)
                return t;
        return null;
    }
    
    private void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        finishMenu.SetActive(true);
        ShowLeaderboard();
        enabled = false;
    }

    public void ShowLeaderboard()
    {
        foreach (Character character in characters)
        {
            var row = Instantiate(rowUI, leaderboardContent.transform).GetComponent<RowUI>();
            row.playerName.text = character.name;
            row.rank.text = character.rank.ToString();
        }
    }
    
    private int GetTotalCheckpointCount()
    {
        int total = -1;
        foreach (Transform t in checkpoints)
            if(t.gameObject.activeSelf)
                total++;
        return total;
    }
    
}
