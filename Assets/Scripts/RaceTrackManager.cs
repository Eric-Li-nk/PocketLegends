using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTrackManager : MonoBehaviour
{

    [SerializeField] private bool isLoop;
    [SerializeField] private int totalLap;
    private int totalCheckpoint;
    
    [SerializeField] private Transform checkpoints;
    
    [Header("Gameobject which contains all characters")]
    public GameObject charactersList;
    private PlayerLapTracker[] charactersLapTracker;
    
    public LeaderboardTracker lt;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        lt.totalLap = totalLap;
        totalCheckpoint = GetTotalCheckpointCount();
        charactersLapTracker = charactersList.GetComponentsInChildren<PlayerLapTracker>();
        foreach (PlayerLapTracker plt in charactersLapTracker)
        {
            plt.totalCheckpoint = totalCheckpoint;
        }
        if (isLoop)
        {
            foreach (PlayerLapTracker plt in charactersLapTracker)
            {
                plt.totalLap = totalLap;
                plt.raceIsLoop = true;
            }
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
