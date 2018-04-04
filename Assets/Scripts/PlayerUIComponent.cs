using UnityEngine;
using UnityEngine.UI;

public class PlayerUIComponent : MonoBehaviour
{
    [SerializeField]
    Text Position;
    [SerializeField]
    Text PlayerName;
    [SerializeField]
    Text Played;
    [SerializeField]
    Text Score;
    [SerializeField]
    PlayerData playerData;
    [SerializeField]
    Button DeleteButton;
    public Image background;
    UIManager uiManager;

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

    public void CreateComponent(PlayerData player, int index)
    {
        playerData = player;

        Position.text = index.ToString("00");
        PlayerName.text = playerData.Name;
        Played.text = playerData.Played.ToString();
        Score.text = playerData.Score.ToString();
    }

    public void DeletePlayer()
    {
        uiManager.DeletePlayer(playerData);
    }

    public PlayerData GetPlayerData
    {
        get { return playerData; }
    }

    public void SetUiManager(UIManager manager)
    {
        uiManager = manager;
    }
}