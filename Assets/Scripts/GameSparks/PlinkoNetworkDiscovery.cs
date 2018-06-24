using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlinkoNetworkDiscovery : NetworkDiscovery
{
    [SerializeField] User user;
    [SerializeField] GameObject availableMatchPrefab;
    [SerializeField] Transform availableGamesPanel;
    Dictionary<string, PlinkoBroadcastData> broadcasts;

    void Start()
    {
        bool init = Initialize();

        if (init)
        {
            StartAsClient();

            broadcasts = new Dictionary<string, PlinkoBroadcastData>();

            for (int i = 0; i < 7; i++)
            {
                GameObject go = Instantiate(availableMatchPrefab, availableGamesPanel);
                go.SetActive(false);
            }
        }
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        PlinkoBroadcastData plinkoBroadcastData = JsonUtility.FromJson<PlinkoBroadcastData>(data);

        broadcasts[fromAddress] = plinkoBroadcastData;

        UpdateList();
    }

    public void CreateGame()
    {
        StopBroadcast();

        PlinkoBroadcastData plinkoBroadcastData = new PlinkoBroadcastData(user.Name, user.Room);

        broadcastData = plinkoBroadcastData.SaveToString();

        StartAsServer();
        NetworkManager.singleton.StartHost();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void UpdateList()
    {
        foreach (Transform child in availableGamesPanel)
        {
            child.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            child.gameObject.SetActive(false);
        }

        int i = 0;
        foreach (KeyValuePair<string, PlinkoBroadcastData> kvp in broadcasts)
        {
            GameObject go = availableGamesPanel.GetChild(i).gameObject;
            go.GetComponentInChildren<Text>().text = kvp.Value.Room;
            go.GetComponentInChildren<Button>().onClick.AddListener(() =>
            {
                StopBroadcast();
                NetworkManager.singleton.networkAddress = kvp.Key;
                NetworkManager.singleton.StartClient();

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            });
            go.SetActive(true);
            i++;
        }
    }
}

[System.Serializable]
public struct PlinkoBroadcastData
{
    public string Name;
    public string Room;

    public string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    public PlinkoBroadcastData(string name, string room)
    {
        Name = name;
        Room = room;
    }
}