using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;

public class AvailableGames : MonoBehaviour
{
    [SerializeField]
    GameObject availableMatchPrefab;
    WaitForSeconds wait;
    List<string> shortCodes;

    // Use this for initialization
    void Start()
    {
        wait = new WaitForSeconds(5f);
        shortCodes = new List<string>();
        shortCodes.Add("PLINKO");

        StartCoroutine(PollForMatches());
    }

    IEnumerator PollForMatches()
    {
        while (true)
        {
            new FindChallengeRequest()
                .SetAccessType("PUBLIC")
                .SetShortCode(shortCodes)
                .Send(response =>
                {
                    Debug.Log(response);
                });

            yield return wait;
        }
    }
}