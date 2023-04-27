using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VehicleLapTracker : MonoBehaviour
{
    [Header("Ce Script n'est utilisé que temporairement le temps de faire un autre script plus grobal pour les trackages des checkpoints")]
    [SerializeField] private float currentLap;
    [SerializeField] private float currentCheckpoint;
    [SerializeField] private int totalCheckpoint;

    private int outOfBound;
    private int checkpointLayer;

    public GameObject finishMenu;
    public TextMeshProUGUI checkpointTrackerText;
    
    private void Awake()
    {
        outOfBound = LayerMask.NameToLayer("OutOfBound");
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
    }

    private void Start()
    {
        checkpointTrackerText.SetText(String.Format("Checkpoint 0/{0}", totalCheckpoint));
    }

    private void OnTriggerEnter(Collider other)
    {
        // Resets the position of the player if he is out of bounds to the last checkpoint
        if(other.gameObject.layer == outOfBound)
            OutOfBounds();
        else if (other.gameObject.layer == checkpointLayer)
        {
            string checkpointName = other.gameObject.transform.parent.name;
            // If it is the last checkpoint, ends the game
            if (checkpointName == "Finish" && currentCheckpoint + 1 == totalCheckpoint)
            {
                checkpointTrackerText.SetText(String.Format("Checkpoint {0}/{0}",totalCheckpoint));
                EndGame();
            }
            // If it is the next checkpoint, increment the current checkpoint by 1
            else if((currentCheckpoint+1).ToString() == checkpointName )
            {
                currentCheckpoint++;
                checkpointTrackerText.SetText(String.Format("Checkpoint {0}/{1}",currentCheckpoint,totalCheckpoint));
            }
        }
    }

    public void OutOfBounds()
    {
        transform.position = GameObject.Find("Checkpoints").transform.Find(currentCheckpoint.ToString()).position;
    }

    void EndGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        finishMenu.SetActive(true);
    }
}
