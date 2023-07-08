using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class StartMenuUIManager : UIManager
{

    public TMP_Dropdown playerCountDropdown;
    public Transform playerNameInputFieldList;
    public Transform playerCharacterSelectionDropdownList;

    public string charactersFolderPath;
    
    public Game gameData;
    
    public Object[] characterPrefabList;
    
    protected override void Start()
    {
        base.Start();
        playerCountDropdown.value = gameData.playerCount - 1;
        characterPrefabList = Resources.LoadAll(charactersFolderPath);
        GeneratePlayerCharacterSelectionDropdown();
    }
    
    public void SavePlayers()
    {
        gameData.playerCount = playerCountDropdown.value + 1;
        gameData.playerName = new List<string>();
        gameData.playerPrefab = new List<GameObject>();
        
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

        TMP_Dropdown[] playerCharacterSelectionList = playerCharacterSelectionDropdownList.GetComponentsInChildren<TMP_Dropdown>();
        
        i = 0;
        foreach (TMP_Dropdown playerCharacterSelectionDropdown in playerCharacterSelectionList)
        {
            int playerSelectedCharacterIndex = playerCharacterSelectionDropdown.value;
            GameObject playerSelectedCharacter = characterPrefabList[playerSelectedCharacterIndex] as GameObject;
            gameData.playerPrefab.Add(playerSelectedCharacter);
            i++;
        }
        
    }
    
    private void GeneratePlayerCharacterSelectionDropdown()
    {
        List<string> characterNameList = new List<string>();
        
        foreach (var character in characterPrefabList)
            characterNameList.Add(character.name);

        TMP_Dropdown[] playerCharacterSelectionList = playerCharacterSelectionDropdownList.GetComponentsInChildren<TMP_Dropdown>(true);
        
        foreach (TMP_Dropdown playerCharacterSelectionDropdown in playerCharacterSelectionList)
        {
            playerCharacterSelectionDropdown.ClearOptions();
            playerCharacterSelectionDropdown.AddOptions(characterNameList);
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
    
    public void TogglePlayerCharacterSelectionDropdown(int val)
    {
        for (int i = 0; i < playerCharacterSelectionDropdownList.childCount; i++)
        {
            GameObject playerCharacterGameObject = playerCharacterSelectionDropdownList.GetChild(i).gameObject;
            if(!playerCharacterGameObject.activeSelf)
                playerCharacterGameObject.GetComponentInChildren<TMP_Dropdown>().value = 0;
            if(i <= val)
                playerCharacterGameObject.SetActive(true);
            else
                playerCharacterGameObject.SetActive(false);
        }
    }
}
