using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLapTracker : MonoBehaviour
{

    [SerializeField] private float currentLap;
    [SerializeField] private float currentCheckpoint;
    [SerializeField] private int totalCheckpoint;
    
    private int outOfBound;
    private int checkpointLayer;

    public GameObject finishText;
    public TextMeshProUGUI checkpointTrackerText;
    
    private void Awake()
    {
        outOfBound = LayerMask.NameToLayer("OutOfBound");
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
    }

    private void Start()
    {
        checkpointTrackerText.SetText(String.Format("Checkpoint 0/{0}",totalCheckpoint));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == outOfBound)
            OutOfBounds();
        else if (other.gameObject.layer == checkpointLayer)
        {
            string checkpointName = other.gameObject.transform.parent.name;
            if (checkpointName == "Finish" && currentCheckpoint + 1 == totalCheckpoint)
            {
                checkpointTrackerText.SetText(String.Format("Checkpoint {0}/{0}",totalCheckpoint));
                EndGame();
            }
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
        finishText.SetActive(true);
    }
}
