using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardTracker : MonoBehaviour
{

    [Header("Gameobject which contains all characters")]
    public GameObject charactersList;

    [Header("Gameobject which contains all checkpoints")]
    public GameObject checkpointList;

    [Header("UI elements that tracks the player rank")]
    public TextMeshProUGUI playerRankText;
    
    private Character[] characters;
    private Transform checkpoints;

    private void Awake()
    {
        characters = charactersList.GetComponentsInChildren<Character>();
        checkpoints = checkpointList.transform;
    }

    private void Update()
    {
        // Updates characters distance to next checkpoint
        foreach(Character character in characters)
        {
            int characterNextCheckpointNumber = character.currentCheckpoint + 1;
            Transform characterNextCheckpoint = GetCheckpointTransform(characterNextCheckpointNumber.ToString());
            character.distanceToNextCheckpoint = Vector3.Distance(character.transform.position, characterNextCheckpoint.position);
        }
    }

    private Transform GetCheckpointTransform(string number)
    {
        foreach (Transform t in checkpoints)
            if (t.name == number)
                return t;
        return null;
    }
}
