using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLapTracker : MonoBehaviour
{

    [SerializeField] private float currentLap;
    [SerializeField] private float currentCheckpoint;
    [SerializeField] private int totalCheckpoint;

    public PlayerMovement playerMovement;

    public GameObject cameraThirdPerson;
    public GameObject cameraThirdPersonFly;
    
    private int outOfBound;
    private int checkpointLayer;
    private int changeStateLayer;
    
    public GameObject finishText;
    public TextMeshProUGUI checkpointTrackerText;
    
    private void Awake()
    {
        outOfBound = LayerMask.NameToLayer("OutOfBound");
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        changeStateLayer = LayerMask.NameToLayer("ChangeState");
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
        else if (other.gameObject.layer == changeStateLayer)
        {
            if (playerMovement.fly == true)
            {
                playerMovement.fly = false;
                cameraThirdPersonFly.SetActive(false);
                cameraThirdPerson.SetActive(true);
            }
            else
            {
                playerMovement.fly = true;
                cameraThirdPersonFly.SetActive(true);
                cameraThirdPerson.SetActive(false);
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
