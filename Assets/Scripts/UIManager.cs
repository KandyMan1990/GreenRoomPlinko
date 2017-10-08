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
        Players = Data.Load();
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
        Data.Save(Players);
    }

    public void AddNewPlayerButtonOnClick(string name)
    {
        //if Players.Count < 15, add name to list of players
        //instantiate the new player into the panel
        //testing stuff below
        PlayerData pd = new PlayerData(name);
        Players.Add(pd);
        InstantiateNewPlayer(pd);
    }

    void InstantiateNewPlayer(PlayerData player)
    {
        GameObject go = Instantiate(PlayerUIPrefab, Panel);
        PlayerUIComponent ui = go.GetComponent<PlayerUIComponent>();

        ui.SetName(player.Name);
        ui.SetPlayed(player.Played);
        ui.SetWins(player.Wins);
    }
}