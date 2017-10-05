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
}