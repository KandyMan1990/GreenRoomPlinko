[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Played;
    public int Wins;
    public int FreeGamesAvailable;

    public PlayerData(string name)
    {
        Name = name;
        Played = 0;
        Wins = 0;
        FreeGamesAvailable = 0;
    }

    public override string ToString()
    {
        return Name;
    }
}