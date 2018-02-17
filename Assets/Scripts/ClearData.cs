using UnityEngine;
using UnityEngine.UI;
using System;

public class ClearData : MonoBehaviour
{
    string key = "ClearData";

    void Awake()
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, DateTime.Today.Year - 1);
        }
        else
        {
            if (PlayerPrefs.GetInt(key) == DateTime.Today.Year)
            {
                GetComponent<Button>().interactable = false;
            }
        }
    }

    public void Clear()
    {
        PlayerPrefs.SetInt(key, DateTime.Today.Year);
        GetComponent<Button>().interactable = false;
    }
}