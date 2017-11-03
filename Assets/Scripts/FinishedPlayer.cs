using UnityEngine;

public class FinishedPlayer
{
    PlayerGameobject playerGameobject;
    bool winner;
    bool instantWinner;
    Transform transform;

    public FinishedPlayer(PlayerGameobject pgo, Transform trans, bool win, bool instantWin)
    {
        playerGameobject = pgo;
        winner = win;
        instantWinner = instantWin;
        transform = trans;
    }

    public PlayerGameobject GetPlayerGameobject
    {
        get { return playerGameobject; }
    }

    public bool GetWinner
    {
        get { return winner; }
    }

    public bool GetInstantWinner
    {
        get { return instantWinner; }
    }

    public Transform GetTransform
    {
        get { return transform; }
    }
}