using UnityEngine;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;

public class CreateGame : MonoBehaviour
{
    System.DateTime dateTime;

    public void OnClick()
    {
        dateTime = System.DateTime.Now;
        dateTime = dateTime.AddMinutes(5);

        new CreateChallengeRequest()
            .SetAccessType("PUBLIC")
            .SetAutoStartJoinedChallengeOnMaxPlayers(false)
            .SetChallengeMessage("I challenge you to a plinko!")
            .SetChallengeShortCode("PLINKO")
            .SetEndTime(dateTime)
            .SetMaxPlayers(10)
            .SetMinPlayers(2)
            .Send(response =>
            {
                Debug.Log(response); // available games panel should check periodically for challenges
            });
    }
}