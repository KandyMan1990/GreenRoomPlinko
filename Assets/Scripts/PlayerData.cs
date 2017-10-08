[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Played;
    public int Wins;

    public PlayerData(string name)
    {
        Name = name;
        Played = 0;
        Wins = 0;
    }
}