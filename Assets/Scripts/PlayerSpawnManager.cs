using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public static PlayerSpawnManager Instance { get; private set; }

    [SerializeField]
    List<Transform> SpawnPoints;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnPlayers(List<PlayerData> players, GameObject obj)
    {
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
            //set text on go to be the first three letters of the name
        }
    }
}