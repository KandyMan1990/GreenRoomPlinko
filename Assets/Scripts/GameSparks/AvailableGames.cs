using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using GameSparks.Core;

public class AvailableGames : MonoBehaviour
{
    [SerializeField] GameObject availableMatchPrefab;
    [SerializeField] Transform availableGamesPanel;
    WaitForSeconds wait;
    List<string> shortCodes;

    // Use this for initialization
    void Start()
    {
        wait = new WaitForSeconds(5f);
        shortCodes = new List<string>();
        shortCodes.Add("PLINKO");

        for (int i = 0; i < 7; i++)
        {
            GameObject go = Instantiate(availableMatchPrefab, availableGamesPanel);
            go.SetActive(false);
        }

        StartCoroutine(PollForMatches());
    }

    IEnumerator PollForMatches()
    {
        while (true)
        {
            new FindChallengeRequest()
                .SetAccessType("PUBLIC")
                .SetShortCode(shortCodes)
                .Send((requestResponse) =>
                {
                    GSEnumerable<FindChallengeResponse._Challenge> challengeInstances = requestResponse.ChallengeInstances;

                    foreach (Transform child in availableGamesPanel)
                    {
                        child.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                        child.gameObject.SetActive(false);
                    }

                    int i = 0;
                    foreach (FindChallengeResponse._Challenge item in challengeInstances)
                    {
                        GameObject go = availableGamesPanel.GetChild(i).gameObject;
                        go.GetComponentInChildren<Text>().text = item.ChallengeMessage;
                        go.GetComponentInChildren<Button>().onClick.AddListener(() =>
                        {
                            new AcceptChallengeRequest()
                                .SetChallengeInstanceId(item.ChallengeId)
                                .SetMessage(string.Empty)
                                .Send((response) => {
                                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                                });
                        });
                        go.SetActive(true);
                    }
                });

            yield return wait;
        }
    }
}