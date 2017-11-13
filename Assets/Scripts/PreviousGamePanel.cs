using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviousGamePanel : MonoBehaviour
{
    [SerializeField]
    GameObject Panel;
    [SerializeField]
    Text Winner;
    [SerializeField]
    Text Losers;

    void Awake()
    {
        string[] names = Data.LoadLastGame();

        if (names.Length == 0)
        {
            Panel.SetActive(false);
            return;
        }

        Winner.text = names[0];

        string losersText = string.Empty;

        for (int i = 1; i < names.Length; i++)
        {
            losersText = string.Concat(losersText, names[i], ".  ");
        }

        Losers.text = losersText;
    }
}