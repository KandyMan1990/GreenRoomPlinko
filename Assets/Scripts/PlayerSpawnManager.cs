using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public static PlayerSpawnManager Instance { get; private set; }

    [SerializeField]
    List<Transform> SpawnPoints;
    [SerializeField]
    GameObject playersPanel;
    PlayerUIComponent[] playersUIs;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnPlayers(List<PlayerData> players, GameObject obj)
    {
        playersUIs = playersPanel.GetComponentsInChildren<PlayerUIComponent>();

        Transform[] shuffledPoints = new Transform[SpawnPoints.Count];
        SpawnPoints.CopyTo(shuffledPoints);

        int n = shuffledPoints.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, (n + 1));
            Transform value = shuffledPoints[k];
            shuffledPoints[k] = shuffledPoints[n];
            shuffledPoints[n] = value;
        }

        for (int i = 0; i < players.Count; i++)
        {
            GameObject go = Instantiate(obj, shuffledPoints[i].position, Quaternion.identity);

            PlayerGameobject pgo = go.GetComponent<PlayerGameobject>();
            pgo.SetName(players[i].Name.Substring(0, 3));
            pgo.SetPlayerData(players[i]);

            for (int j = 0; j < playersUIs.Length; j++)
            {
                if (pgo.GetPlayerData == playersUIs[j].GetPlayerData)
                {
                    Color c = go.GetComponent<SpriteRenderer>().color;
                    c.a = 0.5f;
                    playersUIs[j].background.color = c;

                    playersUIs[j].IncrementPlayed();
                }
            }
        }
    }
}