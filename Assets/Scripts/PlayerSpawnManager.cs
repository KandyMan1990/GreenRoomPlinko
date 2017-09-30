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
}