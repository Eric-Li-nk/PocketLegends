using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartMenuPlayerManager : MonoBehaviour
{
    public List<PlayerInput> players;
    
    public PlayerInputManager playerInputManager;

    public TMP_Dropdown playerCountDropdown;

    private void Awake()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
        playerInputManager.onPlayerLeft += RemovePlayer;
        InputSystem.onDeviceChange += RemovePlayer;
    }

    private void OnEnable()
    {
        playerInputManager.EnableJoining();
    }

    private void OnDisable()
    {
        playerInputManager.DisableJoining();
    }

    private void RemovePlayer(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Removed)
        {
            foreach (PlayerInput player in players)
            {
                if (player.devices.Contains(device))
                {
                    Destroy(player.gameObject);
                    return;
                }
            }
        }
    }

    private void AddPlayer(PlayerInput player)
    {
        players.Add(player);
        playerCountDropdown.value++;
        
    }

    private void RemovePlayer(PlayerInput player)
    {
        players.Remove(player);
        playerCountDropdown.value--;
    }
}
