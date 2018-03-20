using System;

[System.Serializable]
public class PlayerData : IComparable<PlayerData>
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

    public int CompareTo(PlayerData other)
    {
        if (Score > other.Score)
            return 1;

        if (Score < other.Score)
            return -1;

        if (Played < other.Played)
            return 1;

        if (Played > other.Played)
            return -1;

        return 0;
    }

    public override string ToString()
    {
        return Name;
    }
}