using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerUIPrefab;
    [SerializeField]
    Transform Panel;
    List<GameObject> Players = new List<GameObject>();

    void Awake()
    {
        //read in all player data (players, wins) into Players.  Type will have to change from gameobject to player to store name and wins
    }

    void Start()
    {
        foreach (GameObject player in Players)
        {
            Instantiate(player, Panel);
        }
    }

    void OnApplicationQuit()
    {
        //write all player data from players to file
    }
}