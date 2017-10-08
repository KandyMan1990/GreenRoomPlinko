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
        for (int i = 0; i < players.Count; i++)
        {
            GameObject go = Instantiate(obj, SpawnPoints[i].position, Quaternion.identity);
            //set text on go to be the first three letters of the name
        }
    }
}