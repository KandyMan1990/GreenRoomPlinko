using System;
using UnityEngine;
using UnityEngine.UI;

public class FirstSceneManager : MonoBehaviour
{
    [SerializeField]
    VisualConfig defaultVisualConfig;
    [SerializeField]
    VisualConfig christmasVisualConfig;
    [SerializeField]
    Image background;

    VisualConfig visualConfig;

    void Awake()
    {
        SetVisualConfig();
        background.sprite = visualConfig.Background;
    }

    void SetVisualConfig()
    {
        if (DateTime.Now.Month == 12 && DateTime.Now.Day <= 25)
        {
            visualConfig = christmasVisualConfig;
            return;
        }

        visualConfig = defaultVisualConfig;
    }
}