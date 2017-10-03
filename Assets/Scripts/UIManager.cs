using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerUIPrefab;
    [SerializeField]
    Transform Panel;
    [SerializeField]
    Button NewPlayerButton;
    List<PlayerData> Players = new List<PlayerData>();

    void Awake()
    {
        //read in all player data (players, wins) into Players
        //testing stuff below
        Players.Add(new PlayerData("Jim"));
        Players.Add(new PlayerData("Jam", 3));
        Players.Add(new PlayerData("Dude", 5));
    }

    void Start()
    {
        foreach (PlayerData player in Players)
        {
            InstantiateNewPlayer(player);
        }
    }

    void OnApplicationQuit()
    {
        //write all player data from players to file
    }

    public void AddNewPlayerButtonOnClick(Text name)
    {
        //if Players.Count < 15, add name to list of players
        //instantiate the new player into the panel
        //testing stuff below
        PlayerData pd = new PlayerData(name.text);
        Players.Add(pd);
        InstantiateNewPlayer(pd);
    }

    void InstantiateNewPlayer(PlayerData player)
    {
        GameObject go = Instantiate(PlayerUIPrefab, Panel);
        PlayerUIComponent ui = go.GetComponent<PlayerUIComponent>();

        ui.SetName(player.Name);
        ui.SetWins(player.Wins);
    }
}