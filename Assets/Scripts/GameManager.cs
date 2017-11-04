﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    GameObject landingZonesGameObject;
    WaitForSeconds wait = new WaitForSeconds(2f);
    LandingZone[] landingZones;
    Toggle[] toggles;

    public static GameManager Instance { get; private set; }
    List<PlayerData> players = new List<PlayerData>();
    List<FinishedPlayer> finishedPlayers = new List<FinishedPlayer>();
    List<FinishedPlayer> playersToRemove = new List<FinishedPlayer>();
    int currentRound;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        landingZones = landingZonesGameObject.GetComponentsInChildren<LandingZone>();
        toggles = playersPanel.GetComponentsInChildren<Toggle>();
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

        foreach (Toggle t in toggles)
        {
            t.interactable = false;
        }

        NextRound();
    }

    void PrepareNextRound()
    {
        foreach (FinishedPlayer fp in finishedPlayers)
        {
            StartCoroutine(PrepareGameObject(fp));
        }
    }

    IEnumerator PrepareGameObject(FinishedPlayer fp)
    {
        GameObject go = fp.GetPlayerGameobject.gameObject;
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        TrailRenderer tr = go.GetComponent<TrailRenderer>();
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        Text text = go.GetComponentInChildren<Text>();
        Color srColour = sr.color;
        Color textColour = text.color;
        float alpha = 1f;

        tr.enabled = false;
        rb.simulated = false;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            text.color = new Color(textColour.r, textColour.g, textColour.b, alpha);
            sr.color = new Color(srColour.r, srColour.g, srColour.b, alpha);
            yield return null;
        }

        Vector3 pos = fp.GetTransform.position;
        pos.y = 5;
        fp.GetTransform.position = pos;

        fp.GetPlayerGameobject.ResetObj();

        while (alpha < 1)
        {
            alpha += Time.deltaTime;
            text.color = new Color(textColour.r, textColour.g, textColour.b, alpha);
            sr.color = new Color(srColour.r, srColour.g, srColour.b, alpha);
            yield return null;
        }

        tr.enabled = true;
        rb.simulated = true;

        finishedPlayers.Remove(fp);

        if (finishedPlayers.Count == 0)
            NextRound();
    }

    void NextRound()
    {
        currentRound++;
        LandingZone.ResetInstantWin();
        foreach (LandingZone l in landingZones)
        {
            l.ResetLandingZone();
        }
        StartCoroutine(Countdown());
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
        StartCoroutine(FadeCeiling());
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

    IEnumerator FadeCeiling()
    {
        SpriteRenderer sr = Ceiling.GetComponent<SpriteRenderer>();
        BoxCollider2D bc = Ceiling.GetComponent<BoxCollider2D>();
        Color c = sr.color;

        bc.enabled = false;

        while(c.a > 0.5f)
        {
            c.a -= Time.deltaTime * 2;
            sr.color = c;
            yield return null;
        }

        yield return wait;

        while (c.a < 1f)
        {
            c.a += Time.deltaTime * 2;
            sr.color = c;
            yield return null;
        }

        bc.enabled = true;
    }

    public void PlayerFinished(FinishedPlayer fp)
    {
        finishedPlayers.Add(fp);
        if (finishedPlayers.Count == players.Count)
        {
            CheckWinners();
            if (finishedPlayers.Count == 1)
            {
                Debug.Log("winner is " + finishedPlayers[0].GetPlayerGameobject.GetPlayerData.Name + "!");
                return;
            }

            PrepareNextRound();
        }
    }

    void CheckWinners()
    {
        if (ReplayRound())
        {
            return;
        }

        if (finishedPlayers.Any(element => element.GetInstantWinner))
        {
            foreach (FinishedPlayer fp in finishedPlayers)
            {
                if (!fp.GetInstantWinner)
                {
                    playersToRemove.Add(fp);
                }
            }

            foreach (FinishedPlayer fp in playersToRemove)
            {
                Destroy(fp.GetTransform.gameObject);
                finishedPlayers.Remove(fp);
            }
            playersToRemove.Clear();
            return;
        }

        if (finishedPlayers.Any(element => element.GetWinner))
        {
            foreach (FinishedPlayer fp in finishedPlayers)
            {
                if (!fp.GetWinner)
                {
                    playersToRemove.Add(fp);
                }
            }

            foreach (FinishedPlayer fp in playersToRemove)
            {
                Destroy(fp.GetTransform.gameObject);
                finishedPlayers.Remove(fp);
            }
            playersToRemove.Clear();
            return;
        }
    }

    bool ReplayRound()
    {
        if (finishedPlayers.All(element => element.GetInstantWinner))
        {
            return true;
        }

        if (finishedPlayers.All(element => element.GetWinner))
        {
            return true;
        }

        if (finishedPlayers.All(element => !element.GetWinner))
        {
            return true;
        }

        return false;
    }
}