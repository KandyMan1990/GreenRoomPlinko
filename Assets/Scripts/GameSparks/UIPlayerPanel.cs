using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;

public class UIPlayerPanel : MonoBehaviour
{
    // add players from gamesparks list
    // periodically check or receive event to update list
    // stop listening once game starts
    void Start()
    {
    //    User user = UserData.Load();

    //    List<string> shortCodes;
    //    shortCodes = new List<string>();
    //    shortCodes.Add("PLINKO");

    //    new FindChallengeRequest()
    //.SetAccessType("PUBLIC")
    //.SetShortCode(shortCodes)
    //.Send((requestResponse) =>
    //{
    //    GSEnumerable<FindChallengeResponse._Challenge> challengeInstances = requestResponse.ChallengeInstances;

    //    foreach (FindChallengeResponse._Challenge item in challengeInstances)
    //    {
    //        Debug.Log(item);
    //    }
    //});

    //    new GetChallengeRequest()
    //        .SetChallengeInstanceId(user.ChallengeId)
    //        .Send((response) => {
    //            Debug.Log(response.HasErrors);
    //        });

    }
}