using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class StartMenuUIManager : UIManager
{

    public TMP_Dropdown playerCountDropdown;
    public Transform playerNameInputFieldList;
    public Transform playerCharacterSelectionDropdownList;

    public string charactersFolderPath;
    
    public Game gameData;
    
    public Object[] characterPrefabList;

    private List<Button> raceTrackButtonList = new List<Button>();
    private List<string> SelectedRaceTrackList = new List<string>();
    private int numberOfSelectedRaceTrack = 0;
    
    protected override void Start()
    {
        base.Start();
        playerCountDropdown.value = gameData.playerCount - 1;
        characterPrefabList = Resources.LoadAll(charactersFolderPath);
        GeneratePlayerCharacterSelectionDropdown();
    }

    public void Play()
    {
        if (SelectedRaceTrackList.Count > 0)
        {
            SaveRaceTrackList();
            LoadScene(gameData.raceTrackList[0]);
        }
    }
    
    public void SavePlayers()
    {
        gameData.playerCount = playerCountDropdown.value + 1;
        gameData.playerName = new List<string>();
        gameData.playerPrefab = new List<GameObject>();
        gameData.playerScore = new List<int>();

        gameData.currentRaceTrackIndex = 0;
        
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
        
        for(i = 0; i < gameData.playerCount; i++)
            gameData.playerScore.Add(0);
    }

    private void SaveRaceTrackList()
    {
        gameData.currentRaceTrackIndex = 0;
        gameData.raceTrackList = SelectedRaceTrackList;
    }

    public void OnClickRaceTrack(Button raceTrackButton)
    {
        string raceTrackName = raceTrackButton.transform.Find("Text").GetComponent<TMP_Text>().text;
        TMP_Text raceTrackIndexTMP = raceTrackButton.transform.Find("Index").GetComponent<TMP_Text>();
        if (!SelectedRaceTrackList.Contains(raceTrackName))
        {
            raceTrackButtonList.Add(raceTrackButton);
            SelectRaceTrack(raceTrackName, raceTrackIndexTMP);
        }
        else
        {
            DeselectRaceTrack(raceTrackName, raceTrackIndexTMP, raceTrackButton);
        }
    }

    private void SelectRaceTrack(string raceTrackName, TMP_Text raceTrackIndexTMP)
    {
        numberOfSelectedRaceTrack++;
        SelectedRaceTrackList.Add(raceTrackName);
        raceTrackIndexTMP.text = numberOfSelectedRaceTrack.ToString();
    }

    private void DeselectRaceTrack(string raceTrackName, TMP_Text raceTrackIndexTMP, Button raceTrackButton)
    {
        int SelectedRaceTrackIndex = SelectedRaceTrackList.IndexOf(raceTrackName);
        for (int i = SelectedRaceTrackIndex; i < SelectedRaceTrackList.Count-1; i++)
        {
            SelectedRaceTrackList[i] = SelectedRaceTrackList[i + 1];
            raceTrackButtonList[i] = raceTrackButtonList[i + 1];
            raceTrackButtonList[i].transform.Find("Index").GetComponent<TMP_Text>().text = (i+1).ToString();
        }
        SelectedRaceTrackList.RemoveAt(SelectedRaceTrackList.Count-1);
        raceTrackButtonList.RemoveAt(raceTrackButtonList.Count-1);
        raceTrackIndexTMP.text = "";
        numberOfSelectedRaceTrack--;
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
