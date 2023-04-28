using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLapTracker : MonoBehaviour
{

    [SerializeField] private int currentLap;
    [SerializeField] private int currentCheckpoint;
    [SerializeField] private int totalCheckpoint;

    public PlayerMovement playerMovement;

    public GameObject cameraThirdPerson;
    public GameObject cameraThirdPersonFly;
    
    private int outOfBound;
    private int checkpointLayer;
    private int changeStateLayer;
    
    public GameObject finishMenu;
    public TextMeshProUGUI checkpointTrackerText;

    private Character character;

    public Transform checkpoints;
    
    private void Awake()
    {
        outOfBound = LayerMask.NameToLayer("OutOfBound");
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
        changeStateLayer = LayerMask.NameToLayer("ChangeState");
        character = transform.GetComponent<Character>();
        totalCheckpoint = GetTotalCheckpointCount();
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
            if (currentCheckpoint + 1 == totalCheckpoint)
            {
                checkpointTrackerText.SetText(String.Format("Checkpoint {0}/{0}",totalCheckpoint));
                EndGame();
            }
            // If it is the next checkpoint, increment the current checkpoint by 1
            else if((currentCheckpoint+1).ToString() == checkpointName )
            {
                currentCheckpoint++;
                character.currentCheckpoint = currentCheckpoint;
                checkpointTrackerText.SetText(String.Format("Checkpoint {0}/{1}",currentCheckpoint,totalCheckpoint));
            }
        }
        // If the checkpoint is of the changeStateLayer, makes the player be able to fly if he wasn't flying else makes it no longer be able to fly
        else if (other.gameObject.layer == changeStateLayer)
        {
            if (playerMovement.fly)
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
        finishMenu.SetActive(true);
    }

    private int GetTotalCheckpointCount()
    {
        int total = -1;
        foreach (Transform t in checkpoints)
            total++;
        return total;
    }
}
