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
    List<PlayerData> Players = new List<PlayerData>();

    void Awake()
    {
        CountdownText.gameObject.SetActive(false);
        RoundNumberText.gameObject.SetActive(false);
        Players = Data.Load();

        VersionText.text = string.Format("V{0}.{1}.{2}", MajorVersion, MinorVersion, Revision);

        foreach (PlayerData player in Players)
        {
            InstantiateNewPlayer(player);
        }
    }

    public void Commit()
    {
        Data.Save(Players);
    }

    public void AddNewPlayerButtonOnClick(string name)
    {
        if (Players.Count < 15)
        {
            PlayerData pd = new PlayerData(name);
            Players.Add(pd);
            InstantiateNewPlayer(pd);
        }
        else
        {
            Debug.Log("maximum players reached");
        }
    }

    void InstantiateNewPlayer(PlayerData player)
    {
        GameObject go = Instantiate(PlayerUIPrefab, Panel);
        PlayerUIComponent ui = go.GetComponent<PlayerUIComponent>();

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
}