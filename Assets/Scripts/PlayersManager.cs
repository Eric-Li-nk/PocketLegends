using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField] private List<Transform> playerSpawns;
    [SerializeField] private List<LayerMask> playerLayers;

    private PlayerInputManager playerInputManager;
    
    public Transform characters;

    [SerializeField] private Game gameData;
    private LeaderboardTracker lt;
    private RaceTrackManager rtm;
    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        lt = GetComponent<LeaderboardTracker>();
        rtm = GetComponent<RaceTrackManager>();
    }

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject player = Instantiate(gameData.playerPrefab[i],characters);
            player.name = gameData.playerName[i];
            if (i >= gameData.playerCount)
                player.transform.position = playerSpawns[i].position;
        }
            
        rtm.enabled = true;
        lt.enabled = true;
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }
    
    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);
        player.transform.position = playerSpawns[players.Count - 1].position;

        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);

        foreach (CinemachineFreeLook cam in player.GetComponentsInChildren<CinemachineFreeLook>(includeInactive:true))
            cam.gameObject.layer = layerToAdd;
        foreach (CinemachineVirtualCamera cam in player.GetComponentsInChildren<CinemachineVirtualCamera>(includeInactive:true))
            cam.gameObject.layer = layerToAdd;
        player.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;
        player.GetComponentInChildren<ThirdPersonCam>(includeInactive: true).horizontal = player.actions.FindAction("Look");
        //player.GetComponentInChildren<ThirdPersonCam>(includeInactive: true).objOrientation = player.actions.FindAction("Move");
    }

    private void AddAI(Transform AI)
    {
        
    }
}
