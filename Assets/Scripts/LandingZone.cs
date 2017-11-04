using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class LandingZone : MonoBehaviour
{
    [SerializeField]
    Color defaultColour;
    [SerializeField]
    Color WinColour;
    [SerializeField]
    Color LoseColour;
    [SerializeField]
    Color InstantWinColour;
    [SerializeField]
    bool winZone;

    static bool instantWin;

    SpriteRenderer Renderer;

    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();

        Renderer.color = defaultColour;
    }

    void toggleWinZone()
    {
        winZone = !winZone;
    }

    public static void ResetInstantWin()
    {
        instantWin = false;
    }

    public bool WinZone
    {
        get { return winZone; }
    }

    public bool InstantWinZone
    {
        get { return Renderer.color == InstantWinColour; }
    }

    public void ResetLandingZone()
    {
        toggleWinZone();

        if (winZone && !instantWin && Random.Range(0, 100) <= 1)
        {
            instantWin = true;
            StartCoroutine(FadeColour(InstantWinColour));
            return;
        }

        Color temp = winZone ? WinColour : LoseColour;
        StartCoroutine(FadeColour(temp));
    }

    public void RevertToOriginalState()
    {
        StartCoroutine(FadeColour(defaultColour));
    }

    IEnumerator FadeColour(Color c)
    {
        float lerpTime = 0f;

        while (lerpTime < 1f)
        {
            Color tempColour = Color.Lerp(Renderer.color, defaultColour, lerpTime);
            Renderer.color = tempColour;
            lerpTime += Time.deltaTime / 2;
            yield return null;
        }

        lerpTime = 0f;

        while (lerpTime < 1f)
        {
            Color tempColour = Color.Lerp(Renderer.color, c, lerpTime);
            Renderer.color = tempColour;
            lerpTime += Time.deltaTime / 2;
            yield return null;
        }
    }
}