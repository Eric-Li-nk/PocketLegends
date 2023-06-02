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
    public int totalCheckpoint;

    private int outOfBound;
    private int checkpointLayer;
    
    public TextMeshProUGUI checkpointTrackerText;
    public TextMeshProUGUI lapTrackerText;
    public TextMeshProUGUI rankText;
    
    private Character character;

    public bool raceIsLoop = false;
    public int totalLap;
    
    private void Awake()
    {
        outOfBound = LayerMask.NameToLayer("OutOfBound");
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        character = transform.GetComponent<Character>();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        checkpointTrackerText.SetText(String.Format("Checkpoint 0/{0}", totalCheckpoint));
        lapTrackerText.SetText(String.Format("Lap 0/{0}", totalLap));
    }

    private void Update()
    {
        rankText.text = character.rank.ToString();
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
                if (currentCheckpoint == totalCheckpoint && raceIsLoop)
                {
                    checkpointTrackerText.SetText(String.Format("Checkpoint 0/{0}",totalCheckpoint));
                    if (currentLap + 1 == totalLap)
                        checkpointTrackerText.SetText(String.Format("Checkpoint {0}/{1}",currentCheckpoint,totalCheckpoint));
                    currentLap++;
                    character.currentLap = currentLap;
                    lapTrackerText.SetText(String.Format("Lap {0}/{1}",currentLap, totalLap));
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

    private int GetNextCheckpoint()
    {
        if (currentCheckpoint == totalCheckpoint)
            return 1;
        return currentCheckpoint + 1;
    }
}
