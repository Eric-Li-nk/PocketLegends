using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class PlayerLapTracker : MonoBehaviour
{

    [Serialize] private int currentLap;
    [Serialize] private int currentCheckpoint;
    [Serialize] private int totalCheckpoint;

    private int outOfBound;
    private int checkpointLayer;

    public GameObject finishMenu;
    public TextMeshProUGUI checkpointTrackerText;
    public TextMeshProUGUI lapTrackerText;
    
    private Character character;

    public Transform checkpoints;
    
    // Temporary
    public LeaderboardTracker lt;

    public bool raceIsLoop = false;
    public int totalLap;
    
    private void Awake()
    {
        outOfBound = LayerMask.NameToLayer("OutOfBound");
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        character = transform.GetComponent<Character>();
        totalCheckpoint = GetTotalCheckpointCount();
    }

    private void Start()
    {
        checkpointTrackerText.SetText(String.Format("Checkpoint 0/{0}", totalCheckpoint));
        lapTrackerText.SetText(String.Format("Lap 0/{0}", totalLap));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == checkpointLayer)
        {
            int checkpointName = Convert.ToInt32(other.gameObject.transform.parent.name);
            int nextCheckpoint = GetNextCheckpoint();
            // If it is the next checkpoint, increment the current checkpoint by 1
            if( nextCheckpoint == checkpointName )
            {
                currentCheckpoint = nextCheckpoint;
                character.currentCheckpoint = currentCheckpoint;
                checkpointTrackerText.SetText(String.Format("Checkpoint {0}/{1}",currentCheckpoint,totalCheckpoint));
                if (currentCheckpoint == totalCheckpoint)
                {
                    if (raceIsLoop)
                    {
                        checkpointTrackerText.SetText(String.Format("Checkpoint 0/{0}",totalCheckpoint));
                        if (currentLap + 1 == totalLap)
                        {
                            checkpointTrackerText.SetText(String.Format("Checkpoint {0}/{1}",currentCheckpoint,totalCheckpoint));
                            EndGame();
                        }
                            
                        currentLap++;
                        character.currentLap = currentLap;
                        lapTrackerText.SetText(String.Format("Lap {0}/{1}",currentLap, totalLap));
                    }
                    else
                        EndGame();
                }
            }
        }
        // Resets the position of the player if he is out of bounds to the last checkpoint
        else if(other.gameObject.layer == outOfBound)
            OutOfBounds();
    }

    private void OutOfBounds()
    {
        if (currentCheckpoint == 0 && currentLap == 0)
            transform.position = GameObject.Find("Spawn").transform.position;
        else
            transform.position = GameObject.Find("Checkpoints").transform.Find(currentCheckpoint.ToString()).position;
    }

    private void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        finishMenu.SetActive(true);
        lt.ShowLeaderboard();
    }

    private int GetTotalCheckpointCount()
    {
        int total = -1;
        foreach (Transform t in checkpoints)
            if(t.gameObject.activeSelf)
                total++;
        return total;
    }

    private int GetNextCheckpoint()
    {
        if (currentCheckpoint == totalCheckpoint)
            return 1;
        return currentCheckpoint + 1;
    }
}
