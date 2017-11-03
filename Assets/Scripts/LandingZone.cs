using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class LandingZone : MonoBehaviour
{
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
    BoxCollider2D Collider;

    void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<BoxCollider2D>();
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
            Renderer.color = InstantWinColour;
            return;
        }

        Renderer.color = winZone ? WinColour : LoseColour;
    }
}