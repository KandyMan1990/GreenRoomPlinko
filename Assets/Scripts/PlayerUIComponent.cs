﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerUIComponent : MonoBehaviour
{
    [SerializeField]
    Text PlayerName;
    [SerializeField]
    Text Played;
    [SerializeField]
    Text Wins;
    [SerializeField]
    PlayerData playerData;
    public Image background;

    bool inGame;

    public void SetInGame(bool value)
    {
        inGame = value;
        UpdateInGameStatus();
    }

    void UpdateInGameStatus()
    {
        if (inGame)
            GameManager.Instance.AddPlayer(playerData);
        else
            GameManager.Instance.RemovePlayer(playerData);
    }

    public void IncrementPlayed()
    {
        playerData.Played++;
    }

    public void IncrementWins()
    {
        playerData.Wins++;
    }

    public void CreateComponent(PlayerData player)
    {
        playerData = player;

        PlayerName.text = playerData.Name;
        Played.text = playerData.Played.ToString();
        Wins.text = playerData.Wins.ToString();
    }

    public PlayerData GetPlayerData
    {
        get { return playerData; }
    }
}