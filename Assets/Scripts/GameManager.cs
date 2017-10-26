﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    Button startGameButton;
    [SerializeField]
    GameObject playersPanel;
    [SerializeField]
    UIManager uiManager;
    [SerializeField]
    GameObject Ceiling;

    public static GameManager Instance { get; private set; }
    List<PlayerData> players = new List<PlayerData>();
    int currentRound;

    void Awake()
    {
        Instance = this;
    }

    public void AddPlayer(PlayerData player)
    {
        players.Add(player);
    }

    public void RemovePlayer(PlayerData player)
    {
        players.Remove(player);
    }

    public void ResetGame()
    {
        if (players.Count == 0)
        {
            Debug.LogError("no players entered into game");
            return;
        }

        if (players.Count == 1)
        {
            Debug.LogError("more than 1 player should be entered into game");
            return;
        }

        currentRound = 0;
        PlayerSpawnManager.Instance.SpawnPlayers(players, playerPrefab);
        startGameButton.interactable = false;
        Toggle[] toggles = playersPanel.GetComponentsInChildren<Toggle>();

        foreach (Toggle t in toggles)
        {
            t.interactable = false;
        }

        NextRound();
    }

    void StartGame()
    {
        //not necessarily to go here, but
        //when a ball is in landing zone and velocity is 0, call event
        //event checks if all balls in zone and at 0, if so, check next round
        //if not, continue as normal
        //if velocity 0 but not all balls in zone, temp disable colliders of those not in zone for 1/4 a second to free stuck balls
        //could potentially check this periodically
    }

    void NextRound()
    {
        EnableCeiling(true);
        currentRound++;
        StartCoroutine(Countdown());
        //on 0, disable top bar
        //when all players are in a box and not moving
        //  if all players in green or red, next round
        //  if any players in yellow, remove green and red players, next round
        //      else remove players in green, next round
        //  if only 1 player in red, end game
        //  when preparing for next round, reenable top bar, move players back to top
    }

    IEnumerator Countdown()
    {
        float timer = 3f;

        uiManager.SetCountdownText(Mathf.CeilToInt(timer), currentRound);
        yield return null;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            uiManager.SetCountdownText(Mathf.CeilToInt(timer));
            yield return null;
        }

        uiManager.SetCountdownText(Mathf.CeilToInt(0));
        EnableCeiling(false);
    }

    void EndGame()
    {
        startGameButton.interactable = true;
        Toggle[] toggles = playersPanel.GetComponentsInChildren<Toggle>();

        foreach (Toggle t in toggles)
        {
            t.interactable = true;
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            Destroy(p);
        }

        //show winner plus all players who were in
    }

    void EnableCeiling(bool enabled)
    {
        Ceiling.SetActive(enabled);
    }
}