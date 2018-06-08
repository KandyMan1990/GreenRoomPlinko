using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] Text Name, Played, Won, Lost, Made, Received, Average, Vertical, Room;
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

                    Name.text = accountData.DisplayName;
                    Played.text = loadData.GetNumber("played").ToString();
                    Won.text = loadData.GetNumber("won").ToString();
                    Lost.text = loadData.GetNumber("lost").ToString();
                    Made.text = loadData.GetNumber("made").ToString();
                    Received.text = loadData.GetNumber("received").ToString();
                    Average.text = loadData.GetNumber("average").ToString();
                    Vertical.text = loadData.GetString("vertical");
                    Room.text = loadData.GetString("room");
                }
            });
        });
    }
}