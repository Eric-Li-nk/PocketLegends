using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(LeaderboardTracker))]
public class RaceTrackManager : MonoBehaviour
{

    [SerializeField] private bool isLoop;
    [SerializeField] private int totalLap;
    private int totalCheckpoint;

    [SerializeField] private MovementType startingMovementType;
    
    [SerializeField] private Transform checkpoints;
    
    [Header("Gameobject which contains all characters")]
    public GameObject charactersList;
    private PlayerLapTracker[] charactersLapTracker;
    private BotLapTracker[] botLapTracker;
    private SwapMovement[] charactersSwapMovements;
    private PlayerNavMesh[] AINavMeshList;

    private LeaderboardTracker lt;
    
    private void Awake()
    {
        lt = GetComponent<LeaderboardTracker>();
    }

    private void Start()
    {
        lt.totalLap = totalLap;
        totalCheckpoint = GetTotalCheckpointCount();
        charactersLapTracker = charactersList.GetComponentsInChildren<PlayerLapTracker>();
        botLapTracker = charactersList.GetComponentsInChildren<BotLapTracker>();
        charactersSwapMovements = charactersList.GetComponentsInChildren<SwapMovement>();
        AINavMeshList = charactersList.GetComponentsInChildren<PlayerNavMesh>();
        foreach (PlayerLapTracker plt in charactersLapTracker)
            plt.totalCheckpoint = totalCheckpoint;
        foreach (BotLapTracker blt in botLapTracker)
            blt.totalCheckpoint = totalCheckpoint;
        if (isLoop)
        {
            foreach (PlayerLapTracker plt in charactersLapTracker)
            {
                plt.totalLap = totalLap;
                plt.raceIsLoop = true;
            }
            foreach (BotLapTracker blt in botLapTracker)
            {
                blt.totalLap = totalLap;
                blt.raceIsLoop = true;
            }
        }
        else
        {
            foreach (PlayerLapTracker plt in charactersLapTracker)
                plt.lapTrackerText.gameObject.SetActive(false);
        }

        foreach (SwapMovement swapMovement in charactersSwapMovements)
            swapMovement.MovementType = startingMovementType;


        List<Transform> checkpointList = new List<Transform>();

        foreach (Transform t in checkpoints)
            if(t.name != "Spawn")
                checkpointList.Add(t);

        foreach (PlayerNavMesh AInavMesh in AINavMeshList)
        {
            AInavMesh.checkpointTransforms = checkpointList;
            AInavMesh.loopCircuit = isLoop;
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
