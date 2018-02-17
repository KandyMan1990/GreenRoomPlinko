using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    Text VersionText;
    [SerializeField]
    int MajorVersion;
    [SerializeField]
    int MinorVersion;
    [SerializeField]
    int Revision;
    [SerializeField]
    Text CountdownText;
    [SerializeField]
    Text RoundNumberText;
    [SerializeField]
    TableOrderPrefs tableOrderPrefs;

    List<PlayerData> Players = new List<PlayerData>();

    void Awake()
    {
        if (CountdownText)
            CountdownText.gameObject.SetActive(false);

        if (RoundNumberText)
            RoundNumberText.gameObject.SetActive(false);

        VersionText.text = string.Format("V{0}.{1}.{2}", MajorVersion, MinorVersion, Revision);
    }

    void Start()
    {
        CreatePlayersList();
    }

    public void Commit()
    {
        Data.Save(Players);
    }

    public void CreatePlayersList()
    {
        Players = Data.Load();

        if (Players.Count > 0)
        {
            Players = tableOrderPrefs.OrderByAscending ? Players.OrderBy(x => x.Score).Reverse().ToList() : Players.OrderBy(x => x.Score).ToList();

            if (Panel.childCount > 0)
            {
                List<GameObject> children = new List<GameObject>();
                foreach (Transform child in Panel)
                {
                    children.Add(child.gameObject);
                }
                foreach (GameObject child in children)
                {
                    Destroy(child);
                }
            }

            foreach (PlayerData player in Players)
            {
                InstantiateNewPlayer(player);
            }
        }
    }

    public void DeletePlayer(PlayerData pd)
    {
        Players.Remove(pd);

        Data.Save(Players);

        CreatePlayersList();
    }

    public void AddNewPlayerButtonOnClick(string name)
    {
        if (Players.Count < 15)
        {
            PlayerData pd = new PlayerData(name);
            Players.Add(pd);
            InstantiateNewPlayer(pd);

            Commit();
        }
        else
        {
            ErrorManager.Instance.ShowError("Maximum players reached");
        }
    }

    void InstantiateNewPlayer(PlayerData player)
    {
        GameObject go = Instantiate(PlayerUIPrefab, Panel);
        PlayerUIComponent ui = go.GetComponent<PlayerUIComponent>();
        ui.SetUiManager(this);

        ui.CreateComponent(player);
    }

    public void SetCountdownText(int timer)
    {
        setCountdownText(timer);
    }

    public void SetCountdownText(int timer, int round)
    {
        RoundNumberText.text = "ROUND " + round;

        RoundNumberText.gameObject.SetActive(true);

        setCountdownText(timer);
    }

    void setCountdownText(int timer)
    {
        CountdownText.text = timer.ToString();

        if (timer != 0)
        {
            CountdownText.gameObject.SetActive(true);
            return;
        }

        CountdownText.gameObject.SetActive(false);
        RoundNumberText.gameObject.SetActive(false);
    }

    public void ClearData()
    {
        foreach (PlayerData pd in Players)
        {
            pd.FreeGamesAvailable = 0;
            pd.Played = 0;
            pd.Score = 0;
        }

        Data.Save(Players);

        CreatePlayersList();
    }
}