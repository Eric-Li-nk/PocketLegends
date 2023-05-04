using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrackManager : MonoBehaviour
{

    [SerializeField] private bool isLoop;
    [SerializeField] private int totalLap;
    
    [Header("Gameobject which contains all characters")]
    public GameObject charactersList;
    private PlayerLapTracker[] charactersLapTracker;

    public void Awake()
    {
        charactersLapTracker = charactersList.GetComponentsInChildren<PlayerLapTracker>();
        if (isLoop)
        {
            foreach (PlayerLapTracker plt in charactersLapTracker)
            {
                plt.totalLap = totalLap;
                plt.raceIsLoop = true;
            }
        }
    }
}
