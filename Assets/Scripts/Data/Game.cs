using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class Game : ScriptableObject
{

    public int playerCount;
    public List<PlayerInput> playerInput;
    public List<string> playerName;
    public List<GameObject> playerPrefab;
    public List<int> playerScore;

    public int currentRaceTrackIndex;
    public List<string> raceTrackList;
}
