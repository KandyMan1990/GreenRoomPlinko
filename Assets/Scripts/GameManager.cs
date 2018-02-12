using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    Button[] uiButtons;
    [SerializeField]
    GameObject playersPanel;
    [SerializeField]
    UIManager uiManager;
    [SerializeField]
    GameObject Ceiling;
    [SerializeField]
    GameObject landingZonesGameObject;
    [SerializeField]
    GameObject winnersPanel;
    [SerializeField]
    Text winnerText;
    [SerializeField]
    Text loserText;
    [SerializeField]
    SpriteRenderer background;
    [SerializeField]
    VisualConfigService visualConfigService;

    WaitForSeconds wait = new WaitForSeconds(2f);
    LandingZone[] landingZones;
    Toggle[] toggles;
    PlayerUIComponent[] playersUIs;
    VisualConfig visualConfig;

    public static GameManager Instance { get; private set; }
    List<PlayerData> players = new List<PlayerData>();
    List<FinishedPlayer> finishedPlayers = new List<FinishedPlayer>();
    List<FinishedPlayer> playersToRemove = new List<FinishedPlayer>();
    List<PlayerData> losers = new List<PlayerData>();
    int currentRound;

    void Awake()
    {
        Instance = this;
        visualConfig = visualConfigService.GetVisualConfig;

        background.sprite = visualConfig.Background;
        playerPrefab.GetComponent<SpriteRenderer>().sprite = visualConfig.BallSprite;
    }

    void Start()
    {
        landingZones = landingZonesGameObject.GetComponentsInChildren<LandingZone>();
    }

    public void AddPlayer(PlayerData player)
    {
        players.Add(player);
    }

    public void RemovePlayer(PlayerData player)
    {
        players.Remove(player);
    }

    public void AddAllPlayers()
    {
        toggles = playersPanel.GetComponentsInChildren<Toggle>();

        foreach (Toggle t in toggles)
        {
            t.isOn = true;
        }
    }

    public void RemoveAllPlayers()
    {
        toggles = playersPanel.GetComponentsInChildren<Toggle>();

        foreach (Toggle t in toggles)
        {
            t.isOn = false;
        }
    }

    public void ResetGame()
    {
        if (players.Count == 0)
        {
            ErrorManager.Instance.ShowError("No players entered into game");
            return;
        }

        if (players.Count == 1)
        {
            ErrorManager.Instance.ShowError("More than 1 player should be entered into the game");
            return;
        }

        finishedPlayers = new List<FinishedPlayer>();
        playersToRemove = new List<FinishedPlayer>();
        losers = new List<PlayerData>();

        currentRound = 0;
        AudioManager.Instance.PlayPlayerSpawn();
        PlayerSpawnManager.Instance.SpawnPlayers(players, playerPrefab);

        foreach (Button btn in uiButtons)
        {
            btn.interactable = false;
        }   

        toggles = playersPanel.GetComponentsInChildren<Toggle>();

        foreach (Toggle t in toggles)
        {
            t.interactable = false;
        }

        NextRound();
    }

    void PrepareNextRound()
    {
        AudioManager.Instance.PlayPlayerTransition();

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

        if (playersToRemove.Contains(fp))
        {
            ColourPool.Instance.ReturnColour(fp.GetPlayerGameobject.GetColour);

            foreach (Transform obj in playersPanel.transform)
            {
                PlayerUIComponent pui = obj.GetComponent<PlayerUIComponent>();

                if (pui != null && pui.GetPlayerData == fp.GetPlayerGameobject.GetPlayerData)
                {
                    pui.background.color = new Color(0f, 0f, 0f, 0f);
                }
            }

            Destroy(fp.GetTransform.gameObject);
            losers.Add(fp.GetPlayerGameobject.GetPlayerData);
        }

        else
        {
            Vector3 pos = fp.GetTransform.position;
            pos.y += 9;
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
        }

        finishedPlayers.Remove(fp);

        if (finishedPlayers.Count == 0)
            NextRound();
    }

    void NextRound()
    {
        playersToRemove.Clear();
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

    void EndGame(string winner, string[] losersArray)
    {
        if (currentRound == 1)
        {
            AudioManager.Instance.PlayInstantWin();
        }

        AudioManager.Instance.PlayWinner();
        uiManager.Commit();

        winnerText.text = winner;
        winnersPanel.SetActive(true);

        string losersText = string.Empty;

        for (int i = 0; i < losersArray.Length; i++)
        {
            losersText = string.Concat(losersText, losersArray[i], ".  ");
        }

        loserText.text = losersText;

        foreach (Button btn in uiButtons)
        {
            btn.interactable = true;
        }

        uiManager.CreatePlayersList();

        GameObject[] playerGameObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject g in playerGameObjects)
        {
            StartCoroutine(FinalFade(g));
        }

        LandingZone.ResetInstantWin();
        foreach (LandingZone l in landingZones)
        {
            l.RevertToOriginalState();
        }

        players.Clear();

        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("First");
        async.allowSceneActivation = false;

        yield return new WaitForSeconds(AudioManager.Instance.GetWinnerLength);

        async.allowSceneActivation = true;
    }

    IEnumerator FinalFade(GameObject go)
    {
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

        ColourPool.Instance.ReturnColour(go.GetComponent<PlayerGameobject>().GetColour);
        Destroy(go);
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

        if (finishedPlayers.Count + losers.Count == players.Count)
        {
            CheckWinners();
            if (finishedPlayers.Count - playersToRemove.Count == 1)
            {
                string winner = string.Empty;
                string[] loserNames = new string[players.Count - 1];
                playersUIs = playersPanel.GetComponentsInChildren<PlayerUIComponent>();
                int i = 0;

                foreach (PlayerUIComponent ui in playersUIs)
                {
                    PlayerData playerData = ui.GetPlayerData;

                    if (players.Contains(playerData))
                    {
                        ui.background.color = new Color(0f, 0f, 0f, 0f);
                        ui.IncrementPlayed();

                        if (losers.Contains(playerData) || playersToRemove.Any(x => x.GetPlayerGameobject.GetPlayerData == playerData))
                        {
                            ui.IncreaseScore(1);
                            loserNames[i] = playerData.Name;
                            i++;
                        }
                        else
                        {
                            ui.IncreaseScore(-players.Count + 1);
                            winner = playerData.Name;
                        }
                    }
                }

                Data.Save(winner, loserNames);

                EndGame(winner, loserNames);
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
            if (finishedPlayers.Any(element => element.GetInstantWinner))
            {
                return false;
            }

            return true;
        }

        if (finishedPlayers.All(element => !element.GetWinner))
        {
            if (finishedPlayers.Any(element => element.GetInstantWinner))
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public int CurrentRound
    {
        get { return currentRound; }
    }
}