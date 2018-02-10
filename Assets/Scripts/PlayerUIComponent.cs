using UnityEngine;
using UnityEngine.UI;

public class PlayerUIComponent : MonoBehaviour
{
    [SerializeField]
    Text PlayerName;
    [SerializeField]
    Text Played;
    [SerializeField]
    Text Score;
    [SerializeField]
    PlayerData playerData;
    public Image background;

    bool inGame;

    public void SetInGame(bool value)
    {
        inGame = value;
        UpdateInGameStatus();
    }

    void UpdateInGameStatus()
    {
        if (inGame)
            GameManager.Instance.AddPlayer(playerData);
        else
            GameManager.Instance.RemovePlayer(playerData);
    }

    public void IncrementPlayed()
    {
        playerData.Played++;
        Played.text = playerData.Played.ToString();
    }

    public void IncreaseScore(int value)
    {
        playerData.Score += value;
        Score.text = playerData.Score.ToString();
    }

    public void CreateComponent(PlayerData player)
    {
        playerData = player;

        PlayerName.text = playerData.Name;
        Played.text = playerData.Played.ToString();
        Score.text = playerData.Score.ToString();
    }

    public PlayerData GetPlayerData
    {
        get { return playerData; }
    }
}