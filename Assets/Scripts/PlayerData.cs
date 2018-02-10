[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Played;
    public int Score;
    public int FreeGamesAvailable;

    public PlayerData(string name)
    {
        Name = name;
        Played = 0;
        Score = 0;
        FreeGamesAvailable = 0;
    }

    public override string ToString()
    {
        return Name;
    }
}