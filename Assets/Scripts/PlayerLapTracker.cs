using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLapTracker : MonoBehaviour
{

    [SerializeField]
    private float currentLap;
    [SerializeField]
    private float currentCheckpoint;

    private int outOfBound;
    private int checkpointLayer;

    public GameObject finishText;
    
    private void Awake()
    {
        outOfBound = LayerMask.NameToLayer("OutOfBound");
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == outOfBound)
            OutOfBounds();
        else if (other.gameObject.layer == checkpointLayer)
        {
            string checkpointName = other.gameObject.transform.parent.name;
            if (checkpointName == "Finish")
            {
                EndGame();
            }
            else if((currentCheckpoint+1).ToString() == checkpointName )
                currentCheckpoint++;
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
