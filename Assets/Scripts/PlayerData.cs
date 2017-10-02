[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Wins;

    public PlayerData(string name)
    {
        Name = name;
        Wins = 0;
    }

    public PlayerData(string name, int wins)
    {
        Name = name;
        Wins = wins;
    }
}