using UnityEngine;
using UnityEngine.UI;

public class PlayerUIComponent : MonoBehaviour
{
    [SerializeField]
    Text PlayerName;
    [SerializeField]
    Text Played;
    [SerializeField]
    Text Wins;

    bool InGame;

    public void SetInGame(bool value)
    {
        InGame = value;
    }

    public void SetName(string value)
    {
        PlayerName.text = value;
    }

    public void SetPlayed(int value)
    {
        Played.text = value.ToString();
    }

    public void SetWins(int value)
    {
        Wins.text = value.ToString();
    }
}