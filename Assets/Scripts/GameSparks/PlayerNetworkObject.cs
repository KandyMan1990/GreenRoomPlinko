using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerNetworkObject : NetworkBehaviour
{
    [SerializeField] GameObject prefab;

    void Start()
    {
        if (isServer)
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += SceneChanged;
        }
        else if (isClient)
        {
            Init();
        }
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= SceneChanged;
    }

    void SceneChanged(Scene current, Scene next)
    {
        Init();
    }

    void Init()
    {
        Transform go = FindObjectOfType<UIPlayerPanel>().transform;
        Instantiate(prefab, go);
    }
}