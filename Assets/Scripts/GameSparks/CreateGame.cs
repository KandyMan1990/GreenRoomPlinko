using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;

public class CreateGame : MonoBehaviour
{
    DateTime dateTime;
    GSData loadData;
    AccountDetailsResponse accountData;
    string room;

    public void OnClick()
    {
        new AccountDetailsRequest().Send((accountResponse) =>
        {
            accountData = accountResponse;

            new LogEventRequest().SetEventKey("LOAD_PLAYER").Send((loadResponse) =>
            {
                if (!loadResponse.HasErrors)
                {
                    dateTime = DateTime.Now;
                    dateTime = dateTime.AddMinutes(5);

                    loadData = loadResponse.ScriptData.GetGSData("playerData");

                    room = loadData.GetString("room");
                    Char C = Char.ToUpper(room[0]);
                    room = room.Replace(room[0], C);
                    room += " Room";

                    new CreateChallengeRequest()
                        .SetAccessType("PUBLIC")
                        .SetAutoStartJoinedChallengeOnMaxPlayers(false)
                        .SetChallengeMessage(room)
                        .SetChallengeShortCode("PLINKO")
                        .SetEndTime(dateTime)
                        .SetMaxPlayers(10)
                        .SetMinPlayers(2)
                        .Send((response) =>
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                        });
                }
            });
        });
    }
}