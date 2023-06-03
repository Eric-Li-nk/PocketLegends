using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StartMenuUIManager : UIManager
{

    public TMP_Dropdown playerCountDropdown;
    public Transform playerNameInputFieldList;

    public Game gameData;
    
    protected override void Start()
    {
        base.Start();
        playerCountDropdown.value = gameData.playerCount - 1;
    }
    
    public void SavePlayers()
    {
        gameData.playerCount = playerCountDropdown.value + 1;
        gameData.playerName = new List<string>();

        TMP_InputField[] playerNameList = playerNameInputFieldList.GetComponentsInChildren<TMP_InputField>();

        int i = 1;
        foreach (TMP_InputField playerNameInputField in playerNameList)
        {
            string playerName = playerNameInputField.text;
            if (playerName == String.Empty)
                gameData.playerName.Add("Player " + i);
            else
                gameData.playerName.Add(playerNameInputField.text);
            i++;
        }
    }

    public void TogglePlayerNameInputField(int val)
    {
        for (int i = 0; i < playerNameInputFieldList.childCount; i++)
        {
            GameObject playerNameGameObject = playerNameInputFieldList.GetChild(i).gameObject;
            if(!playerNameGameObject.activeSelf)
                playerNameGameObject.GetComponentInChildren<TMP_InputField>().text = String.Empty;
            if(i <= val)
                playerNameGameObject.SetActive(true);
            else
                playerNameGameObject.SetActive(false);
        }
    }
}
