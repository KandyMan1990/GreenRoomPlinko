using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] Text Name, Played, Won, Lost, Made, Received, Average, Vertical, Room;
    [SerializeField] User user;
    GSData loadData;
    AccountDetailsResponse accountData;

    void Start()
    {
        new AccountDetailsRequest().Send((accountResponse) =>
        {
            accountData = accountResponse;

            new LogEventRequest().SetEventKey("LOAD_PLAYER").Send((loadResponse) =>
            {
                if (!loadResponse.HasErrors)
                {
                    loadData = loadResponse.ScriptData.GetGSData("playerData");

                    user.Name = accountData.DisplayName;
                    user.Played = (int)loadData.GetNumber("played");
                    user.Won = (int)loadData.GetNumber("won");
                    user.Lost = (int)loadData.GetNumber("lost");
                    user.Made = (int)loadData.GetNumber("made");
                    user.Received = (int)loadData.GetNumber("received");
                    user.Average = (int)loadData.GetNumber("average");
                    user.Vertical = loadData.GetString("vertical");
                    user.Room = loadData.GetString("room");

                    Name.text = user.Name;
                    Played.text = user.Played.ToString();
                    Won.text = user.Won.ToString();
                    Lost.text = user.Lost.ToString();
                    Made.text = user.Made.ToString();
                    Received.text = user.Received.ToString();
                    Average.text = user.Average.ToString();
                    Vertical.text = user.Vertical;
                    Room.text = user.Room;
                }
            });
        });
    }
}